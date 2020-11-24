using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;

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
        private static Dictionary<int, PeerConnection> instances;

        public static void SetInstance(PeerConnection instance)
        {
            if (instances == null)
                instances = new Dictionary<int, PeerConnection>();

            instances[instance.Id] = instance;

            if (DataChannelPlugin.unity_rtcSetLocalDescriptionCallback(instance.Id, OnLocalDescription) < 0)
                throw new Exception("Error from unity_rtcSetLocalDescriptionCallback.");

            if (DataChannelPlugin.unity_rtcSetLocalCandidateCallback(instance.Id, OnLocalCandidate) < 0)
                throw new Exception("Error from unity_rtcSetLocalCandidateCallback.");

            if (DataChannelPlugin.unity_rtcSetStateChangeCallback(instance.Id, OnStateChange) < 0)
                throw new Exception("Error from unity_rtcSetStateChangeCallback.");

            if (DataChannelPlugin.unity_rtcSetGatheringStateChangeCallback(instance.Id, OnGatheringStateChange) < 0)
                throw new Exception("Error from unity_rtcSetGatheringStateChangeCallback.");
        }

        public static void Cleanup()
        {
            instances = null;
        }

        [MonoPInvokeCallback(typeof(RtcDescriptionCallbackFunc))]
        public static void OnLocalDescription(int pc, string sdp, string type, IntPtr ptr)
        {
            instances?[pc].OnLocalDescription(sdp, type);
        }

        [MonoPInvokeCallback(typeof(RtcCandidateCallbackFunc))]
        public static void OnLocalCandidate(int pc, string cand, string mid, IntPtr ptr)
        {
            instances?[pc].OnLocalCandidate(cand, mid);
        }

        [MonoPInvokeCallback(typeof(RtcStateChangeCallbackFunc))]
        public static void OnStateChange(int pc, RtcState state, IntPtr ptr)
        {
            instances?[pc].OnStateChange(state);
        }

        [MonoPInvokeCallback(typeof(RtcGatheringStateCallbackFunc))]
        public static void OnGatheringStateChange(int pc, RtcGatheringState state, IntPtr ptr)
        {
            instances?[pc].OnGatheringStateChange(state);
        }
    }

    // A static class to detour Unity not supporting providing instance methods as callbacks to native code.
    // NotSupportedException: IL2CPP does not support marshaling delegates that point to instance methods to native code.
    public static class DataChannelCallbackBridge
    {
        private static Dictionary<int, DataChannel> instances;

        public static void SetInstance(DataChannel instance)
        {
            if (instances == null)
                instances = new Dictionary<int, DataChannel>();

            instances[instance.Id] = instance;

            if (DataChannelPlugin.unity_rtcSetOpenCallback(instance.Id, OnOpen) < 0)
                throw new Exception("Error from unity_rtcSetOpenCallback.");

            if (DataChannelPlugin.unity_rtcSetClosedCallback(instance.Id, OnClosed) < 0)
                throw new Exception("Error from unity_rtcSetClosedCallback.");

            if (DataChannelPlugin.unity_rtcSetErrorCallback(instance.Id, OnError) < 0)
                throw new Exception("Error from unity_rtcSetErrorCallback.");

            if (DataChannelPlugin.unity_rtcSetMessageCallback(instance.Id, OnMessage) < 0)
                throw new Exception("Error from unity_rtcSetMessageCallback.");
        }

        public static void Cleanup()
        {
            instances = null;
        }

        [MonoPInvokeCallback(typeof(RtcOpenCallbackFunc))]
        public static void OnOpen(int id, IntPtr ptr)
        {
            instances?[id].OnOpen(ptr);
        }

        [MonoPInvokeCallback(typeof(RtcClosedCallbackFunc))]
        public static void OnClosed(int id, IntPtr ptr)
        {
            instances?[id].OnClosed(ptr);
        }

        [MonoPInvokeCallback(typeof(RtcErrorCallbackFunc))]
        public static void OnError(int id, string error, IntPtr ptr)
        {
            instances?[id].OnError(error, ptr);
        }

        [MonoPInvokeCallback(typeof(RtcMessageCallbackFunc))]
        public static void OnMessage(int id, IntPtr meesage, int size, IntPtr ptr)
        {
            instances?[id].OnMessage(meesage, size, ptr);
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
    private const string DLL_NAME = "DataChannelUnity";

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
