using UnityEngine;

public class DataChannel
{
    public int Id { get; private set; }
    public DataChannel(int id)
    {
        Id = id;
    }

    ~DataChannel()
    {
        Debug.Log("unity_rtcDeleteDataChannel: " + Plugin.unity_rtcDeleteDataChannel(Id));
    }
}
