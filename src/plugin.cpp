#include <string>
#include <vector>
#include <rtc/rtc.h>
#include "IUnityInterface.h"

extern "C"
{
    // Log
    // NULL cb to log to stdout
    UNITY_INTERFACE_EXPORT void UNITY_INTERFACE_API unity_rtcInitLogger(rtcLogLevel level, rtcLogCallbackFunc cb)
    {
        rtcInitLogger(level, cb);
    }

    // User pointer
    UNITY_INTERFACE_EXPORT void UNITY_INTERFACE_API unity_rtcSetUserPointer(int id, void* ptr)
    {
        rtcSetUserPointer(id, ptr);
    }

    // PeerConnection
    // returns pc id
    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcCreatePeerConnection(const char** ice_servers, int ice_servers_count)
    {
        rtcConfiguration config;
        config.iceServers = ice_servers;
        config.iceServersCount = ice_servers_count;
        config.portRangeBegin = 0;
        config.portRangeEnd = 0;

        return rtcCreatePeerConnection(&config);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcDeletePeerConnection(int pc)
    {
        return rtcDeletePeerConnection(pc);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetLocalDescriptionCallback(int pc, rtcDescriptionCallbackFunc cb)
    {
        int result{rtcSetLocalDescriptionCallback(pc, cb)};
        return result;
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetLocalCandidateCallback(int pc, rtcCandidateCallbackFunc cb)
    {
        return rtcSetLocalCandidateCallback(pc, cb);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetStateChangeCallback(int pc, rtcStateChangeCallbackFunc cb)
    {
        return rtcSetStateChangeCallback(pc, cb);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetGatheringStateChangeCallback(int pc, rtcGatheringStateCallbackFunc cb)
    {
        return rtcSetGatheringStateChangeCallback(pc, cb);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetLocalDescription(int pc, const char* type)
    {
        return rtcSetLocalDescription(pc, type);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetRemoteDescription(int pc, const char* sdp, const char* type)
    {
        return rtcSetRemoteDescription(pc, sdp, type);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcAddRemoteCandidate(int pc, const char* cand, const char* mid)
    {
        return rtcAddRemoteCandidate(pc, cand, mid);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcGetLocalDescription(int pc, char* buffer, int size)
    {
        return rtcGetLocalDescription(pc, buffer, size);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcGetRemoteDescription(int pc, char* buffer, int size)
    {
        return rtcGetRemoteDescription(pc, buffer, size);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcGetLocalAddress(int pc, char* buffer, int size)
    {
        return rtcGetLocalAddress(pc, buffer, size);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcGetRemoteAddress(int pc, char* buffer, int size)
    {
        return rtcGetRemoteAddress(pc, buffer, size);
    }

    // DataChannel
    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetDataChannelCallback(int pc, rtcDataChannelCallbackFunc cb)
    {
        return rtcSetDataChannelCallback(pc, cb);
    }

    // returns dc id
    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcAddDataChannel(int pc, const char* label)
    {
        return rtcAddDataChannel(pc, label);
    }


    // Equivalent to calling rtcAddDataChannel() and rtcSetLocalDescription()
    // returns dc id
    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcCreateDataChannel(int pc, const char* label)
    {
        return rtcCreateDataChannel(pc, label);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcDeleteDataChannel(int dc)
    {
        return rtcDeleteDataChannel(dc);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcGetDataChannelLabel(int dc, char* buffer, int size)
    {
        return rtcGetDataChannelLabel(dc, buffer, size);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcGetDataChannelProtocol(int dc, char* buffer, int size)
    {
        return rtcGetDataChannelProtocol(dc, buffer, size);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcGetDataChannelReliability(int dc, rtcReliability* reliability)
    {
        return rtcGetDataChannelReliability(dc, reliability);
    }

    // Track
    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetTrackCallback(int pc, rtcTrackCallbackFunc cb)
    {
        return rtcSetTrackCallback(pc, cb);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcAddTrack(int pc, const char* mediaDescriptionSdp)
    {
        return rtcAddTrack(pc, mediaDescriptionSdp);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcDeleteTrack(int tr)
    {
        return rtcDeleteTrack(tr);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcGetTrackDescription(int tr, char* buffer, int size)
    {
        return rtcGetTrackDescription(tr, buffer, size);
    }

    // DataChannel, Track, and WebSocket common API
    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetOpenCallback(int id, rtcOpenCallbackFunc cb)
    {
        return rtcSetOpenCallback(id, cb);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetClosedCallback(int id, rtcClosedCallbackFunc cb)
    {
        return rtcSetClosedCallback(id, cb);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetErrorCallback(int id, rtcErrorCallbackFunc cb)
    {
        return rtcSetErrorCallback(id, cb);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetMessageCallback(int id, rtcMessageCallbackFunc cb)
    {
        return rtcSetMessageCallback(id, cb);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSendMessage(int id, const char* data, int size)
    {
        return rtcSendMessage(id, data, size);
    }

    // total size buffered to send
    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcGetBufferedAmount(int id)
    {
        return rtcGetBufferedAmount(id);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetBufferedAmountLowThreshold(int id, int amount)
    {
        return rtcSetBufferedAmountLowThreshold(id, amount);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetBufferedAmountLowCallback(int id, rtcBufferedAmountLowCallbackFunc cb)
    {
        return rtcSetBufferedAmountLowCallback(id, cb);
    }

    // DataChannel, Track, and WebSocket common extended API
    // total size available to receive
    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcGetAvailableAmount(int id)
    {
        return rtcGetAvailableAmount(id);
    }
    
    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcSetAvailableCallback(int id, rtcAvailableCallbackFunc cb)
    {
        return rtcSetAvailableCallback(id, cb);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcReceiveMessage(int id, char* buffer, int* size)
    {
        return rtcReceiveMessage(id, buffer, size);
    }

    // Optional preload and cleanup
    UNITY_INTERFACE_EXPORT void UNITY_INTERFACE_API unity_rtcPreload()
    {
        return rtcPreload();
    }

    UNITY_INTERFACE_EXPORT void UNITY_INTERFACE_API unity_rtcCleanup()
    {
        return rtcCleanup();
    }
}
