# datachannel-unity

# How to Build
1. Run init-vcpkg.ps1
2. Run cmake-vs.ps1
3. Run build-plugin.ps1

## Directory Layout
```
. 
+-- libdatachannel                   # paullouisageneau/libdatachannel as a submodule 
+-- src                              # C++ source code 
+-- unity 
|   +-- DataChannel                  # The Unity package 
|   +-- bin                          # .meta files for the ./DataChannel package 
+-- vcpkg                            # microsoft/vcpkg as a submodule 
+-- build-plugin.ps1                 # Builds the unity plugin for /unity/DataChannel with code in /src 
+-- cmake-vs.ps1                     # Runs cmake for Visual Studio 2019 
+-- init-vcpkg.ps1                   # Installs packages in vcpkgs.txt using /vcpkg. 
+-- vcpkgs.txt                       # Lists of packages to install using /vcpkg. 
```
