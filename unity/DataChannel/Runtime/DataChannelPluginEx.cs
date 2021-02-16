using System;
using System.Runtime.InteropServices;

public static class DataChannelPluginEx
{
    // These enum getter functions are here since the enums don't have a fixed
    // numeric value yet.
    [DllImport(DataChannelPlugin.DLL_NAME)]
    public static extern int get_rtc_codec_h264();

    [DllImport(DataChannelPlugin.DLL_NAME)]
    public static extern int get_rtc_codec_opus();

    [DllImport(DataChannelPlugin.DLL_NAME)]
    public static extern int get_rtc_direction_recvonly();

    // Reliability
    [DllImport(DataChannelPlugin.DLL_NAME)]
    public static extern IntPtr create_reliability();

    [DllImport(DataChannelPlugin.DLL_NAME)]
    public static extern void delete_reliability(IntPtr ptr);

    [DllImport(DataChannelPlugin.DLL_NAME)]
    public static extern bool reliability_get_unordered(IntPtr ptr);

    [DllImport(DataChannelPlugin.DLL_NAME)]
    public static extern void reliability_set_unordered(IntPtr ptr, bool unordered);

    [DllImport(DataChannelPlugin.DLL_NAME)]
    public static extern bool reliability_get_unreliable(IntPtr ptr);

    [DllImport(DataChannelPlugin.DLL_NAME)]
    public static extern void reliability_set_unreliable(IntPtr ptr, bool unreliable);

    // DataChannelInit
    [DllImport(DataChannelPlugin.DLL_NAME)]
    public static extern IntPtr create_data_channel_init();

    [DllImport(DataChannelPlugin.DLL_NAME)]
    public static extern void delete_data_channel_init(IntPtr ptr);

    [DllImport(DataChannelPlugin.DLL_NAME)]
    public static extern void data_channel_init_get_reliability(IntPtr ptr, IntPtr reliability);

    [DllImport(DataChannelPlugin.DLL_NAME)]
    public static extern void data_channel_init_set_reliability(IntPtr ptr, IntPtr reliability);
}