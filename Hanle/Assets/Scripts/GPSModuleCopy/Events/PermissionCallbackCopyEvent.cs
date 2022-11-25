using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKUCopy.Events
{
    public class PermissionCallbackCopyEvent : BaseCopyEvent
    {
        public override EventType eventType
        {
            get
            {
                return EventType.TYPE_GPS_PERMISSION;
            }
        }

        public bool isGranted;
        public bool neverAskAgain;
    }
}
