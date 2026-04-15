#!/bin/bash

# Default values
COMMAND="publish"
BRANCH=""
COMMIT=""
MESSAGE=""
VERSION="auto"
BUMP_TYPE="patch"
PREVIEW="auto"
ARCH="win-x64"
UNATTENDED="false"
BASE_VER="0"

# Allowed values
_command_values=("publish" "package" "release" "version")
_bump_type_values=("major" "minor" "patch")
_preview_values=("auto" "true" "false")
_arch_values=("win-x64") #"linux-x64" "osx-x64")
_unattended_values=("true" "false")

# Colors
Color_Off='\033[0m'
Color_Green='\033[0;32m'
Color_Yellow='\033[0;33m'

function main() {
  read_args "$@"

  case "$COMMAND" in
    "publish") publish ;;
    "package") package ;;
    "release") release ;;
    "version") version_cmd ;;
  esac
}

function publish() {
  rm -rf src/bin/ src/obj/
  dotnet publish src/Comets.Application -c Release -r $ARCH --no-self-contained -p:AssemblyVersion="$(echo $VERSION | sed 's/-preview//')" -p:Version="$VERSION" -o ./publish/$ARCH

  if [[ ! $? -eq 0 ]]; then exit 1; fi # exit if build failed or canceled

  echo
  echo -e "${Color_Green}Publish OK${Color_Off}"
}

function package() {
  local publish_path="publish/$ARCH"
  local assembly_name="Comets"
  local assembly_ext=""
  local shacmd=""

  case "$ARCH" in
    win*) assembly_ext=".exe" ;;
  esac

  case "$ARCH" in
    lin*) shacmd="sha256sum" ;;
    win*) shacmd="sha256sum$assembly_ext" ;;
    mac*) shacmd="shasum -a 256" ;;
  esac

  local release_path="release/$ARCH"
  local archive_name="${assembly_name}_${VERSION}_${ARCH}.zip"
  local archive_path="$release_path/../$archive_name"
  local assembly_full="${assembly_name}${assembly_ext}"

  mkdir -p $release_path
  cp "$publish_path/$assembly_full" "$release_path"
  curl -s "https://minorplanetcenter.net/iau/Ephemerides/Comets/Soft00Cmt.txt" -o $release_path/Comets.db

  cd $release_path && zip -r -9 "../$archive_name" "*" && cd - > /dev/null

  local sha256=$($shacmd "$archive_path" | cut -d " " -f 1)
  echo "$sha256  $archive_name" >> $release_path/../checksums.txt

  echo -e "${Color_Green}Package OK${Color_Off}"
}

function release() {
  if [ ! $UNATTENDED = "true" ]
  then
    read -p "Do you want to continue to git tag, push...? (y/n) " yn
    if [ ! $yn = "y" ]; then exit; fi
  fi

  git tag "v$VERSION"
  git push origin "v$VERSION"

  local url="$(git remote get-url origin | sed -E 's/\.git$//')/releases/tag/v$VERSION"
  echo -e "${Color_Yellow}Version $VERSION released${Color_Off}"
  echo -e "URL: $url"
  echo -e "${Color_Green}All done${Color_Off}"
}

function version_cmd() {
  echo "$VERSION"
}

