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
        peerConnection.LocalDescriptionCreated = OnLocalDescription;
        peerConnection.LocalCandidateCreated = OnLocalCandidate;
    }

    void OnApplicationQuit()
    {
        DataChannelPlugin.unity_rtcCleanup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            peerConnection.SetLocalDescription();
        }
    }

    void OnLocalDescription(Description description)
    {
        print($"type: {description.type}, sdp: {description.sdp}");
    }

    void OnLocalCandidate(Candidate candidate)
    {
        print($"cand: {candidate.cand}, mid: {candidate.mid}");
    }
}
