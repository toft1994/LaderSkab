using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaderSkab.Display
{
    class Display : IDisplay
    {
        public void DisplayID(DisplayId id)
        {
            switch (id)
            {
                case LaderSkab.Display.DisplayId.ConnectPhone:
                    DisplayConnectPhone();
                    break;

                case LaderSkab.Display.DisplayId.RemovePhone:
                    DisplayRemovePhone();
                    break;

                case LaderSkab.Display.DisplayId.ConnectionError:
                    DisplayConnectionError();
                    break;

                case LaderSkab.Display.DisplayId.SlotTaken:
                    DisplayChargingSlotTaken();
                    break;

                case LaderSkab.Display.DisplayId.WaitingRfid:
                    DisplayWaitingForRFID();
                    break;

                case LaderSkab.Display.DisplayId.RfidError:
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
