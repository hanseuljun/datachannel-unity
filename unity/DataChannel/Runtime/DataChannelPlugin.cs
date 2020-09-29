using System;
using System.Runtime.InteropServices;

public static class DataChannelPlugin
{
    public delegate void RtcDescriptionCallbackFunc(string sdp, string type, IntPtr ptr);

    private const string DLL_NAME = "DataChannelUnity";

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcCreatePeerConnection(string[] ice_servers, int ice_servers_count);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcDeletePeerConnection(int pc);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetLocalDescriptionCallback(int pc, RtcDescriptionCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetLocalDescription(int pc);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcAddDataChannel(int pc, string label);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcDeleteDataChannel(int dc);
}
