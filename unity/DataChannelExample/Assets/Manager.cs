using UnityEngine;
using Rtc;

public class Manager : MonoBehaviour
{
    private PeerConnection peerConnection;
    void Start()
    {
        peerConnection = new PeerConnection();
        print("peerConnection ID: " + peerConnection.Id);
        peerConnection.AddDataChannel("example");
        peerConnection.SetLocalDescription();
    }
}
