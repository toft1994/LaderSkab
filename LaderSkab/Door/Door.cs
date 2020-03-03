using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaderSkab.Door
{
    class Door : IDoor
    {
        public event EventHandler DoorOpenedEvent;
        public event EventHandler DoorClosedEvent;

        public void OnDoorOpen()
        {
            DoorOpenedEvent?.Invoke(this, null);
        }
        public void OnDoorClose()
        {
            DoorClosedEvent?.Invoke(this,null);
        }
    }
}
