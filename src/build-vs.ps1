New-Item .\build -ItemType Directory
cmake -S .\src -B .\build -G "Visual Studio 16 2019" -A x64
Pause
