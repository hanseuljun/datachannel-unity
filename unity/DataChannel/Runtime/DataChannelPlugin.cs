using AOT;
using System;
using System.Runtime.InteropServices;

namespace Rtc
{
    public static class DataChannelPluginUtils
    {
        public static RtcLogCallbackFunc logCallback;
        public static void InitLogger(RtcLogLevel level)
        {
            logCallback = new RtcLogCallbackFunc(OnLog);
            DataChannelPlugin.unity_rtcInitLogger(level, logCallback);
        }

        public static void Cleanup()
        {
            // unity_rtcCleanup should come first, otherwise callbacks of cleaned up bridges can be called.
            DataChannelPlugin.unity_rtcCleanup();

            logCallback = null;
            PeerConnectionCallbackBridge.Cleanup();
            DataChannelCallbackBridge.Cleanup();
        }

        [MonoPInvokeCallback(typeof(RtcLogCallbackFunc))]
        public static void OnLog(RtcLogLevel level, string message)
        {
            UnityEngine.Debug.Log($"[{level}] {message}");
        }
    }

    // A static class to detour Unity not supporting providing instance methods as callbacks to native code.
    // NotSupportedException: IL2CPP does not support marshaling delegates that point to instance methods to native code.
    public static class PeerConnectionCallbackBridge
    {
        public static PeerConnection instance1;
        public static RtcDescriptionCallbackFunc localDescriptionCallback1;
        public static RtcCandidateCallbackFunc localCandidateCallback1;
        public static RtcStateChangeCallbackFunc stateChangeCallback1;
        public static RtcGatheringStateCallbackFunc gatheringStateCallback1;

        public static void SetInstance1(PeerConnection instance)
        {
            if (instance1 != null)
                throw new Exception("There is already instance1 in PeerConnectionCallbackBridge.");

            instance1 = instance;

            localDescriptionCallback1 = new RtcDescriptionCallbackFunc(OnLocalDescription1);
            if (DataChannelPlugin.unity_rtcSetLocalDescriptionCallback(instance1.Id, localDescriptionCallback1) < 0)
                throw new Exception("Error from unity_rtcSetLocalDescriptionCallback.");

            localCandidateCallback1 = new RtcCandidateCallbackFunc(OnLocalCandidate1);
            if (DataChannelPlugin.unity_rtcSetLocalCandidateCallback(instance1.Id, localCandidateCallback1) < 0)
                throw new Exception("Error from unity_rtcSetLocalCandidateCallback.");

            stateChangeCallback1 = new RtcStateChangeCallbackFunc(OnStateChange1);
            if (DataChannelPlugin.unity_rtcSetStateChangeCallback(instance1.Id, stateChangeCallback1) < 0)
                throw new Exception("Error from unity_rtcSetStateChangeCallback.");

            gatheringStateCallback1 = new RtcGatheringStateCallbackFunc(OnGatheringStateChange1);
            if (DataChannelPlugin.unity_rtcSetGatheringStateChangeCallback(instance1.Id, gatheringStateCallback1) < 0)
                throw new Exception("Error from unity_rtcSetGatheringStateChangeCallback.");
        }

        public static void Cleanup()
        {
            instance1 = null;
            localDescriptionCallback1 = null;
            localCandidateCallback1 = null;
            stateChangeCallback1 = null;
            gatheringStateCallback1 = null;
        }

        [MonoPInvokeCallback(typeof(RtcDescriptionCallbackFunc))]
        public static void OnLocalDescription1(string sdp, string type, IntPtr ptr)
        {
            instance1.OnLocalDescription(sdp, type, ptr);
        }

        [MonoPInvokeCallback(typeof(RtcCandidateCallbackFunc))]
        public static void OnLocalCandidate1(string cand, string mid, IntPtr ptr)
        {
            instance1.OnLocalCandidate(cand, mid, ptr);
        }

        [MonoPInvokeCallback(typeof(RtcStateChangeCallbackFunc))]
        public static void OnStateChange1(RtcState state, IntPtr ptr)
        {
            instance1.OnStateChange(state, ptr);
        }

        [MonoPInvokeCallback(typeof(RtcGatheringStateCallbackFunc))]
        public static void OnGatheringStateChange1(RtcGatheringState state, IntPtr ptr)
        {
            instance1.OnGatheringStateChange(state, ptr);
        }
    }

    // A static class to detour Unity not supporting providing instance methods as callbacks to native code.
    // NotSupportedException: IL2CPP does not support marshaling delegates that point to instance methods to native code.
    public static class DataChannelCallbackBridge
    {
        private static DataChannel instance1;
        public static RtcOpenCallbackFunc openCallback1;
        public static RtcClosedCallbackFunc closedCallback1;
        public static RtcErrorCallbackFunc errorCallback1;
        public static RtcMessageCallbackFunc messageCallback1;

        public static void SetInstance1(DataChannel instance)
        {
            if (instance1 != null)
                throw new Exception("There is already instance1 in DataChannelCallbackBridge.");

            instance1 = instance;

            openCallback1 = new RtcOpenCallbackFunc(OnOpen1);
            if (DataChannelPlugin.unity_rtcSetOpenCallback(instance.Id, openCallback1) < 0)
                throw new Exception("Error from unity_rtcSetOpenCallback.");

            closedCallback1 = new RtcClosedCallbackFunc(OnClosed1);
            if (DataChannelPlugin.unity_rtcSetClosedCallback(instance.Id, closedCallback1) < 0)
                throw new Exception("Error from unity_rtcSetClosedCallback.");

            errorCallback1 = new RtcErrorCallbackFunc(OnError1);
            if (DataChannelPlugin.unity_rtcSetErrorCallback(instance.Id, errorCallback1) < 0)
                throw new Exception("Error from unity_rtcSetErrorCallback.");

            messageCallback1 = new RtcMessageCallbackFunc(OnMessage1);
            if (DataChannelPlugin.unity_rtcSetMessageCallback(instance.Id, messageCallback1) < 0)
                throw new Exception("Error from unity_rtcSetMessageCallback.");
        }

        public static void Cleanup()
        {
            instance1 = null;
            openCallback1 = null;
            closedCallback1 = null;
            errorCallback1 = null;
            messageCallback1 = null;
        }

        [MonoPInvokeCallback(typeof(RtcOpenCallbackFunc))]
        public static void OnOpen1(IntPtr ptr)
        {
            instance1.OnOpen(ptr);
        }

        [MonoPInvokeCallback(typeof(RtcClosedCallbackFunc))]
        public static void OnClosed1(IntPtr ptr)
        {
            instance1.OnClosed(ptr);
        }

        [MonoPInvokeCallback(typeof(RtcErrorCallbackFunc))]
        public static void OnError1(string error, IntPtr ptr)
        {
            instance1.OnError(error, ptr);
        }

        [MonoPInvokeCallback(typeof(RtcMessageCallbackFunc))]
        public static void OnMessage1(IntPtr meesage, int size, IntPtr ptr)
        {
            instance1.OnMessage(meesage, size, ptr);
        }
    }
}

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

public static class DataChannelPlugin
{
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
    public static extern int unity_rtcSetLocalDescriptionCallback(int pc, RtcDescriptionCallbackFunc cb);

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

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcGetLocalDescriptionSdp(int pc, IntPtr buffer, int size);

    // DataChannel
    [DllImport(DLL_NAME)]
    public static extern int unity_rtcAddDataChannel(int pc, string label);

    [DllImport(DLL_NAME)]
    public static extern int unity_rtcDeleteDataChannel(int dc);

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
