using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laderskab.Door
{
    public class Door : IDoor
    {
        public event EventHandler DoorOpenedEvent;
        public event EventHandler DoorClosedEvent;

        private bool _locked;

        public void OnDoorOpen()
        {
            DoorOpenedEvent?.Invoke(this, null);
        }

        public void OnDoorClose()
        {
            DoorClosedEvent?.Invoke(this, null);
        }

        public void LockDoor()
        {
            _locked = true;
        }

        public void UnlockDoor()
        {
            _locked = false;
        }

        public bool IsLocked()
        {
            return _locked;
        }
    }
}
