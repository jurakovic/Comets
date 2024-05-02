#!/bin/bash

# Default values
VERSION="auto"
BUMP_TYPE="patch"
ARCH="x64"
PREVIEW="auto"

# Allowed values
_bump_type_values=("major" "minor" "patch")
_arch_values=("x64" "x86")
_preview_values=("auto" "true" "false")

function main() {
    #[ ! "$1" ] && help && return

    while [ "${1:-}" != "" ]; do
        case "$1" in
            "-h" | "--help")
              help
              return
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
            "-a" | "--arch")
              shift
              if is_in_array "$1" "${_arch_values[@]}"; then
                  ARCH="$1"
              else
                  invalid_argument "$1" "${_arch_values[*]}"
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
            #*)
            #  help
            #  return
            #  ;;
        esac
        shift
    done

    if [ "$PREVIEW" = "auto" ]; then
        local branch=$(git branch --show-current)
        if [ "$branch" != "master" ]; then
            PREVIEW="true"
        else
            PREVIEW="false"
        fi
    fi

    if [ "$VERSION" = "auto" ]; then
        local latest=$(git show origin/release:version)
        VERSION=$(bump_version "$latest" "$BUMP_TYPE" "$PREVIEW")
    fi

    echo "ARCH:      $ARCH"
    echo "PREVIEW:   $PREVIEW"
    echo "BUMP_TYPE: $BUMP_TYPE"
    echo "VERSION:   $VERSION"
}

function help() {
    echo "help..."
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
}

function bump_version() {
    local current_version="$1"
    local bump_type="$2"
    local preview="$3"

    IFS='-' read -ra chunks <<< "$current_version"
    local current_main_version="${chunks[0]}"
    local current_prev_version="${chunks[2]:-0}"
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
            next_version+="-preview-1"
        else
            # Bumping existing preview
            next_version="$current_main_version"
            next_version+="-preview-$((current_prev_version + 1))"
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


#invalid_argument "test" "${_bump_type_values[@]}"
main "$@"


exit 0

latest=$(git show origin/release:version)

echo $latest

version="0.9.0"

#dotnet publish src/Comets.Application -f net8.0-windows -r win-x64 --no-self-contained -p:AssemblyVersion=$version -p:Version=$version

bump_version "1.2.3" "minor" "false"

./publish.sh
./publish.sh -v "1.2.3" -b major -a x86 -p true
