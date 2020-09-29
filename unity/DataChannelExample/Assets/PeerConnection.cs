using System.Collections;
using System.Collections.Generic;
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
        }

        ~PeerConnection()
        {
            Plugin.unity_rtcDeletePeerConnection(Id);
        }
    }
}
