using System;

namespace Rtc
{
    public class DataChannel : Channel, IDisposable
    {
        private bool disposed;

        public DataChannel(int id) : base(id)
        {
            disposed = false;
        }

        ~DataChannel()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!disposed)
                DataChannelPlugin.unity_rtcDeleteDataChannel(Id);

            disposed = true;
        }
    }
}