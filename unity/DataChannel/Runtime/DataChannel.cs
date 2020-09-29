using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Rtc
{
    public class DataChannel
    {
        public event Action Opened;
        public event Action<byte[]> MessageReceived;

        public int Id { get; private set; }
        public DataChannel(int id)
        {
            Id = id;
            Debug.Log("unity_rtcSetMessageCallback: " + DataChannelPlugin.unity_rtcSetMessageCallback(Id, OnMessage));
            Debug.Log("unity_rtcSetMessageCallback: " + DataChannelPlugin.unity_rtcSetOpenCallback(Id, OnOpen));
        }

        ~DataChannel()
        {
            Debug.Log("unity_rtcDeleteDataChannel: " + DataChannelPlugin.unity_rtcDeleteDataChannel(Id));
        }

        public void SendMessage(byte[] message)
        {
            GCHandle handle = GCHandle.Alloc(message, GCHandleType.Pinned);
            IntPtr bytes = handle.AddrOfPinnedObject();
            DataChannelPlugin.unity_rtcSendMessage(Id, bytes, message.Length);
            handle.Free();
        }

        private void OnOpen(IntPtr ptr)
        {
            Opened();
        }

        private void OnMessage(IntPtr meesage, int size, IntPtr ptr)
        {
            byte[] managedMessage = new byte[size];
            Marshal.Copy(meesage, managedMessage, 0, size);
            MessageReceived(managedMessage);
        }
    }
}