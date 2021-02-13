using UnityEngine;
using Rtc;

public class CopyPasteOfferer : MonoBehaviour
{
    private PeerConnection peerConnection;
    private DataChannel dataChannel;

    void Start()
    {
        DataChannelPluginUtils.InitLogger(RtcLogLevel.RTC_LOG_DEBUG);
        // See https://github.com/paullouisageneau/libdatachannel/issues/275
        // for how to let libdatachannel know about the ICE servers.
        string[] iceServers = new string[] { "stun.l.google.com:19302" };
        peerConnection = new PeerConnection(iceServers);
        peerConnection.LocalDescriptionCreated = OnLocalDescription;
        peerConnection.LocalCandidateCreated = OnLocalCandidate;
        dataChannel = peerConnection.AddDataChannel("datachannelexample");
        peerConnection.SetLocalDescription("offer");
    }

    void OnLocalDescription(Description description)
    {
        print("description:\n" + description.sdp);
    }

    void OnLocalCandidate(Candidate candidate)
    {
        print("candidate:\n" + candidate.cand);
    }
}
