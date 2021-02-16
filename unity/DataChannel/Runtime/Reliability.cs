using System;

namespace Rtc
{
    public class Reliability
    {
        public IntPtr Ptr { get; private set; }

        public Reliability()
        {
            Ptr = DataChannelPluginEx.create_reliability();
        }

        ~Reliability()
        {
            DataChannelPluginEx.delete_reliability(Ptr);
        }

        public bool GetUnordered()
        {
            return DataChannelPluginEx.reliability_get_unordered(Ptr);
        }

        public void SetUnordered(bool unordered)
        {
            DataChannelPluginEx.reliability_set_unordered(Ptr, unordered);
        }

        public bool GetUnreliable()
        {
            return DataChannelPluginEx.reliability_get_unreliable(Ptr);
        }

        public void SetUnreliable(bool unreliable)
        {
            DataChannelPluginEx.reliability_set_unreliable(Ptr, unreliable);
        }
    }
}