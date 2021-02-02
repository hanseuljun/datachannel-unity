using System;

public class DataChannelInit
{
    private IntPtr ptr;

    public DataChannelInit()
    {
        ptr = DataChannelPlugin.create_data_channel_init();
    }

    ~DataChannelInit()
    {
        DataChannelPlugin.delete_data_channel_init(ptr);
    }

    public bool GetReliabilityUnordered()
    {
        return DataChannelPlugin.data_channel_init_get_reliability_unordered(ptr);
    }

    public void SetReliabilityUnordered(bool unordered)
    {
        DataChannelPlugin.data_channel_init_set_reliability_unordered(ptr, unordered);
    }

    public bool GetReliabilityUnreliable()
    {
        return DataChannelPlugin.data_channel_init_get_reliability_unreliable(ptr);
    }

    public void SetReliabilityUnreliable(bool unreliable)
    {
        DataChannelPlugin.data_channel_init_set_reliability_unreliable(ptr, unreliable);
    }
}
