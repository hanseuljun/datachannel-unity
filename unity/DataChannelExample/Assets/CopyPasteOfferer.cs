using System;
using UnityEngine;
using Rtc;

public class CopyPasteOfferer : MonoBehaviour
{
    private PeerConnection peerConnection;
    private DataChannel dataChannel;
    private string localDescriptionStr;
    private string localCandidateStr;
    private string remoteDescriptionStr;

    void Start()
    {
        peerConnection = null;
        dataChannel = null;
        localDescriptionStr = "";
        localCandidateStr = "";
        remoteDescriptionStr = "";

        DataChannelPluginUtils.InitLogger(RtcLogLevel.RTC_LOG_DEBUG);
        // See https://github.com/paullouisageneau/libdatachannel/issues/275
        // for how to let libdatachannel know about the ICE servers.
        string[] iceServers = new string[] { "stun.l.google.com:19302" };
        peerConnection = new PeerConnection(iceServers);
        //peerConnection.SetUserPointer(new IntPtr(123));
        peerConnection.LocalDescriptionCreated = OnLocalDescription;
        peerConnection.LocalCandidateCreated = OnLocalCandidate;
        peerConnection.StateChanged = OnState;
        peerConnection.GatheringStateChanged = OnGatheringState;
        dataChannel = peerConnection.CreateDataChannel("datachannelexample");
        //peerConnection.SetLocalDescription("offer");
        //print($"dataChannel.Id: {dataChannel.Id}");
        //print($"peerConnection.GetLocalDescriptionSdp(): {peerConnection.GetLocalDescriptionSdp()}");
    }

    void OnApplicationQuit()
    {
        DataChannelPluginUtils.Cleanup();
    }

    void OnGUI()
    {
        GUI.TextArea(new Rect(10, 10, 400, 400), localDescriptionStr);
        GUI.TextArea(new Rect(10, 420, 400, 200), localCandidateStr);

        GUI.Label(new Rect(420, 10, 200, 20), "Enter Remote Description Here");
        remoteDescriptionStr = GUI.TextArea(new Rect(420, 40, 400, 400), remoteDescriptionStr);
        if (GUI.Button(new Rect(420, 450, 400, 50), "Set Remote Description"))
            peerConnection.SetRemoteDescription(remoteDescriptionStr, "");
    }

    private void OnLocalDescription(Description description)
    {
        localDescriptionStr = "Local Description(Paste this to the other peer):\n" + description.sdp;
    }

    private void OnLocalCandidate(Candidate candidate)
    {
        localCandidateStr += "Local Candidate(Paste this to the other peer after the local description):\n" + candidate.cand + "\n\n";
    }

    private void OnState(RtcState state)
    {
        print("OnState: " + state);
    }

    private void OnGatheringState(RtcGatheringState state)
    {
        print("OnGatheringState: " + state);
    }
}
