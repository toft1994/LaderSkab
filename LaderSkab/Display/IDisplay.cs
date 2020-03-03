using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaderSkab.Display
{
    public enum DisplayId
    {
        ConnectPhone,
        RemovePhone,
        ConnectionError,
        SlotTaken,
        WaitingRfid,
        RfidError
    }
    interface IDisplay
    {
        void DisplayID(DisplayId id);
    }
}
