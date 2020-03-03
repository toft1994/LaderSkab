using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaderSkab.RFIDReader
{
    interface IRFIDReader
    {

        void OnRfidRead(string rfid);
    }
}
