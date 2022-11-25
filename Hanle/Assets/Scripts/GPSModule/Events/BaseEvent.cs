using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KKU.Events
{
    public abstract class BaseEvent
    {
        public enum EventType : byte
        {
            TYPE_GPS_PERMISSION,
            TYPE_GPS_START,
            TYPE_GPS_MOVE
        }

        public abstract EventType eventType { get; }

    }
}
