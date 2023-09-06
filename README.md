# HeresyDebug
## A mod for Blasphemous 2

This is a prototype mod for Blasphemous 2, version 1.0.5 (release). Currently, its functionality is limited to an attempted fix for a softlock that rarely occurs after defeating Svsona, Fermosa Fembra. Note that it attempts to lock players out of Yerma's questline bad ending.

### Build Instructions (Not Required for Install)

1. Install BepInEx and run the game.
2. Create a new directory "lib" wherever the project source is.
3. Copy all the dlls from Game Folder > BepInEx > interop to the "lib" folder.
4. Using Visual Studio 2022 targeting .NET 6.0, open and build the solution.

Output is at source folder > bin > Debug (or Release, depending on your options) > net6.0 > HeresyDebug.dll
