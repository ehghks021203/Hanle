
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KKUCopy.Events;
using UnityEngine;

namespace KKUCopy
{
    public interface GPSCopyEventListener
    {

        void GPSEvent(BaseCopyEvent baseEvent);
    }
}
