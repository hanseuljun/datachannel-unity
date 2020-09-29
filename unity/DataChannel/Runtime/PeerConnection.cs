using System;

namespace Rtc
{
    public class LocalDescription
    {
        public string sdp;
        public string type;

        public LocalDescription(string sdp, string type)
        {
            this.sdp = sdp;
            this.type = type;
        }
    }

    public class LocalCandidate
    {
        public string cand;
        public string mid;

        public LocalCandidate(string cand, string mid)
        {
            this.cand = cand;
            this.mid = mid;
        }
    }

    public class PeerConnection
    {
        public Action<LocalDescription> LocalDescriptionCreated { get; set; }
        public Action<LocalCandidate> LocalCandidateCreated { get; set; }
        public Action<RtcState> StateChanged { get; set; }
        public Action<RtcGatheringState> GatheringStateChanged { get; set; }
        public int Id { get; private set; }
        // PeerConnection needs to hold these delegates as the native plugin libdatachannel
        // expects the function pointers from these to live stay alive when calling them as callbacks.
        private RtcDescriptionCallbackFunc descriptionCallback;
        private RtcCandidateCallbackFunc candidateCallback;
        private RtcStateChangeCallbackFunc stateChangeCallback;
        private RtcGatheringStateCallbackFunc gatheringStateCallback;

        public PeerConnection()
        {
            string[] iceServers = new string[] { "stun:stun.l.google.com:19302" };
            Id = DataChannelPlugin.unity_rtcCreatePeerConnection(iceServers, iceServers.Length);

            descriptionCallback = new RtcDescriptionCallbackFunc(OnLocalDescription);
            if (DataChannelPlugin.unity_rtcSetLocalDescriptionCallback(Id, descriptionCallback) < 0)
                throw new Exception("Error from unity_rtcSetLocalDescriptionCallback.");

            candidateCallback = new RtcCandidateCallbackFunc(OnLocalCandidate);
            if (DataChannelPlugin.unity_rtcSetLocalCandidateCallback(Id, candidateCallback) < 0)
                throw new Exception("Error from unity_rtcSetLocalCandidateCallback.");

            stateChangeCallback = new RtcStateChangeCallbackFunc(OnStateChange);
            if (DataChannelPlugin.unity_rtcSetStateChangeCallback(Id, stateChangeCallback) < 0)
                throw new Exception("Error from unity_rtcSetStateChangeCallback.");

            gatheringStateCallback = new RtcGatheringStateCallbackFunc(OnGatheringStateChange);
            if (DataChannelPlugin.unity_rtcSetGatheringStateChangeCallback(Id, gatheringStateCallback) < 0)
                throw new Exception("Error from unity_rtcSetGatheringStateChangeCallback.");
        }

        ~PeerConnection()
        {
            DataChannelPlugin.unity_rtcDeletePeerConnection(Id);
        }

        public void SetLocalDescription()
        {
            if (DataChannelPlugin.unity_rtcSetLocalDescription(Id) < 0)
                throw new Exception("Error from unity_rtcSetLocalDescription.");
        }

        public void SetRemoteDescription(string sdp, string type)
        {
            if (DataChannelPlugin.unity_rtcSetRemoteDescription(Id, sdp, type) < 0)
                throw new Exception("Error from unity_rtcSetRemoteDescription.");
        }

        public void AddRemoteCandidate(string cand, string mid)
        {
            if (DataChannelPlugin.unity_rtcAddRemoteCandidate(Id, cand, mid) < 0)
                throw new Exception("Error from unity_rtcAddRemoteCandidate.");
        }

        public DataChannel AddDataChannel(string label)
        {
            int dc = DataChannelPlugin.unity_rtcAddDataChannel(Id, label);
            if (dc < 0)
                throw new Exception("Error from unity_rtcAddDataChannel.");

            return new DataChannel(dc);
        }

        private void OnLocalDescription(string sdp, string type, IntPtr ptr)
        {
            LocalDescriptionCreated?.Invoke(new LocalDescription(sdp, type));
        }

        private void OnLocalCandidate(string cand, string mid, IntPtr ptr)
        {
            LocalCandidateCreated?.Invoke(new LocalCandidate(cand, mid));
        }

        private void OnStateChange(RtcState state, IntPtr ptr)
        {
            StateChanged?.Invoke(state);
        }

        private void OnGatheringStateChange(RtcGatheringState state, IntPtr ptr)
        {
            GatheringStateChanged?.Invoke(state);
        }

    }
}
