Invoke-Expression -Command $PSScriptRoot\scripts\install-vcpkgs.ps1
Invoke-Expression -Command $PSScriptRoot\scripts\cmake-vs.ps1
Invoke-Expression -Command $PSScriptRoot\scripts\build-plugin.ps1