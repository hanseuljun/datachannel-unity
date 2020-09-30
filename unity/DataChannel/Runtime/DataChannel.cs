using System;
using System.Runtime.InteropServices;

namespace Rtc
{

    public class DataChannel
    {
        public Action Opened { get; set; }
        public Action Closed { get; set; }
        public Action<string> ErrorCreated { get; set; }
        public Action<byte[]> MessageReceived { get; set; }

        public int Id { get; private set; }

        public DataChannel(int id)
        {
            Id = id;
            DataChannelCallbackBridge.SetInstance1(this);
        }

        ~DataChannel()
        {
            DataChannelPlugin.unity_rtcDeleteDataChannel(Id);
        }

        public void SendMessage(byte[] message)
        {
            GCHandle handle = GCHandle.Alloc(message, GCHandleType.Pinned);
            IntPtr bytes = handle.AddrOfPinnedObject();
            DataChannelPlugin.unity_rtcSendMessage(Id, bytes, message.Length);
            handle.Free();
        }

        public void OnOpen(IntPtr ptr)
        {
            Opened?.Invoke();
        }

        public void OnClosed(IntPtr ptr)
        {
            Closed?.Invoke();
        }

        public void OnError(string error, IntPtr ptr)
        {
            ErrorCreated?.Invoke(error);
        }

        public void OnMessage(IntPtr meesage, int size, IntPtr ptr)
        {
            byte[] managedMessage = new byte[size];
            Marshal.Copy(meesage, managedMessage, 0, size);
            MessageReceived?.Invoke(managedMessage);
        }
    }
}