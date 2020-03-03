using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laderskab.Display
{
    public class Display : IDisplay
    {
        public void DisplayId(DisplayId id)
        {
            switch (id)
            {
                case Laderskab.Display.DisplayId.ConnectPhone:
                    DisplayConnectPhone();
                    break;

                case Laderskab.Display.DisplayId.RemovePhone:
                    DisplayRemovePhone();
                    break;

                case Laderskab.Display.DisplayId.ConnectionError:
                    DisplayConnectionError();
                    break;

                case Laderskab.Display.DisplayId.SlotTaken:
                    DisplayChargingSlotTaken();
                    break;

                case Laderskab.Display.DisplayId.WaitingRfid:
                    DisplayWaitingForRFID();
                    break;

                case Laderskab.Display.DisplayId.RfidError:
                    DisplayRFIDError();
                    break;
            }
        }

        private void DisplayChargingSlotTaken()
        {
            Console.WriteLine("Ladeskab optaget");
        }

        private void DisplayConnectionError()
        {
            Console.WriteLine("Tilslutningsfejl");
        }

        private void DisplayConnectPhone()
        {
            Console.WriteLine("Tilslut telefon");
        }

        private void DisplayRemovePhone()
        {
            Console.WriteLine("Fjern telefon");
        }

        private void DisplayRFIDError()
        {
            Console.WriteLine("RFID fejl");
        }

        private void DisplayWaitingForRFID()
        {
            Console.WriteLine("Indlæs RFID");
        }
    }
}
