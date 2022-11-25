using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKUCopy.Events
{
    public class GPSCopyMoveEvent : BaseCopyEvent
    {
        public override EventType eventType
        {
            get
            {
                return EventType.TYPE_GPS_MOVE;
            }
        }

        public enum Place
        {
            None,
            A,
            B,
            C,
            D,
            E
        }

        public Place place = Place.None;
        public bool isArrive = false;
    }
}
