#!/bin/bash

# Default values
BRANCH=""
COMMIT=""
MESSAGE=""
VERSION="auto"
BUMP_TYPE="patch"
PREVIEW="auto"
ARCH="x64"

# Allowed values
_bump_type_values=("major" "minor" "patch")
_preview_values=("auto" "true" "false")
_arch_values=("x64" "x86")

function main() {
    read_args "$@"

    read -p "Press any key to continue..." -n1 -s; echo

    dotnet publish src/Comets.Application -f net8.0-windows -r win-$ARCH --no-self-contained -p:AssemblyVersion="$(echo $VERSION | sed 's/-preview//')" -p:Version="$VERSION"

    if [[ ! $? -eq 0 ]]; then exit 1; fi # exit if build canceled

    local sha256=$(sha1sum.exe "src/Comets.Application/bin/Release/net8.0-windows/win-$ARCH/publish/Comets.exe" | cut -d " " -f 1)
    local url="https://github.com/jurakovic/Comets/releases/tag/v$VERSION"

    git switch -c release origin/release || git checkout release

    echo "VERSION:   $VERSION"    > version
    echo "BUMP_TYPE: $BUMP_TYPE" >> version
    echo "PREVIEW:   $PREVIEW"   >> version
    echo "ARCH:      $ARCH"      >> version
    echo "BRANCH:    $BRANCH"    >> version
    echo "COMMIT:    $COMMIT"    >> version
    echo "SHA256:    $sha256"    >> version
    echo "URL:       $url"       >> version

    git add version
    git commit -m "$VERSION ($BRANCH)"
    git push

    git checkout "$BRANCH"
    git tag "$VERSION"
    git push origin "$VERSION"
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
        esac
        shift
    done

    BRANCH=$(git branch --show-current)
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
        local previous=$(git show origin/release:version | head -n 1 | sed 's/VERSION:   //')
        echo "PREV_VER:  $previous"
        VERSION=$(bump_version "$previous" "$BUMP_TYPE" "$PREVIEW")
    else
        BUMP_TYPE=""
        PREVIEW=""
    fi

    echo "VERSION:   $VERSION"
    echo "BUMP_TYPE: $BUMP_TYPE"
    echo "PREVIEW:   $PREVIEW"
    echo "ARCH:      $ARCH"
    echo "BRANCH:    $BRANCH"
    echo "COMMIT:    $COMMIT"
    echo "MESSAGE:   $MESSAGE"
}

function help() {
    echo "Options:                         Allowed values:"
    echo "  -v, --version <VERSION>        auto*, x.y.z"
    echo "  -b, --bump_type <BUMP_TYPE>    major, minor, patch*"
    echo "  -p, --preview <PREVIEW>        auto*, true, false"
    echo "  -a, --arch <ARCH>              x64*, x86"
    echo "  -h, --help"
    echo "                                 * Default value"
    echo ""
    echo "Example usage:"
    echo "  ./publish.sh"
    echo "  ./publish.sh -v 1.2.3"
    echo "  ./publish.sh -b major -p true"
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
