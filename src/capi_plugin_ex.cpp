#include "IUnityInterface.h"
#include <rtc/rtc.h>
#include <string>
#include <vector>

extern "C"
{
    UNITY_INTERFACE_EXPORT rtcReliability* UNITY_INTERFACE_API
    create_reliability()
    {
        return new rtcReliability{false, false, 0, 0};
    }

    UNITY_INTERFACE_EXPORT void UNITY_INTERFACE_API
    delete_reliability(rtcReliability* ptr)
    {
        delete ptr;
    }

    UNITY_INTERFACE_EXPORT bool UNITY_INTERFACE_API
    reliability_get_unordered(rtcReliability* ptr)
    {
        return ptr->unordered;
    }

    UNITY_INTERFACE_EXPORT void UNITY_INTERFACE_API
    reliability_set_unordered(rtcReliability* ptr, bool unordered)
    {
        ptr->unordered = unordered;
    }

    UNITY_INTERFACE_EXPORT bool UNITY_INTERFACE_API
    reliability_get_unreliable(rtcReliability* ptr)
    {
        return ptr->unreliable;
    }

    UNITY_INTERFACE_EXPORT void UNITY_INTERFACE_API
    reliability_set_unreliable(rtcReliability* ptr, bool unreliable)
    {
        ptr->unreliable = unreliable;
    }

    UNITY_INTERFACE_EXPORT rtcDataChannelInit* UNITY_INTERFACE_API
    create_data_channel_init()
    {
        rtcReliability reliability{false, false, 0, 0};
        return new rtcDataChannelInit{reliability, "", false, false, 0};
    }

    UNITY_INTERFACE_EXPORT void UNITY_INTERFACE_API
    delete_data_channel_init(rtcDataChannelInit* ptr)
    {
        delete ptr;
    }

    UNITY_INTERFACE_EXPORT void UNITY_INTERFACE_API
    data_channel_init_get_reliability(rtcDataChannelInit* ptr,
                                      rtcReliability* reliability)
    {
        *reliability = ptr->reliability;
    }

    UNITY_INTERFACE_EXPORT void UNITY_INTERFACE_API
    data_channel_init_set_reliability(rtcDataChannelInit* ptr,
                                      rtcReliability* reliability)
    {
        ptr->reliability = *reliability;
    }
}
