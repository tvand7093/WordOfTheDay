#!/bin/bash

rvm install ruby-2.2.2

# Install Mono
brew update
brew install mono

# Install Calabash
ruby <(curl -fsSL https://raw.githubusercontent.com/calabash/install/master/install-calabash-local-osx.rb)

# Setup path
touch ~/.bashrc

export GEM_HOME=~/.calabash >> ~/.bashrc
export GEM_PATH=~/.calabash >> ~/.bashrc
export PATH="$PATH:$HOME/.calabash/bin" >> ~/.bashrc

# Reload shell
source ~/.bashrc

# Restore NuGet packages
nuget restore WordOfTheDay.sln
