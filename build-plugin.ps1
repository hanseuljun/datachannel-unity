Install-Module VSSetup -Scope CurrentUser

$vsInstance = Get-VSSetupInstance `
            | Select-VSSetupInstance -Version '[16.0, 17.0)' -Latest
$vsPath = $vsInstance.InstallationPath + "\MSBuild"
$msBuilds = Get-ChildItem $vsPath -recurse -filter "MSBuild.exe"
$msBuild = $msBuilds[0].FullName

$configuration = "RelWithDebInfo"
$buildPath = (Get-Location).path + "\build"

&$msBuild ("/t:DataChannelUnity", "/p:Configuration=$configuration", "/p:Platform=Win32", "$buildPath\x86\DataChannelUnity.sln")
&$msBuild ("/t:DataChannelUnity", "/p:Configuration=$configuration", "/p:Platform=x64", "$buildPath\x64\DataChannelUnity.sln")

$packagePath = (Get-Location).path + "\unity\DataChannelUnity"

$x86Path = "$buildPath\x86\src\$configuration"
$x64Path = "$buildPath\x64\src\$configuration"
$uwpPluginPath = "$packagePath\Runtime\Plugins\UWP\x86"
$editorPath = "$packagePath\Editor\Plugins"

Copy-Item "$x86Path\DataChannelUnity.dll" -Destination $uwpPluginPath
Copy-Item "$x64Path\DataChannelUnity.dll" -Destination $editorPath

Pause