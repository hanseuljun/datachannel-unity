$VcpkgPath = "$PSScriptRoot\..\vcpkg"

# Start-Process -Wait gets stuck sometimes. Using -Passthru with "| Wait-Process" instead.
Start-Process -Passthru -NoNewWindow -FilePath $VcpkgPath\bootstrap-vcpkg.bat | Wait-Process

$PackageList = Get-Content -Path "$PSScriptRoot\vcpkgs.txt"
$ArgumentList = "install --recurse $PackageList"
Start-Process -Passthru -NoNewWindow -FilePath $VcpkgPath\vcpkg.exe -ArgumentList $ArgumentList | Wait-Process
