Start-Process -NoNewWindow -Wait -FilePath .\vcpkg\bootstrap-vcpkg.bat

$vcpkgs = Get-Content -Path .\vcpkgs.txt
$args = ,"install" + $vcpkgs
Start-Process -NoNewWindow -Wait -FilePath .\vcpkg\vcpkg.exe -ArgumentList $args
