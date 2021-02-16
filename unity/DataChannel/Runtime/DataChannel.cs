namespace Rtc
{
    public class DataChannel : Channel
    {
        public DataChannel(int id) : base(id)
        {
        }

        ~DataChannel()
        {
            DataChannelPlugin.unity_rtcDeleteDataChannel(Id);
        }
    }
}