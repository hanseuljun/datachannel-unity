using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rtc
{
    public class Track : Channel
    {
        public Track(int id) : base(id)
        {
        }

        ~Track()
        {
            DataChannelPlugin.unity_rtcDeleteTrack(Id);
        }
    }
}