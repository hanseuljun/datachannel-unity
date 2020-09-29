using System;
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
        public int Id { get; private set; }

        public PeerConnection()
        {
            string[] iceServers = new string[] { "stun:stun.l.google.com:19302" };
            Id = DataChannelPlugin.unity_rtcCreatePeerConnection(iceServers, iceServers.Length);
            Debug.Log("unity_rtcSetLocalDescriptionCallback: " + DataChannelPlugin.unity_rtcSetLocalDescriptionCallback(Id, OnLocalDescription));
            Debug.Log("unity_rtcSetLocalCandidateCallback: " + DataChannelPlugin.unity_rtcSetLocalCandidateCallback(Id, OnLocalCandidate));
        }

        ~PeerConnection()
        {
            Debug.Log("unity_rtcDeletePeerConnection: " + DataChannelPlugin.unity_rtcDeletePeerConnection(Id));
        }

        public void SetLocalDescription()
        {
            Debug.Log("unity_rtcSetLocalDescription: " + DataChannelPlugin.unity_rtcSetLocalDescription(Id));
        }

        public DataChannel AddDataChannel(string label)
        {
            int dc = DataChannelPlugin.unity_rtcAddDataChannel(Id, label);
            Debug.Log("unity_rtcAddDataChannel: " + dc);
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
    }
}
