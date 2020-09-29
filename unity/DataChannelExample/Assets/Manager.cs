using UnityEngine;
using Rtc;

public class Manager : MonoBehaviour
{
    void Start()
    {
        var peerConnection = new PeerConnection();
        print("peerConnection ID: " + peerConnection.Id);
    }

    void Update()
    {
        
    }
}
