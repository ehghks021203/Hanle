using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKUCopy.Events
{
    public class GPSCopyStartEvent : BaseCopyEvent
    {
        public override EventType eventType
        {
            get
            {
                return EventType.TYPE_GPS_START;
            }
        }

        public bool isStarted;
        
    }
}
