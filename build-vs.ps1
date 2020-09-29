New-Item .\build -ItemType Directory
cmake -B .\build -G "Visual Studio 16 2019" -A x64 -DCMAKE_TOOLCHAIN_FILE="$CMakeToolchainFile"
Pause
