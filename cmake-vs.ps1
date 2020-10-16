New-Item .\build -ItemType Directory
New-Item .\build\x86-uwp -ItemType Directory
New-Item .\build\x64 -ItemType Directory

$CMakeToolchainFile = (Get-Location).path + "\vcpkg\scripts\buildsystems\vcpkg.cmake"
cmake -B .\build\x86-uwp -G "Visual Studio 16 2019" -A Win32 -DCMAKE_SYSTEM_NAME=WindowsStore -DCMAKE_SYSTEM_VERSION="10.0" -DCMAKE_TOOLCHAIN_FILE="$CMakeToolchainFile" -DCAPI_STDCALL=ON
cmake -B .\build\x64 -G "Visual Studio 16 2019" -A x64 -DCMAKE_TOOLCHAIN_FILE="$CMakeToolchainFile" -DCAPI_STDCALL=ON

Write-Output $CMakeToolchainFile
Pause
