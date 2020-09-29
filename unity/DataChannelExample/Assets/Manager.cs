using System;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    void Start()
    {
        string[] iceServers = new string[] { "stun:stun.l.google.com:19302" };
        int pc = Plugin.unity_rtcCreatePeerConnection(iceServers, iceServers.Length);
        print("pc: " + pc);
        print("delete: " + Plugin.unity_rtcDeletePeerConnection(pc));
    }

    void Update()
    {
        
    }
}
