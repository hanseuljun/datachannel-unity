New-Item .\build -ItemType Directory
New-Item .\build\x86 -ItemType Directory
New-Item .\build\x64 -ItemType Directory

cmake -B .\build\x86 -G "Visual Studio 16 2019" -A Win32 -DCMAKE_TOOLCHAIN_FILE="$CMakeToolchainFile"
cmake -B .\build\x64 -G "Visual Studio 16 2019" -A x64 -DCMAKE_TOOLCHAIN_FILE="$CMakeToolchainFile"
Pause
