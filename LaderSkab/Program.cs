using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laderskab.Display;
using Laderskab.Door;
using Laderskab.StationControl;
using Laderskab.RFIDReader;

namespace Laderskab
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            var door = new Door.Door();
            var rfidReader = new RFIDReader.RFIDReader();
            var display = new Display.Display();
            var usbCharger = new UsbSimulator.UsbChargerSimulator();
            var chargeControl = new ChargeControl.ChargeControl(display, usbCharger);
            var stationControl = new StationControl.StationControl(door, display, rfidReader, chargeControl);

            bool finish = false;
            do
            {
                string input;
                //System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;
                input = input.ToUpper();

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OnDoorOpen();
                        break;

                    case 'C':
                        door.OnDoorClose();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.OnRfidRead(id);
                        break;

                    case 'B':
                        usbCharger.SimulateConnected(true);
                        usbCharger.SimulateOverload(false);
                        break;

                    case 'N':
                        usbCharger.SimulateConnected(false);
                        usbCharger.SimulateOverload(false);
                        break;

                    case 'M':
                        usbCharger.SimulateOverload(true);
                        usbCharger.SimulateConnected(true);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}