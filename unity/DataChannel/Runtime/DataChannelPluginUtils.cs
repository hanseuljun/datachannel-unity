using System;
using System.Collections.Generic;
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
            ChannelCallbackBridge.Cleanup();
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
    public static class ChannelCallbackBridge
    {
        private static Dictionary<int, Channel> instances;

        public static void SetInstance(Channel instance)
        {
            if (instances == null)
                instances = new Dictionary<int, Channel>();

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