using KKU.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KKU
{
    public interface GPSEventListener
    {

        void GPSEvent(BaseEvent baseEvent);
    }
}
