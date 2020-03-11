using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laderskab.Display
{
    public class Display : IDisplay
    {
        public DisplayChargeId CurrentChargeId { get; set; }
        public DisplayMessageId CurrentMessageId { get; set; }

        public Display()
        {
            CurrentMessageId = DisplayMessageId.Nothing;
            CurrentChargeId = DisplayChargeId.Nothing;
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            //Console.Clear();
            Console.WriteLine("------------------------------------");
            Console.WriteLine("General information:");
            DisplayInfoMessage(CurrentMessageId);
            Console.WriteLine("------------------------------------\n");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Charging information:");
            DisplayChargeMessage(CurrentChargeId);
            Console.WriteLine("------------------------------------");
        }

        #region DisplayCommands
        private void DisplayChargeMessage(DisplayChargeId id)
        {
            switch (id)
            {
                case Laderskab.Display.DisplayChargeId.FullyCharged:
                    DisplayFullyCharged();
                    break;

                case Laderskab.Display.DisplayChargeId.Charging:
                    DisplayCharging();
                    break;

                case Laderskab.Display.DisplayChargeId.ConnectionError:
                    DisplayConnectionError();
                    break;

                case Laderskab.Display.DisplayChargeId.ShortCircuit:
                    DisplayShortCircuit();
                    break;

                case DisplayChargeId.Nothing:
                    // Nothing to display.
                    break;
            }
        }

        private void DisplayInfoMessage(DisplayMessageId id)
        {
            switch (id)
            {
                case Laderskab.Display.DisplayMessageId.ConnectPhone:
                    DisplayConnectPhone();
                    break;

                case Laderskab.Display.DisplayMessageId.RemovePhone:
                    DisplayRemovePhone();
                    break;

                case Laderskab.Display.DisplayMessageId.ConnectionError:
                    DisplayConnectionError();
                    break;

                case Laderskab.Display.DisplayMessageId.SlotTaken:
                    DisplayChargingSlotTaken();
                    break;

                case Laderskab.Display.DisplayMessageId.WaitingRfid:
                    DisplayWaitingForRFID();
                    break;

                case Laderskab.Display.DisplayMessageId.RfidError:
                    DisplayRFIDError();
                    break;
            }
        }

        private void DisplayChargingSlotTaken()
        {
            Console.WriteLine("Ladeskab optaget\nBrug dit RFID tag til at låse op.");
        }

        private void DisplayConnectionError()
        {
            Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

        private void DisplayConnectPhone()
        {
            Console.WriteLine("Tilslut telefon");
        }

        private void DisplayRemovePhone()
        {
            Console.WriteLine("Tag din telefon ud af skabet og luk døren");
        }

        private void DisplayRFIDError()
        {
            Console.WriteLine("RFID fejl");
        }

        private void DisplayWaitingForRFID()
        {
            Console.WriteLine("Indlæs RFID");
        }

        private void DisplayFullyCharged()
        {
            Console.WriteLine("Telefon fuldt opladet");
        }

        private void DisplayCharging()
        {
            Console.WriteLine("Telefon oplader");
        }
        
        private void DisplayShortCircuit()
        {
            Console.WriteLine("Kortslutning! Frakobel telefon med det samme!");
        }
        #endregion
    }
}
