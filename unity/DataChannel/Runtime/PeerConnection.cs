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

    public class PeerConnection : IDisposable
    {
        public Action<Description> LocalDescriptionCreated { get; set; }
        public Action<Candidate> LocalCandidateCreated { get; set; }
        public Action<RtcState> StateChanged { get; set; }
        public Action<RtcGatheringState> GatheringStateChanged { get; set; }
        public int Id { get; private set; }
        private bool disposed;

        public PeerConnection(string[] iceServers)
        {
            disposed = false;
            Id = DataChannelPlugin.unity_rtcCreatePeerConnection(iceServers, iceServers.Length);
            PeerConnectionCallbackBridge.SetInstance(this);
        }

        ~PeerConnection()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!disposed)
                DataChannelPlugin.unity_rtcDeletePeerConnection(Id);

            disposed = true;
        }

        public void SetLocalDescription(string type)
        {
            if (DataChannelPlugin.unity_rtcSetLocalDescription(Id, type) < 0)
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
            int bufferSize = 8 * 1024;
            IntPtr buffer = Marshal.AllocHGlobal(bufferSize);
            if (DataChannelPlugin.unity_rtcGetLocalDescription(Id, buffer, bufferSize) < 0)
                throw new Exception("Error from unity_rtcGetLocalDescriptionSdp.");
            string sdp = Marshal.PtrToStringAnsi(buffer);
            Marshal.FreeHGlobal(buffer);
            return sdp;
        }

        public DataChannel CreateDataChannel(string label)
        {
            int dc = DataChannelPlugin.unity_rtcCreateDataChannel(Id, label);
            if (dc < 0)
                throw new Exception("Error from unity_rtcCreateDataChannel.");

            return new DataChannel(dc);
        }

        public DataChannel CreateDataChannelEx(string label, DataChannelInit init)
        {
            int dc = DataChannelPlugin.unity_rtcCreateDataChannelEx(Id, label, init.Ptr);
            if (dc < 0)
                throw new Exception("Error from unity_rtcCreateDataChannelEx.");

            return new DataChannel(dc);
        }

        public Track AddTrackEx(RtcCodec codec, int payloadType, int ssrc, string mid,
                                RtcDirection direction, string name, string msid, string trackId)
        {
            int tr = DataChannelPlugin.unity_rtcAddTrackEx(Id, codec, payloadType, ssrc, mid, direction, name, msid, trackId);
            if (tr < 0)
                throw new Exception("Error from AddTrackEx.");

            return new Track(tr);
        }

        public void OnLocalDescription(string sdp, string type)
        {
            LocalDescriptionCreated?.Invoke(new Description(sdp, type));
        }

        public void OnLocalCandidate(string cand, string mid)
        {
            LocalCandidateCreated?.Invoke(new Candidate(cand, mid));
        }

        public void OnStateChange(RtcState state)
        {
            StateChanged?.Invoke(state);
        }

        public void OnGatheringStateChange(RtcGatheringState state)
        {
            GatheringStateChanged?.Invoke(state);
        }
    }
}
