# Start-Process -Wait gets stuck sometimes. Using -Passthru with "| Wait-Process" instead.
Start-Process -Passthru -NoNewWindow -FilePath .\vcpkg\bootstrap-vcpkg.bat | Wait-Process

$vcpkgs = Get-Content -Path .\vcpkgs.txt
$args = ,"install" + $vcpkgs
Start-Process -Passthru -NoNewWindow -FilePath .\vcpkg\vcpkg.exe -ArgumentList $args | Wait-Process
