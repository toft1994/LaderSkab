using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laderskab.ChargeControl;
using Laderskab.Display;
using Laderskab.Door;
using Laderskab.RFIDReader;
using Laderskab.StationControl;
using UsbSimulator;

namespace Laderskab.StationControl
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

        private IChargeControl _chargeControl;
        private IDoor _door;
        private IDisplay _display;
        private IRFIDReader _rfidReader;

        private LadeskabState _state;
        private int _oldId;
        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public StationControl(IDoor door, IDisplay display, IRFIDReader rfid, IChargeControl charger)
        {
            _door = door;
            _display = display;
            _rfidReader = rfid;
            _chargeControl = charger;

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
                    if (_chargeControl.IsConnected())
                    {
                        _door.LockDoor();
                        _chargeControl.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        _state = LadeskabState.Locked;
                        _display.CurrentMessageId = DisplayMessageId.SlotTaken;
                        _display.UpdateDisplay();
                    }
                    else
                    {
                        _display.CurrentMessageId = DisplayMessageId.ConnectionError;
                        _display.UpdateDisplay();
                    }

                    break;
                
                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _chargeControl.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        _display.CurrentMessageId = DisplayMessageId.RemovePhone;
                        _display.UpdateDisplay();
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.CurrentMessageId = DisplayMessageId.RfidError;
                        _display.UpdateDisplay();
                    }

                    break;
            }
        }

        /* Hardware triggere */
        private void HandleDoorOpenedEvent(object sender, EventArgs args)
        {
            _display.CurrentMessageId = DisplayMessageId.ConnectPhone;
            _display.UpdateDisplay();
        }
        private void HandleDoorCloseEvent(object sender, EventArgs args)
        {
            _display.CurrentMessageId = DisplayMessageId.Nothing;
            _display.UpdateDisplay();
        }
        private void HandleRfidDataEvent(object sender, RFIDDataEventArgs args)
        {
            RfidDetected(args.RFIDtag);
        }
    }
}
