$vcpkgPath = $PSScriptRoot + "\..\vcpkg"
$txtPath = $PSScriptRoot + "\..\vcpkgs.txt"

# Start-Process -Wait gets stuck sometimes. Using -Passthru with "| Wait-Process" instead.
Start-Process -Passthru -NoNewWindow -FilePath $vcpkgPath\bootstrap-vcpkg.bat | Wait-Process

$vcpkgs = Get-Content -Path $txtPath
$args = ,"install --recurse" + $vcpkgs
Start-Process -Passthru -NoNewWindow -FilePath $vcpkgPath\vcpkg.exe -ArgumentList $args | Wait-Process
