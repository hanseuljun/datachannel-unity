# datachannel-unity

# How to Build
1. Run install-vcpkgs.ps1
2. Run cmake-vs.ps1
3. Run build-plugin.ps1

# How to Use
1. Add this repository as a submodule to your repository.
2. Build this repository.
3. Add /unity/DataChannel as a local package to your Unity project.
4. Write code using scripts at /unity/DataChannel/Runtime. DataChannelPlugin.cs contains the P/Invoke methods connected to the C APIs of libdatachannel (https://github.com/paullouisageneau/libdatachannel/blob/master/include/rtc/rtc.h). DataChannel and PeerConnection are C# wrapper classes that would be easier to use.

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
