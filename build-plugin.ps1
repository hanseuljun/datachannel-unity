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

&$msBuild ("/t:DataChannelUnity", "/p:Configuration=$configuration", "/p:Platform=Win32", "$buildPath\x86\DataChannelUnity.sln")
&$msBuild ("/t:DataChannelUnity", "/p:Configuration=$configuration", "/p:Platform=x64", "$buildPath\x64\DataChannelUnity.sln")

$packagePath = (Get-Location).path + "\unity\DataChannel"

$x86Path = "$buildPath\x86\src\$configuration"
$x64Path = "$buildPath\x64\src\$configuration"
$uwpPluginPath = "$packagePath\Runtime\Plugins\UWP\x86"
$editorPath = "$packagePath\Editor\Plugins"
$binPath = (Get-Location).path + "\unity\bin"

Copy-Item "$x86Path\DataChannelUnity.dll" -Destination $uwpPluginPath
Copy-Item "$binPath\DataChannelUnity.dll.meta" -Destination $uwpPluginPath

if ($debug -eq $false) {
	Copy-Item "$binPath\msvcp140.dll" -Destination $uwpPluginPath
	Copy-Item "$binPath\msvcp140.dll.meta" -Destination $uwpPluginPath

	Copy-Item "$binPath\vcruntime140.dll" -Destination $uwpPluginPath
	Copy-Item "$binPath\vcruntime140.dll.meta" -Destination $uwpPluginPath
} else {
	Copy-Item "$binPath\msvcp140d.dll" -Destination $uwpPluginPath
	Copy-Item "$binPath\msvcp140d.dll.meta" -Destination $uwpPluginPath

	Copy-Item "$binPath\vcruntime140d.dll" -Destination $uwpPluginPath
	Copy-Item "$binPath\vcruntime140d.dll.meta" -Destination $uwpPluginPath
}

Copy-Item "$x86Path\libcrypto-1_1.dll" -Destination $uwpPluginPath
Copy-Item "$binPath\libcrypto-1_1.dll.meta" -Destination $uwpPluginPath

Copy-Item "$binPath\libssl-1_1.dll.meta" -Destination $uwpPluginPath
Copy-Item "$x86Path\libssl-1_1.dll" -Destination $uwpPluginPath


Copy-Item "$x64Path\DataChannelUnity.dll" -Destination $editorPath
Copy-Item "$x64Path\libcrypto-1_1-x64.dll" -Destination $editorPath
Copy-Item "$x64Path\libssl-1_1-x64.dll" -Destination $editorPath

Pause