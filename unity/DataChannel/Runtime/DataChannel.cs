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

            if (DataChannelPlugin.unity_rtcSetMessageCallback(Id, OnMessage) < 0)
                throw new Exception("Error from unity_rtcSetMessageCallback.");

            if (DataChannelPlugin.unity_rtcSetOpenCallback(Id, OnOpen) < 0)
                throw new Exception("Error from unity_rtcSetOpenCallback.");
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