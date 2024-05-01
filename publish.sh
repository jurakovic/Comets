#!/bin/bash

latest=$(git show release:version)

echo $latest

version="0.9.0"

dotnet publish src/Comets.Application -f net8.0-windows -r win-x64 --no-self-contained -p:AssemblyVersion=$version -p:Version=$version

