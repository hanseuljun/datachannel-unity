using System;
using UnityEngine;

namespace Rtc
{
    public class PeerConnection
    {
        public int Id { get; private set; }

        public PeerConnection()
        {
            string[] iceServers = new string[] { "stun:stun.l.google.com:19302" };
            Id = Plugin.unity_rtcCreatePeerConnection(iceServers, iceServers.Length);
            Debug.Log("unity_rtcSetLocalDescriptionCallback: " + Plugin.unity_rtcSetLocalDescriptionCallback(Id, OnLocalDescription));
        }

        ~PeerConnection()
        {
            Debug.Log("unity_rtcDeletePeerConnection: " + Plugin.unity_rtcDeletePeerConnection(Id));
        }

        public void SetLocalDescription()
        {
            Debug.Log("unity_rtcSetLocalDescription: " + Plugin.unity_rtcSetLocalDescription(Id));
        }

        public DataChannel AddDataChannel(string label)
        {
            int dc = Plugin.unity_rtcAddDataChannel(Id, label);
            Debug.Log("unity_rtcAddDataChannel: " + dc);
            return new DataChannel(dc);
        }

        private void OnLocalDescription(string sdp, string type, IntPtr ptr)
        {
            Debug.Log($"LocalDescription - sdp: {sdp}, type: {type}");
        }

    }
}
