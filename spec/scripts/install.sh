#!/bin/bash

MONO_MAJOR=4.0.2
MONO_PATCH=4.0.2.5

# Download Mono
echo Downloading Mono...
curl "http://download.mono-project.com/archive/$MONO_MAJOR/macos-10-x86/MonoFramework-MDK-$MONO_PATCH.macos10.xamarin.x86.pkg" > mono.pkg

# Install Mono
echo Installing Mono...
sudo installer -pkg "mono.pkg" -target /

echo Running Calabash install script...
# Install Calabash
ruby <(curl -fsSL https://raw.githubusercontent.com/calabash/install/master/install-calabash-local-osx.rb)

echo Exporting variables...
export GEM_HOME=~/.calabash
export GEM_PATH=~/.calabash
export PATH="$PATH:$HOME/.calabash/bin"

echo Restoring Nuget Packages...
# Restore NuGet packages
mono --runtime=v4.0.30319 ./.nuget/nuget.exe restore WordOfTheDay.sln
