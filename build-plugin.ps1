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

&$msBuild ("/t:DataChannelUnity", "/p:Configuration=$configuration", "/p:Platform=Win32", "$buildPath\x86-uwp\DataChannelUnity.sln")
&$msBuild ("/t:DataChannelUnity", "/p:Configuration=$configuration", "/p:Platform=x64", "$buildPath\x64\DataChannelUnity.sln")

$packagePath = (Get-Location).path + "\unity\DataChannel"

$x86UwpPath = "$buildPath\x86-uwp\src\$configuration"
$x64Path = "$buildPath\x64\src\$configuration"

$x86UwpDataChannelPath = "$buildPath\x86-uwp\libdatachannel\$configuration"
$x64DataChannelPath = "$buildPath\x64\libdatachannel\$configuration"

$uwpX86PluginPath = "$packagePath\Runtime\Plugins\UWP\x86"
$editorPath = "$packagePath\Editor\Plugins"
$binPath = (Get-Location).path + "\unity\bin"

Copy-Item "$x86UwpPath\DataChannelUnity.dll" -Destination $uwpX86PluginPath
Copy-Item "$binPath\DataChannelUnity.dll.meta" -Destination $uwpX86PluginPath

Copy-Item "$x86UwpPath\libcrypto-1_1.dll" -Destination $uwpX86PluginPath
Copy-Item "$binPath\libcrypto-1_1.dll.meta" -Destination $uwpX86PluginPath

Copy-Item "$x86UwpPath\libssl-1_1.dll" -Destination $uwpX86PluginPath
Copy-Item "$binPath\libssl-1_1.dll.meta" -Destination $uwpX86PluginPath

Copy-Item "$x86UwpDataChannelPath\datachannel.dll" -Destination $uwpX86PluginPath
Copy-Item "$binPath\datachannel.dll.meta" -Destination $uwpX86PluginPath

Copy-Item "$x64Path\DataChannelUnity.dll" -Destination $editorPath
Copy-Item "$x64Path\libcrypto-1_1-x64.dll" -Destination $editorPath
Copy-Item "$x64Path\libssl-1_1-x64.dll" -Destination $editorPath
Copy-Item "$x64DataChannelPath\datachannel.dll" -Destination $editorPath

Pause