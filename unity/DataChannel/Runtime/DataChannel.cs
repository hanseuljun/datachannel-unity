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
        // DataChannel needs to hold these delegates as the native plugin libdatachannel
        // expects the function pointers from these to live stay alive when calling them as callbacks.
        private RtcOpenCallbackFunc openCallback;
        private RtcClosedCallbackFunc closedCallback;
        private RtcErrorCallbackFunc errorCallback;
        private RtcMessageCallbackFunc messageCallback;

        public DataChannel(int id)
        {
            Id = id;

            openCallback = new RtcOpenCallbackFunc(OnOpen);
            if (DataChannelPlugin.unity_rtcSetOpenCallback(Id, openCallback) < 0)
                throw new Exception("Error from unity_rtcSetOpenCallback.");

            closedCallback = new RtcClosedCallbackFunc(OnClosed);
            if (DataChannelPlugin.unity_rtcSetClosedCallback(id, closedCallback) < 0)
                throw new Exception("Error from unity_rtcSetClosedCallback.");

            errorCallback = new RtcErrorCallbackFunc(OnError);
            if (DataChannelPlugin.unity_rtcSetErrorCallback(id, errorCallback) < 0)
                throw new Exception("Error from unity_rtcSetErrorCallback.");

            messageCallback = new RtcMessageCallbackFunc(OnMessage);
            if (DataChannelPlugin.unity_rtcSetMessageCallback(Id, messageCallback) < 0)
                throw new Exception("Error from unity_rtcSetMessageCallback.");
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

        private void OnOpen(IntPtr ptr)
        {
            Opened?.Invoke();
        }

        private void OnClosed(IntPtr ptr)
        {
            Closed?.Invoke();
        }

        private void OnError(string error, IntPtr ptr)
        {
            ErrorCreated?.Invoke(error);
        }

        private void OnMessage(IntPtr meesage, int size, IntPtr ptr)
        {
            byte[] managedMessage = new byte[size];
            Marshal.Copy(meesage, managedMessage, 0, size);
            MessageReceived?.Invoke(managedMessage);
        }
    }
}