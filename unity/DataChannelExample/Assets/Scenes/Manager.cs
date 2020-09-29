using UnityEngine;
using Rtc;

public class Manager : MonoBehaviour
{
    private PeerConnection peerConnection;
    private DataChannel dataChannel;
    void Start()
    {
        peerConnection = new PeerConnection();
        print("peerConnection ID: " + peerConnection.Id);
        dataChannel = peerConnection.AddDataChannel("example");
        peerConnection.SetLocalDescription();
        peerConnection.LocalDescriptionCreated += OnLocalDescription;
        peerConnection.LocalCandidateCreated += OnLocalCandidate;
    }

    void OnApplicationQuit()
    {
        Debug.Log("Dispose all");
        peerConnection.Dispose();
        peerConnection = null;
        dataChannel.Dispose();
        dataChannel = null;
    }

    void OnLocalDescription(LocalDescription description)
    {
        print($"type: {description.type}, sdp: {description.sdp}");
    }

    void OnLocalCandidate(LocalCandidate candidate)
    {
        print($"cand: {candidate.cand}, mid: {candidate.mid}");
    }
}
