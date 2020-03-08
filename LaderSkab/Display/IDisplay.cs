using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laderskab.Display
{
    public enum DisplayMessageId
    {
        ConnectPhone,
        RemovePhone,
        ConnectionError,
        SlotTaken,
        WaitingRfid,
        RfidError,
        Nothing,
    }

    public enum DisplayChargeId
    {
        FullyCharged,
        Charging,
        ConnectionError,
        ShortCircuit,
        Nothing,
    }
    public interface IDisplay
    {
        DisplayChargeId CurrentChargeId { get; set; }
        DisplayMessageId CurrentMessageId { get; set; }
        void UpdateDisplay();
    }
}
