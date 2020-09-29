using System.Runtime.InteropServices;

public static class Plugin
{
    private const string DLL_NAME = "DataChannelUnity";

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcCreatePeerConnection(string[] ice_servers, int ice_servers_count);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcDeletePeerConnection(int pc);
}
