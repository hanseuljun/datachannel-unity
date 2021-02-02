using System;

namespace Rtc
{
    public class Reliability
    {
        public IntPtr Ptr { get; private set; }

        public Reliability()
        {
            Ptr = DataChannelPlugin.create_reliability();
        }

        ~Reliability()
        {
            DataChannelPlugin.delete_reliability(Ptr);
        }

        public bool GetUnordered()
        {
            return DataChannelPlugin.reliability_get_unordered(Ptr);
        }

        public void SetUnordered(bool unordered)
        {
            DataChannelPlugin.reliability_set_unordered(Ptr, unordered);
        }

        public bool GetUnreliable()
        {
            return DataChannelPlugin.reliability_get_unreliable(Ptr);
        }

        public void SetUnreliable(bool unreliable)
        {
            DataChannelPlugin.reliability_set_unreliable(Ptr, unreliable);
        }
    }
}