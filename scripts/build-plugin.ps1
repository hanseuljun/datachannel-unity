param ([switch]$debug = $false)

Install-Module VSSetup -Scope CurrentUser

$vsInstance = Get-VSSetupInstance `
            | Select-VSSetupInstance -Version '[16.0, 17.0)' -Latest
$vsPath = $vsInstance.InstallationPath + "\MSBuild"
$msBuilds = Get-ChildItem $vsPath -recurse -filter "MSBuild.exe"
$msBuild = $msBuilds[0].FullName

$configuration = "RelWithDebInfo"
if ($debug) {
    $configuration = "Debug"
}

$buildPath = (Get-Location).path + "\build"

&$msBuild ("/t:DataChannelUnity", "/p:Configuration=$configuration", "/p:Platform=ARM64", "$buildPath\arm64-uwp\DataChannelUnity.sln")
&$msBuild ("/t:DataChannelUnity", "/p:Configuration=$configuration", "/p:Platform=x64", "$buildPath\x64\DataChannelUnity.sln")
&$msBuild ("/t:DataChannelUnity", "/p:Configuration=$configuration", "/p:Platform=Win32", "$buildPath\x86-uwp\DataChannelUnity.sln")

$packagePath = (Get-Location).path + "\unity\DataChannel"

$arm64UwpPath = "$buildPath\arm64-uwp\src\$configuration"
$arm64UwpDataChannelPath = "$buildPath\arm64-uwp\libdatachannel\$configuration"
$arm64UwpPluginPath = "$packagePath\Runtime\Plugins\UWP\arm64"
$arm64UwpBinPath = (Get-Location).path + "\unity\bin\arm64-uwp"

Copy-Item "$arm64UwpPath\DataChannelUnity.dll" -Destination $arm64UwpPluginPath
Copy-Item "$arm64UwpBinPath\DataChannelUnity.dll.meta" -Destination $arm64UwpPluginPath

Copy-Item "$arm64UwpDataChannelPath\datachannel.dll" -Destination $arm64UwpPluginPath
Copy-Item "$arm64UwpBinPath\datachannel.dll.meta" -Destination $arm64UwpPluginPath

Copy-Item "$arm64UwpDataChannelPath\libcrypto-1_1-arm64.dll" -Destination $arm64UwpPluginPath
Copy-Item "$arm64UwpBinPath\libcrypto-1_1-arm64.dll.meta" -Destination $arm64UwpPluginPath

Copy-Item "$arm64UwpDataChannelPath\libssl-1_1-arm64.dll" -Destination $arm64UwpPluginPath
Copy-Item "$arm64UwpBinPath\libssl-1_1-arm64.dll.meta" -Destination $arm64UwpPluginPath

# Copy x64 dll files.
$x64Path = "$buildPath\x64\src\$configuration"
$x64DataChannelPath = "$buildPath\x64\libdatachannel\$configuration"
$editorPath = "$packagePath\Editor\Plugins"

Copy-Item "$x64Path\DataChannelUnity.dll" -Destination $editorPath
Copy-Item "$x64DataChannelPath\datachannel.dll" -Destination $editorPath
Copy-Item "$x64DataChannelPath\libcrypto-1_1-x64.dll" -Destination $editorPath
Copy-Item "$x64DataChannelPath\libssl-1_1-x64.dll" -Destination $editorPath

# Copy x86 dll files and their Unity3D meta files.
$x86UwpPath = "$buildPath\x86-uwp\src\$configuration"
$x86UwpDataChannelPath = "$buildPath\x86-uwp\libdatachannel\$configuration"
$x86UwpPluginPath = "$packagePath\Runtime\Plugins\UWP\x86"
$x86UwpBinPath = (Get-Location).path + "\unity\bin\x86-uwp"

Copy-Item "$x86UwpPath\DataChannelUnity.dll" -Destination $x86UwpPluginPath
Copy-Item "$x86UwpBinPath\DataChannelUnity.dll.meta" -Destination $x86UwpPluginPath

Copy-Item "$x86UwpDataChannelPath\datachannel.dll" -Destination $x86UwpPluginPath
Copy-Item "$x86UwpBinPath\datachannel.dll.meta" -Destination $x86UwpPluginPath

Copy-Item "$x86UwpDataChannelPath\libcrypto-1_1.dll" -Destination $x86UwpPluginPath
Copy-Item "$x86UwpBinPath\libcrypto-1_1.dll.meta" -Destination $x86UwpPluginPath

Copy-Item "$x86UwpDataChannelPath\libssl-1_1.dll" -Destination $x86UwpPluginPath
Copy-Item "$x86UwpBinPath\libssl-1_1.dll.meta" -Destination $x86UwpPluginPath

Pause