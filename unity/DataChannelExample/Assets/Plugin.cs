using System.Runtime.InteropServices;

public static class Plugin
{
    private const string DLL_NAME = "DataChannelUnity";

    [DllImport(DLL_NAME)]
    public static extern int test();
}
