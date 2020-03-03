using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laderskab.Display
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
    public interface IDisplay
    {
        void DisplayId(DisplayId id);
    }
}
