using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Rtc
{
    public class Description
    {
        public string sdp;
        public string type;

        public Description(string sdp, string type)
        {
            this.sdp = sdp;
            this.type = type;
        }
    }

    public class Candidate
    {
        public string cand;
        public string mid;

        public Candidate(string cand, string mid)
        {
            this.cand = cand;
            this.mid = mid;
        }
    }

    public class PeerConnection
    {
        public Action<Description> LocalDescriptionCreated { get; set; }
        public Action<Candidate> LocalCandidateCreated { get; set; }
        public Action<RtcState> StateChanged { get; set; }
        public Action<RtcGatheringState> GatheringStateChanged { get; set; }
        public int Id { get; private set; }

        public PeerConnection()
        {
            string[] iceServers = new string[] { "stun:stun.l.google.com:19302" };
            Id = DataChannelPlugin.unity_rtcCreatePeerConnection(iceServers, iceServers.Length);
            PeerConnectionCallbackBridge.SetInstance1(this);
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

        public string GetLocalDescriptionSdp()
        {
            // Assuming 8 KB would be enough for a SDP message.
            int sdpBufferSize = 8 * 1024;
            IntPtr sdpBuffer = Marshal.AllocHGlobal(sdpBufferSize);
            if (DataChannelPlugin.unity_rtcGetLocalDescriptionSdp(Id, sdpBuffer, sdpBufferSize) < 0)
                throw new Exception("Error from unity_rtcGetLocalDescriptionSdp.");
            string sdp = Marshal.PtrToStringAnsi(sdpBuffer);
            Marshal.FreeHGlobal(sdpBuffer);
            return sdp;
        }

        public DataChannel AddDataChannel(string label)
        {
            int dc = DataChannelPlugin.unity_rtcAddDataChannel(Id, label);
            if (dc < 0)
                throw new Exception("Error from unity_rtcAddDataChannel.");

            return new DataChannel(dc);
        }

        public void OnLocalDescription(string sdp, string type, IntPtr ptr)
        {
            LocalDescriptionCreated?.Invoke(new Description(sdp, type));
        }

        public void OnLocalCandidate(string cand, string mid, IntPtr ptr)
        {
            LocalCandidateCreated?.Invoke(new Candidate(cand, mid));
        }

        public void OnStateChange(RtcState state, IntPtr ptr)
        {
            StateChanged?.Invoke(state);
        }

        public void OnGatheringStateChange(RtcGatheringState state, IntPtr ptr)
        {
            GatheringStateChanged?.Invoke(state);
        }
    }
}
