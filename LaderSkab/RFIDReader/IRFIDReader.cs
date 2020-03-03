using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laderskab.RFIDReader
{
    public class RFIDDataEventArgs
    {
        public int RFIDtag { get; set; }
    }

    public interface IRFIDReader
    {
        event EventHandler<RFIDDataEventArgs> RFIDEvent;
        void OnRfidRead(int rfid);
    }
}
