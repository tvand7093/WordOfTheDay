#!/bin/bash

echo Building solution and running Calabash tests...
# build project and run calabash tests
xbuild /p:Configuration=Test WordOfTheDay.sln

echo Running NUnit Tests
# Run NUnit Tests
mono ./packages/NUnit.Runners.2.6.4/tools/nunit-console.exe "./WordOfTheDayTests/bin/Test/WordOfTheDayTests.dll"
