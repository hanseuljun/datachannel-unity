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

&$msBuild ("/t:DataChannelUnity", "/p:Configuration=$configuration", "/p:Platform=x64", "$buildPath\x64\DataChannelUnity.sln")
&$msBuild ("/t:DataChannelUnity", "/p:Configuration=$configuration", "/p:Platform=Win32", "$buildPath\x86-uwp\DataChannelUnity.sln")

$packagePath = (Get-Location).path + "\unity\DataChannel"

$x64Path = "$buildPath\x64\src\$configuration"
$x86UwpPath = "$buildPath\x86-uwp\src\$configuration"

$x64DataChannelPath = "$buildPath\x64\libdatachannel\$configuration"
$x86UwpDataChannelPath = "$buildPath\x86-uwp\libdatachannel\$configuration"

$editorPath = "$packagePath\Editor\Plugins"
$uwpX86PluginPath = "$packagePath\Runtime\Plugins\UWP\x86"
$uwpX86BinPath = (Get-Location).path + "\unity\bin\x86-uwp"

Copy-Item "$x86UwpPath\DataChannelUnity.dll" -Destination $uwpX86PluginPath
Copy-Item "$uwpX86BinPath\DataChannelUnity.dll.meta" -Destination $uwpX86PluginPath

Copy-Item "$x86UwpDataChannelPath\datachannel.dll" -Destination $uwpX86PluginPath
Copy-Item "$uwpX86BinPath\datachannel.dll.meta" -Destination $uwpX86PluginPath

Copy-Item "$x86UwpDataChannelPath\libcrypto-1_1.dll" -Destination $uwpX86PluginPath
Copy-Item "$uwpX86BinPath\libcrypto-1_1.dll.meta" -Destination $uwpX86PluginPath

Copy-Item "$x86UwpDataChannelPath\libssl-1_1.dll" -Destination $uwpX86PluginPath
Copy-Item "$uwpX86BinPath\libssl-1_1.dll.meta" -Destination $uwpX86PluginPath

Copy-Item "$x64Path\DataChannelUnity.dll" -Destination $editorPath
Copy-Item "$x64DataChannelPath\datachannel.dll" -Destination $editorPath
Copy-Item "$x64DataChannelPath\libcrypto-1_1-x64.dll" -Destination $editorPath
Copy-Item "$x64DataChannelPath\libssl-1_1-x64.dll" -Destination $editorPath

Pause