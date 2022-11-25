using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKU.Events
{
    public class GPSMoveEvent : BaseEvent
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
            WorldHistory,
            Mireuk_Daewonji,
            Mireuk_Wonter,
            DaeGwangSa,
            BaekJaGama,
            FriendTree,
            YeonAhTree,
            HanHwonRyeong,
            HaNeulJae_Top
        }

        public Place place = Place.None;
        public bool isArrive = false;
    }
}
