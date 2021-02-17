param ($Config = 'RelWithDebInfo')

Write-Output "datachannel-unity build-plugin.ps1 - Config: $Config"

Install-Module VSSetup -Scope CurrentUser

function Find-MsBuild {
    $vsInstance = Get-VSSetupInstance `
    | Select-VSSetupInstance -Version '[16.0, 17.0)' -Latest
    $vsPath = $vsInstance.InstallationPath + '\MSBuild'
    $msBuilds = Get-ChildItem $vsPath -recurse -filter 'MSBuild.exe'
    $msBuilds[0].FullName
}

$MsBuild = Find-MsBuild

$CmakeBuildPath = "$PSScriptRoot\..\build"
$UnityPackagePath = "$PSScriptRoot\..\unity\DataChannel"
$UnityBinPath = "$PSScriptRoot\..\unity\bin"

&$MsBuild('/t:DataChannelUnity', "/p:Configuration=$Config", '/p:Platform=x64', "$CmakeBuildPath\x64\DataChannelUnity.sln")
&$MsBuild('/t:DataChannelUnity', "/p:Configuration=$Config", '/p:Platform=Win32', "$CmakeBuildPath\x86-uwp\DataChannelUnity.sln")
&$MsBuild('/t:DataChannelUnity', "/p:Configuration=$Config", '/p:Platform=ARM64', "$CmakeBuildPath\arm64-uwp\DataChannelUnity.sln")

function Build-UnityPlugin($Platform, $SslSuffix = '', [Switch]$Editor = $false) {
    $PlatformBuildPath = "$CmakeBuildPath\$Platform\src\$Config"
    $PlatformLibDataChannelPath = "$CmakeBuildPath\$Platform\libdatachannel\$Config"

    $PlatformPluginsPath = "$UnityPackagePath\Runtime\Plugins\$Platform"
    $EditorPluginPath = "$UnityPackagePath\Editor\Plugins"

    $PluginDestination = $Editor -eq $false ? $PlatformPluginsPath : $EditorPluginPath

    Copy-Item "$PlatformBuildPath\DataChannelUnity.dll" -Destination $PluginDestination
    Copy-Item "$PlatformLibDataChannelPath\datachannel.dll" -Destination $PluginDestination
    Copy-Item "$PlatformLibDataChannelPath\libcrypto-1_1$($SslSuffix).dll" -Destination $PluginDestination
    Copy-Item "$PlatformLibDataChannelPath\libssl-1_1$($SslSuffix).dll" -Destination $PluginDestination
    
    if($Editor -eq $false) {
        $PlatformUnityBinPath = "$UnityBinPath\$Platform"

        Copy-Item "$PlatformUnityBinPath\DataChannelUnity.dll.meta" -Destination $PluginDestination
        Copy-Item "$PlatformUnityBinPath\datachannel.dll.meta" -Destination $PluginDestination
        Copy-Item "$PlatformUnityBinPath\libcrypto-1_1$($SslSuffix).dll.meta" -Destination $PluginDestination
        Copy-Item "$PlatformUnityBinPath\libssl-1_1$($SslSuffix).dll.meta" -Destination $PluginDestination
    }
}

Build-UnityPlugin -Platform 'x64' -SslSuffix '-x64' -Editor
Build-UnityPlugin -Platform 'x86-uwp'
Build-UnityPlugin -Platform 'x64' -SslSuffix '-x64'
Build-UnityPlugin -Platform 'arm64-uwp' -SslSuffix '-arm64'
