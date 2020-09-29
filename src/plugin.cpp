#include <string>
#include <vector>
#include <rtc/rtc.h>
#include "IUnityInterface.h"

extern "C"
{
    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcCreatePeerConnection(const char** ice_servers, int ice_servers_count)
    {
        rtcConfiguration config;
        config.iceServers = ice_servers;
        config.iceServersCount = ice_servers_count;

        return rtcCreatePeerConnection(&config);
    }

    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcDeletePeerConnection(int pc)
    {
        return rtcDeletePeerConnection(pc);
    }
}
