using Laderskab.Display;
using UsbSimulator;

namespace LaderSkab.ChargeControl
{
    public class ChargeControl : IChargeControl
    {
        private IDisplay _display;
        private IUsbCharger _usbChargerSimulator;

        public ChargeControl(IDisplay display, IUsbCharger usbCharger)
        {
            _display = display;
            _usbChargerSimulator = usbCharger;
            _usbChargerSimulator.CurrentValueEvent += _usbChargerSimulator_CurrentValueEvent;
        }
        
        public bool IsConnected()
        {
            return _usbChargerSimulator.Connected; 
        }

        public void StartCharge()
        {
            _usbChargerSimulator.StartCharge();
        }

        public void StopCharge()
        {
            _usbChargerSimulator.StopCharge();
            _display.CurrentChargeId = DisplayChargeId.Nothing;
        }

        private void _usbChargerSimulator_CurrentValueEvent(object sender, CurrentEventArgs e)
        {
            if (e.Current > 500)
            {
                StopCharge();
                _display.CurrentChargeId = DisplayChargeId.ShortCircuit;
                _display.UpdateDisplay();
            }
            else if (e.Current <= 500 && e.Current >= 5)
            {
                _display.CurrentChargeId = DisplayChargeId.Charging;
            }
            else if (e.Current > 0 && e.Current < 5)
            {
                StopCharge();
                _display.CurrentChargeId = DisplayChargeId.FullyCharged;
                _display.UpdateDisplay();
            }
        }
    }
}