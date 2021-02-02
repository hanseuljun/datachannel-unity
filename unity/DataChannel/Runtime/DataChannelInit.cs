using System;

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

    public bool GetReliabilityUnordered()
    {
        return DataChannelPlugin.data_channel_init_get_reliability_unordered(Ptr);
    }

    public void SetReliabilityUnordered(bool unordered)
    {
        DataChannelPlugin.data_channel_init_set_reliability_unordered(Ptr, unordered);
    }

    public bool GetReliabilityUnreliable()
    {
        return DataChannelPlugin.data_channel_init_get_reliability_unreliable(Ptr);
    }

    public void SetReliabilityUnreliable(bool unreliable)
    {
        DataChannelPlugin.data_channel_init_set_reliability_unreliable(Ptr, unreliable);
    }
}
