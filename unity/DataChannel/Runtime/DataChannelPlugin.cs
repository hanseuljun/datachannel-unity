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

public static class DataChannelPlugin
{
    public delegate void RtcLogCallbackFunc(RtcLogLevel level, [MarshalAs(UnmanagedType.LPStr)] string message);
    public delegate void RtcDescriptionCallbackFunc([MarshalAs(UnmanagedType.LPStr)] string sdp, [MarshalAs(UnmanagedType.LPStr)] string type, IntPtr ptr);
    public delegate void RtcCandidateCallbackFunc([MarshalAs(UnmanagedType.LPStr)] string cand, [MarshalAs(UnmanagedType.LPStr)] string mid, IntPtr ptr);
    public delegate void RtcStateChangeCallbackFunc(RtcState state, IntPtr ptr);
    public delegate void RtcGatheringStateCallbackFunc(RtcGatheringState satte, IntPtr ptr);
    public delegate void RtcDataChannelCallbackFunc(int dc, IntPtr ptr);
    public delegate void RtcTrackCallbackFunc(int tr, IntPtr ptr);
    public delegate void RtcOpenCallbackFunc(IntPtr ptr);
    public delegate void RtcClosedCallbackFunc(IntPtr ptr);
    public delegate void RtcErrorCallbackFunc([MarshalAs(UnmanagedType.LPStr)] string error, IntPtr ptr);
    public delegate void RtcMessageCallbackFunc(IntPtr meesage, int size, IntPtr ptr);
    public delegate void RtcBufferedAmountLowCallbackFunc(IntPtr ptr);
    public delegate void RtcAvailableCallbackFunc(IntPtr ptr);

    private const string DLL_NAME = "DataChannelUnity";

    // Log
    [DllImport(DLL_NAME)]
    public static extern void unity_rtcInitLogger(RtcLogLevel level, RtcLogCallbackFunc cb);

    // PeerConnection
    [DllImport(DLL_NAME)]
    public static extern int unity_rtcCreatePeerConnection(string[] ice_servers, int ice_servers_count);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcDeletePeerConnection(int pc);

    [DllImport(DLL_NAME)]
    //public static extern int unity_rtcSetLocalDescriptionCallback(int pc, RtcDescriptionCallbackFunc cb);
    public static extern int unity_rtcSetLocalDescriptionCallback(int pc, IntPtr cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetLocalCandidateCallback(int pc, RtcCandidateCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetStateChangeCallback(int pc, RtcStateChangeCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetGatheringStateChangeCallback(int pc, RtcGatheringStateCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetLocalDescription(int pc);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetRemoteDescription(int pc, string sdp, string type);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcAddRemoteCandidate(int pc, string cand, string mid);

    // DataChannel
    [DllImport(DLL_NAME)]
    public static extern int unity_rtcAddDataChannel(int pc, string label);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcDeleteDataChannel(int dc);

    // DataChannel, Track, and WebSocket common API
    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetOpenCallback(int id, RtcOpenCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSetMessageCallback(int id, RtcMessageCallbackFunc cb);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcSendMessage(int id, IntPtr data, int size);
}
