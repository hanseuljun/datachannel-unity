$SourcePath = $PSScriptRoot + "\.."
$BuildPath = $PSScriptRoot + "\..\build"

New-Item $BuildPath -ItemType Directory

$CMakeToolchainFile = $PSScriptRoot + "\..\vcpkg\scripts\buildsystems\vcpkg.cmake"

cmake -S $SourcePath -B $BuildPath\arm64-uwp -G "Visual Studio 16 2019" -A ARM64 -DCMAKE_SYSTEM_NAME=WindowsStore -DCMAKE_SYSTEM_VERSION="10.0" -DCMAKE_TOOLCHAIN_FILE="$CMakeToolchainFile" -DNO_EXAMPLES=ON -DNO_TESTS=ON -DRSA_KEY_BITS_2048=ON -DCAPI_STDCALL=ON

cmake -S $SourcePath -B $BuildPath\x64 -G "Visual Studio 16 2019" -A x64 -DCMAKE_TOOLCHAIN_FILE="$CMakeToolchainFile" -DCAPI_STDCALL=ON

cmake -S $SourcePath -B $BuildPath\x86-uwp -G "Visual Studio 16 2019" -A Win32 -DCMAKE_SYSTEM_NAME=WindowsStore -DCMAKE_SYSTEM_VERSION="10.0" -DCMAKE_TOOLCHAIN_FILE="$CMakeToolchainFile" -DNO_EXAMPLES=ON -DNO_TESTS=ON -DRSA_KEY_BITS_2048=ON -DCAPI_STDCALL=ON