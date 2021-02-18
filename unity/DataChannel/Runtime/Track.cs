namespace Rtc
{
    public class Track : Channel
    {
        private bool disposed;

        public Track(int id) : base(id)
        {
            disposed = false;
        }

        ~Track()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!disposed)
                DataChannelPlugin.unity_rtcDeleteTrack(Id);

            disposed = true;
        }
    }
}