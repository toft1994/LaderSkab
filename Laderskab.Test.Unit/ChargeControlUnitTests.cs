using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laderskab.Display;
using NUnit.Framework;
using UsbSimulator;
using NSubstitute;

namespace Laderskab.Test.Unit
{
    [TestFixture]
    class ChargeControlUnitTests
    {
        private ChargeControl.ChargeControl _uut;
        private Display.Display _display;
        private IUsbCharger _usbCharger;

        [SetUp]
        public void Setup()
        {
            //Set up dependencies using NSubstitute
            _display = Substitute.For<Display.Display>();
            _usbCharger = Substitute.For<IUsbCharger>();

            //Inject the dependencies through the constructor
            _uut = new ChargeControl.ChargeControl(_display, _usbCharger);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsConnected_DifferentCases_ConnectionStatusAccurate(bool status)
        {
            _usbCharger.Connected.Returns(status);
            Assert.That(_uut.IsConnected(), Is.EqualTo(status));
        }

        [Test]
        public void StartCharge_usbChargerSimulatorStartChargeIsCalled()
        {
            //Call the method
            _uut.StartCharge();

            //Assert that the method correctly calls StartCharge on the usbCharger
            _usbCharger.Received().StartCharge();
        }

        [Test]
        public void StopCharge_usbChargerSimulatorStopChargeIsCalledDisplaySetToNothing()
        {
            //Call the method
            _uut.StopCharge();

            //Assert that the method correctly calls StartCharge on the usbCharger
            _usbCharger.Received().StopCharge();

            Assert.That(_display.CurrentChargeId, Is.EqualTo(DisplayChargeId.Nothing));
        }

        [Test]
        public void CurrentValueEvent_Over500_StopChargeShortCircuit()
        {
            //Raise the even from usbCharger
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() {Current = 600});

            //Since the handler calls StopCharge on itself, we can test
            //If StopCharge has been called on usbCharger
            _usbCharger.Received().StopCharge();

            //Assert display is set to the right thing
            Assert.That(_display.CurrentChargeId, Is.EqualTo(DisplayChargeId.ShortCircuit));
            //Assert that updateDisplay has been called
            _display.Received().UpdateDisplay();
        }

        [TestCase(500)]
        [TestCase(5)]
        [TestCase(250)]
        public void CurrentValueEvent_Between500And5_CurrentChargeIdCharging(int current)
        {
            //Raise event
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() {Current = current});

            //Assert that _display.CurrentChargeId is Charging
            Assert.That(_display.CurrentChargeId, Is.EqualTo(DisplayChargeId.Charging));
        }

        [Test]
        public void CurrentValueEvent_Between0And5_CurrentChargeIdFullyChargedMethodsCalled()
        {
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() {Current = 2});

            //Since the handler calls StopCharge on itself, we can test
            //If StopCharge has been called on usbCharger
            _usbCharger.Received().StopCharge();

            Assert.That(_display.CurrentChargeId, Is.EqualTo(DisplayChargeId.FullyCharged));

            _display.Received().UpdateDisplay();
        }
    }
}