function read_args() {
  while [ "${1:-}" != "" ]; do
    case "$1" in
      "-h" | "--help")
        help
        exit 0
        ;;
      "-v" | "--version")
        shift
        # todo: regex validate
        VERSION="$1"
        ;;
      "-b" | "--bump_type")
        shift
        if is_in_array "$1" "${_bump_type_values[@]}"; then
            BUMP_TYPE="$1"
        else
            invalid_argument "$1" "${_bump_type_values[*]}"
            exit 1
        fi
        ;;
      "-p" | "--preview")
        shift
        if is_in_array "$1" "${_preview_values[@]}"; then
            PREVIEW="$1"
        else
            invalid_argument "$1" "${_preview_values[*]}"
            exit 1
        fi
        ;;
      "-a" | "--arch")
        shift
        if is_in_array "$1" "${_arch_values[@]}"; then
            ARCH="$1"
        else
            invalid_argument "$1" "${_arch_values[*]}"
            exit 1
        fi
        ;;
      "-u" | "--unattended")
        shift
        if is_in_array "$1" "${_unattended_values[@]}"; then
            UNATTENDED="$1"
        else
            invalid_argument "$1" "${_unattended_values[*]}"
            exit 1
        fi
        ;;
      "-M" | "--base_ver")
        shift
        BASE_VER="$1"
        ;;
      *)
        if is_in_array "$1" "${_command_values[@]}"; then
            COMMAND="$1"
        else
            invalid_argument "$1" "${_command_values[*]}"
            exit 1
        fi
        ;;
    esac
    shift
  done

  if [ -n "$GITHUB_REF_NAME" ]; then
    BRANCH="$GITHUB_REF_NAME"
  else
    BRANCH=$(git branch --show-current)
  fi
  COMMIT=$(git log -n 1 --format="%H")
  MESSAGE=$(git log -n 1 --format="%B")

  if [ "$PREVIEW" = "auto" ]; then
    if [ "$BRANCH" != "master" ]; then
      PREVIEW="true"
    else
      PREVIEW="false"
    fi
  fi

  if [ "$VERSION" = "auto" ]; then
    local latest_stable=$(git tag --sort=-version:refname \
      | grep -E "^v${BASE_VER}\.[0-9]+\.[0-9]+$" | head -1 | sed 's/^v//')
    local latest_preview=$(git tag --sort=-version:refname \
      | grep -E "^v${BASE_VER}\.[0-9]+\.[0-9]+-preview\.[0-9]+$" | head -1 | sed 's/^v//')

    if [ "$COMMAND" != "version" ]; then
      echo "BASE_VER:    $BASE_VER"
      echo "STABLE_VER:  $latest_stable"
      echo "PREVIEW_VER: $latest_preview"
    fi

    if [[ -z "$latest_stable" && -z "$latest_preview" ]]; then
      # No tags exist yet for this major stream — start fresh
      if [[ "$PREVIEW" == "true" ]]; then
        VERSION="${BASE_VER}.0.0-preview.1"
      else
        VERSION="${BASE_VER}.0.0"
      fi
    else
      # Determine current_version: whichever is truly latest overall within this stream
      local current_version="$latest_stable"
      if [[ -n "$latest_preview" ]]; then
        local preview_main="${latest_preview%-preview.*}"
        version_compare "$preview_main" "${latest_stable:-0.0.0}"
        if [[ $? -eq 1 ]]; then
          current_version="$latest_preview"  # preview cycle is ahead of stable
        fi
      fi
      VERSION=$(bump_version "$current_version" "$BUMP_TYPE" "$PREVIEW")
    fi
  else
    BUMP_TYPE=""
    PREVIEW=""
  fi

  if [ "$COMMAND" != "version" ]; then
    echo "COMMAND:     $COMMAND"
    echo "VERSION:     $VERSION"
    echo "BUMP_TYPE:   $BUMP_TYPE"
    echo "PREVIEW:     $PREVIEW"
    echo "BASE_VER:    $BASE_VER"
    echo "ARCH:        $ARCH"
    echo "BRANCH:      $BRANCH"
    echo "COMMIT:      $COMMIT"
    echo "MESSAGE:     $MESSAGE"
  fi

  if [ "$COMMAND" = "version" ]; then
    : # no countdown for version query
  elif [ $UNATTENDED = "true" ]
  then
    for i in {3..0..1}; do echo -en "\rContinue in $i" && if [ "$i" -gt "0" ]; then sleep 1; fi; done
    echo -en "\r             "
    echo
  else
    read -p "Press any key to continue..." -n1 -s; echo
    echo
  fi
}

function help() {
  echo "Usage:"
  echo "  ./publish.sh <command> [options]"
  echo ""
  echo "Commands:"
  echo "  publish                        Step 1: run dotnet publish"
  echo "  package                        Step 2: archive executable to zip"
  echo "  release                        Step 3: execute git tag, push..."
  echo "  version                        Print computed version and exit"
  echo ""
  echo "Options:                         Allowed values:"
  echo "  -v, --version <VERSION>        auto*, x.y.z, x.y.z-preview.N"
  echo "  -b, --bump_type <BUMP_TYPE>    major, minor, patch*"
  echo "  -p, --preview <PREVIEW>        auto*, true, false"
  echo "  -M, --base_ver <BASE_VER>      1*, 2, 3, ..."
  echo "  -a, --arch <ARCH>              win-x64, linux-x64, osx-x64"
  echo "  -u, --unattended <UNATTENDED>  true, false*"
  echo "  -h, --help"
  echo "                                 * Default value"
  echo ""
  echo "Examples:"
  echo "  ./publish.sh publish"
  echo "  ./publish.sh publish -v 1.2.3"
  echo "  ./publish.sh publish -b major -p true"
  echo "  ./publish.sh version"
  echo "  ./publish.sh version -b minor"
  echo "  ./publish.sh version -M 2"
}

