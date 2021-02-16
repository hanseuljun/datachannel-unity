using System;

namespace Rtc
{
    public class DataChannelInit
    {
        public IntPtr Ptr { get; private set; }

        public DataChannelInit()
        {
            Ptr = DataChannelPluginEx.create_data_channel_init();
        }

        ~DataChannelInit()
        {
            DataChannelPluginEx.delete_data_channel_init(Ptr);
        }

        public Reliability GetReliability()
        {
            var reliability = new Reliability();
            DataChannelPluginEx.data_channel_init_get_reliability(Ptr, reliability.Ptr);
            return reliability;
        }

        public void SetReliability(Reliability reliability)
        {
            DataChannelPluginEx.data_channel_init_set_reliability(Ptr, reliability.Ptr);
        }
    }
}