using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

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
        public event Action<LocalDescription> LocalDescriptionCreated;
        public event Action<LocalCandidate> LocalCandidateCreated;
        public event Action<RtcState> StateChanged;
        public event Action<RtcGatheringState> GatheringStateChanged;
        public int Id { get; private set; }

        public PeerConnection()
        {
            string[] iceServers = new string[] { "stun:stun.l.google.com:19302" };
            Id = DataChannelPlugin.unity_rtcCreatePeerConnection(iceServers, iceServers.Length);
            
            if (DataChannelPlugin.unity_rtcSetLocalDescriptionCallback(Id, OnLocalDescription) < 0)
                throw new Exception("Error from unity_rtcSetLocalDescriptionCallback.");

            if (DataChannelPlugin.unity_rtcSetLocalCandidateCallback(Id, OnLocalCandidate) < 0)
                throw new Exception("Error from unity_rtcSetLocalCandidateCallback.");

            if (DataChannelPlugin.unity_rtcSetStateChangeCallback(Id, OnStateChange) < 0)
                throw new Exception("Error from unity_rtcSetStateChangeCallback.");

            if (DataChannelPlugin.unity_rtcSetGatheringStateChangeCallback(Id, OnGatheringStateChange) < 0)
                throw new Exception("Error from unity_rtcSetGatheringStateChangeCallback.");
        }

        ~PeerConnection()
        {
            Debug.Log("unity_rtcDeletePeerConnection: " + DataChannelPlugin.unity_rtcDeletePeerConnection(Id));
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
            Debug.Log($"LocalDescription - sdp: {sdp}, type: {type}");
            LocalDescriptionCreated(new LocalDescription(sdp, type));
        }

        private void OnLocalCandidate(string cand, string mid, IntPtr ptr)
        {
            Debug.Log($"OnLocalCandidate - cand: {cand}, type: {mid}");
            LocalCandidateCreated(new LocalCandidate(cand, mid));
        }

        private void OnStateChange(RtcState state, IntPtr ptr)
        {
            Debug.Log($"OnStateChange - state: {state}");
            StateChanged(state);
        }

        private void OnGatheringStateChange(RtcGatheringState state, IntPtr ptr)
        {
            Debug.Log($"OnGatheringStateChange - state: {state}");
            GatheringStateChanged(state);

        }
    }
}
