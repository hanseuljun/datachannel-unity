using System;
using System.Runtime.InteropServices;

namespace Rtc
{
    public class Channel
    {
        public Action Opened { get; set; }
        public Action Closed { get; set; }
        public Action<string> ErrorReceived { get; set; }
        public Action<byte[]> MessageReceived { get; set; }

        public int Id { get; private set; }

        public Channel(int id)
        {
            Id = id;
            ChannelCallbackBridge.SetInstance(this);
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
            ErrorReceived?.Invoke(error);
        }

        public void OnMessage(IntPtr meesage, int size, IntPtr ptr)
        {
            byte[] managedMessage = new byte[size];
            Marshal.Copy(meesage, managedMessage, 0, size);
            MessageReceived?.Invoke(managedMessage);
        }
    }
}