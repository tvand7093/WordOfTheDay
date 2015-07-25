#!/bin/bash

# Install Mono
echo Updating Homebrew...
brew update

echo Installing mono...
brew install mono

echo Running Calabash install script...
# Install Calabash
ruby <(curl -fsSL https://raw.githubusercontent.com/calabash/install/master/install-calabash-local-osx.rb)

echo Exporting variables...
export GEM_HOME=~/.calabash
export GEM_PATH=~/.calabash
export PATH="$PATH:$HOME/.calabash/bin"

# Restore NuGet packages
mono --runtime=v4.0.30319 ./.nuget/nuget.exe restore WordOfTheDay.sln
