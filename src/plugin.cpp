#include <string>
#include <vector>
#include <rtc/rtc.h>
#include "IUnityInterface.h"

extern "C"
{
    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API test(char** input, int input_size)
    {
        int size_sum = 0;
        for (int i = 0; i < input_size; ++i) {
            std::string str(input[i]);
            size_sum += str.size();
        }
        return size_sum;
    }

    //UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcCreatePeerConnection(const char** ice_servers, int ice_servers_count)
    //{
    //    rtcConfiguration config;
    //    config.iceServers = ice_servers;
    //    config.iceServersCount = ice_servers_count;

    //    return rtcCreatePeerConnection(&config);
    //}

    //UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API unity_rtcDeletePeerConnection(int pc)
    //{
    //    return rtcDeletePeerConnection(pc);
    //}
}
