using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laderskab.Door
{
    public interface IDoor
    {
        event EventHandler DoorOpenedEvent;
        event EventHandler DoorClosedEvent;

        void LockDoor();
        void UnlockDoor();
    }
}
