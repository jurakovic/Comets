#!/bin/bash

#parameters:
#version (default: auto; auto, "x.x.x..")
#bump (default: patch; major, minor, patch)
#arch (default: x64; x64, x86)


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


latest=$(git show origin/release:version)

echo $latest

version="0.9.0"

#dotnet publish src/Comets.Application -f net8.0-windows -r win-x64 --no-self-contained -p:AssemblyVersion=$version -p:Version=$version

bump_version "1.2.3" "minor" "false"

