#include "IUnityInterface.h"

extern "C"
{
    UNITY_INTERFACE_EXPORT int UNITY_INTERFACE_API test()
    {
        return 123;
    }
}