function is_in_array() {
  local value="$1"
  shift
  for element in "$@"; do
    if [[ "$element" == "$value" ]]; then
      return 0
    fi
  done
  return 1
}

function invalid_argument() {
  echo "Error. Invalid argument value: $1"
  echo "Allowed values: ${*:2}"
  echo
  help
}

function version_compare() {
  # split the versions into main version and prerelease parts
  IFS='-' read -r main1 pre1 <<< "$1"
  IFS='-' read -r main2 pre2 <<< "$2"

  # compare prerelease versions if main versions are the same
  if [[ $main1 == $main2 ]]; then
    if [[ -z $pre1 && -n $pre2 ]]; then
      return 1 # no pre1: 1.2.3 > 1.2.3-preview.X
    elif [[ -n $pre1 && -z $pre2 ]]; then
      return 2 # no pre2: 1.2.3-preview.X < 1.2.3
    elif [[ -n $pre1 && -n $pre2 ]]; then
      if [[ $pre1 == $pre2 ]]; then
        return 0 # preview.1 = preview.1
      else
        # extract numeric parts for comparison
        p1="${pre1/'preview.'/}"
        p2="${pre2/'preview.'/}"
        if (($p1 > $p2)); then
          return 1 # preview.10 > preview.1
        else
          return 2 # preview.1 < preview.10
        fi
      fi
    else
      return 0 # 1.2.3 = 1.2.3
    fi
  fi

  # compare the main versions numerically
  # src: https://stackoverflow.com/a/4025065; vercomp
  local IFS=.
  local i ver1=($main1) ver2=($main2)
  # fill empty fields in ver1 with zeros
  for ((i=${#ver1[@]}; i<${#ver2[@]}; i++))
  do
    ver1[i]=0
  done
  for ((i=0; i<${#ver1[@]}; i++))
  do
    if ((10#${ver1[i]:=0} > 10#${ver2[i]:=0}))
    then
      return 1
    fi
    if ((10#${ver1[i]} < 10#${ver2[i]}))
    then
      return 2
    fi
  done
  return 0
}

function bump_version() {
  local current_version="$1"
  local bump_type="$2"
  local preview="$3"

  IFS='-' read -ra chunks <<< $(echo $current_version | sed 's/-preview./-/')
  local current_main_version="${chunks[0]}"
  local current_prev_version="${chunks[1]:-0}"
  local next_version=""

  if [[ "$preview" == "false" ]]; then
    if [[ "$current_prev_version" -eq 0 ]]; then
      # Regular bump
      next_version=$(bump_version_main "$current_main_version" "$bump_type")
    else
      # Unpreview
      next_version="$current_main_version"
    fi
  else
    if [[ "$current_prev_version" -eq 0 ]]; then
      # First preview
      next_version=$(bump_version_main "$current_main_version" "$bump_type")
      next_version+="-preview.1"
    else
      # Bumping existing preview
      next_version="$current_main_version"
      next_version+="-preview.$((current_prev_version + 1))"
    fi
  fi

  echo "$next_version"
}

function bump_version_main() {
  local current_version="$1"
  local bump_type="$2"

  # Split the version into major, minor, and patch
  IFS='.' read -ra chunks <<< "$current_version"
  local major="${chunks[0]}"
  local minor="${chunks[1]:-0}"
  local patch="${chunks[2]:-0}"

  # Increment version based on bump type
  case "$bump_type" in
    major)
      ((major++))
      minor=0
      patch=0
      ;;
    minor)
      ((minor++))
      patch=0
      ;;
    patch)
      ((patch++))
      ;;
    *)
      echo "Invalid bump type: $bump_type"
      return 1
      ;;
  esac

  echo "${major}.${minor}.${patch}"
}

main "$@"
