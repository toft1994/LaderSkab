using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laderskab.Display;
using Laderskab.Door;
using Laderskab.RFIDReader;
using Laderskab.StationControl;
using UsbSimulator;

namespace Ladeskab
{
    public class StationControl : IStationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        private IUsbCharger _charger;
        private IDoor _door;
        private IDisplay _display;
        private IRFIDReader _rfidReader;

        private LadeskabState _state;
        private int _oldId;
        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public StationControl(IDoor door, IDisplay display, IRFIDReader rfid, IUsbCharger charger)
        {
            _door = door;
            _display = display;
            _rfidReader = rfid;
            _charger = charger;

            /* Subscribe to event */
            _door.DoorOpenedEvent += HandleDoorOpenedEvent;
            _door.DoorClosedEvent += HandleDoorCloseEvent;
            _rfidReader.RFIDEvent += HandleRfidDataEvent;
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                        _display.DisplayId(DisplayId.SlotTaken);
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        private void HandleDoorOpenedEvent(object sender, EventArgs args)
        {
            _display.DisplayId(DisplayId.ConnectPhone);
        }
        private void HandleDoorCloseEvent(object sender, EventArgs args)
        {
            _display.DisplayId(DisplayId.WaitingRfid);
        }
        private void HandleRfidDataEvent(object sender, RFIDDataEventArgs args)
        {
            RfidDetected(args.RFIDtag);
        }
    }
}
