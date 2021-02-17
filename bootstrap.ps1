Write-Output "Running datachannel-unity\scripts\install-vcpkgs.ps1"
Invoke-Expression -Command $PSScriptRoot\scripts\install-vcpkgs.ps1

Write-Output "Running datachannel-unity\scripts\cmake-vs.ps1"
Invoke-Expression -Command $PSScriptRoot\scripts\cmake-vs.ps1

Write-Output "Running datachannel-unity\scripts\build-plugins.ps1"
Invoke-Expression -Command $PSScriptRoot\scripts\build-plugins.ps1