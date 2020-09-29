using UnityEngine;

namespace Rtc
{
    public class DataChannel
    {
        public int Id { get; private set; }
        public DataChannel(int id)
        {
            Id = id;
        }

        ~DataChannel()
        {
            Debug.Log("unity_rtcDeleteDataChannel: " + DataChannelPlugin.unity_rtcDeleteDataChannel(Id));
        }
    }
}