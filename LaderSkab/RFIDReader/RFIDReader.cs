using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laderskab.RFIDReader
{
    public class RFIDReader : IRFIDReader
    {
        public event EventHandler<RFIDDataEventArgs> RFIDEvent;

        public void OnRfidRead(int rfid)
        {
            var args = new RFIDDataEventArgs
            {
                RFIDtag = rfid
            };
            RFIDEvent?.Invoke(this,args);
        }
    }
}
