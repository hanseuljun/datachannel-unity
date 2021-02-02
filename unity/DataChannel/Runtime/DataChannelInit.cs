using System;

namespace Rtc
{
    public class DataChannelInit
    {
        public IntPtr Ptr { get; private set; }

        public DataChannelInit()
        {
            Ptr = DataChannelPlugin.create_data_channel_init();
        }

        ~DataChannelInit()
        {
            DataChannelPlugin.delete_data_channel_init(Ptr);
        }

        public Reliability GetReliability()
        {
            var reliability = new Reliability();
            DataChannelPlugin.data_channel_init_get_reliability(Ptr, reliability.Ptr);
            return reliability;
        }

        public void SetReliability(Reliability reliability)
        {
            DataChannelPlugin.data_channel_init_set_reliability(Ptr, reliability.Ptr);
        }
    }
}