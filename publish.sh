#!/bin/bash

latest=$(git show release:version)

echo $latest

dotnet publish -f net8.0-windows -r win-x64 --no-self-contained -p:PublishSingleFile=true -p:AssemblyVersion=1.2.3.4 -p:Version=1.2.3.4
