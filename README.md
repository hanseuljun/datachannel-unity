# datachannel-unity

# How to Build
1. Install opennssl for both x86 and x64 using vcpkg.
```powershell
.\vcpkg.exe install openssl:x86-uwp openssl:x64-windows
```
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
```
