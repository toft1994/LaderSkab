using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaderSkab.Door
{
    internal interface IDoor
    {
        event EventHandler DoorOpenedEvent;
        event EventHandler DoorClosedEvent;
    }
}
