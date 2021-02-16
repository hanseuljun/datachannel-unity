using System;
using System.Runtime.InteropServices;

public enum RtcState : int
{
    RTC_NEW = 0,
    RTC_CONNECTING = 1,
    RTC_CONNECTED = 2,
    RTC_DISCONNECTED = 3,
    RTC_FAILED = 4,
    RTC_CLOSED = 5
}

public enum RtcGatheringState : int
{
    RTC_GATHERING_NEW = 0,
    RTC_GATHERING_INPROGRESS = 1,
    RTC_GATHERING_COMPLETE = 2
}

public enum RtcLogLevel : int
{
    RTC_LOG_NONE = 0,
    RTC_LOG_FATAL = 1,
    RTC_LOG_ERROR = 2,
    RTC_LOG_WARNING = 3,
    RTC_LOG_INFO = 4,
    RTC_LOG_DEBUG = 5,
    RTC_LOG_VERBOSE = 6
}

public delegate void RtcLogCallbackFunc(RtcLogLevel level, [MarshalAs(UnmanagedType.LPStr)] string message);
public delegate void RtcDescriptionCallbackFunc(int pc, [MarshalAs(UnmanagedType.LPStr)] string sdp, [MarshalAs(UnmanagedType.LPStr)] string type, IntPtr ptr);
public delegate void RtcCandidateCallbackFunc(int pc, [MarshalAs(UnmanagedType.LPStr)] string cand, [MarshalAs(UnmanagedType.LPStr)] string mid, IntPtr ptr);
public delegate void RtcStateChangeCallbackFunc(int pc, RtcState state, IntPtr ptr);
public delegate void RtcGatheringStateCallbackFunc(int pc, RtcGatheringState satte, IntPtr ptr);
public delegate void RtcDataChannelCallbackFunc(int pc, int dc, IntPtr ptr);
public delegate void RtcTrackCallbackFunc(int pc, int tr, IntPtr ptr);
public delegate void RtcOpenCallbackFunc(int id, IntPtr ptr);
public delegate void RtcClosedCallbackFunc(int id, IntPtr ptr);
public delegate void RtcErrorCallbackFunc(int id, [MarshalAs(UnmanagedType.LPStr)] string error, IntPtr ptr);
public delegate void RtcMessageCallbackFunc(int id, IntPtr meesage, int size, IntPtr ptr);
public delegate void RtcBufferedAmountLowCallbackFunc(int id, IntPtr ptr);
public delegate void RtcAvailableCallbackFunc(int id, IntPtr ptr);

public static class DataChannelPlugin
{
    public const string DLL_NAME = "DataChannelUnity";

    // Log
    [DllImport(DLL_NAME)]
    public static extern void unity_rtcInitLogger(RtcLogLevel level, RtcLogCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern void unity_rtcSetUserPointer(int id, IntPtr ptr);

    // PeerConnection
    [DllImport(DLL_NAME)]
    public static extern int unity_rtcCreatePeerConnection(string[] ice_servers, int ice_servers_count);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcDeletePeerConnection(int pc);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetLocalDescriptionCallback(int pc, RtcDescriptionCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetLocalCandidateCallback(int pc, RtcCandidateCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetStateChangeCallback(int pc, RtcStateChangeCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetGatheringStateChangeCallback(int pc, RtcGatheringStateCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetLocalDescription(int pc, string type);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetRemoteDescription(int pc, string sdp, string type);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcAddRemoteCandidate(int pc, string cand, string mid);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcGetLocalDescription(int pc, IntPtr buffer, int size);

    // DataChannel
    [DllImport(DLL_NAME)]
    public static extern int unity_rtcAddDataChannel(int pc, string label);

    // DataChannel
    [DllImport(DLL_NAME)]
    public static extern int unity_rtcAddDataChannelEx(int pc, string label, IntPtr init);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcDeleteDataChannel(int dc);

    // Track
    [DllImport(DLL_NAME)]
    public static extern int unity_rtcDeleteTrack(int tr);

    // Media
    [DllImport(DLL_NAME)]
    public static extern int unity_rtcAddTrackEx(int pc, int codec, int payloadType, int ssrc, string _mid,
                                                 int direction, string _name, string _msid, string _trackID);

    // DataChannel, Track, and WebSocket common API
    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetOpenCallback(int id, RtcOpenCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetClosedCallback(int id, RtcClosedCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetErrorCallback(int id, RtcErrorCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetMessageCallback(int id, RtcMessageCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSendMessage(int id, IntPtr data, int size);

    [DllImport(DLL_NAME)]
    public static extern void unity_rtcCleanup();
}
