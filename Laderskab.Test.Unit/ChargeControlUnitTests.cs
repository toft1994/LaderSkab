using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [TestCase()]
        public void IsConnected_DifferentCases_ConnectionStatusAccurate()
        {
            _usbCharger.Connected.Returns(true);
            Assert.That(_uut.IsConnected(), Is.True);
        }
    }
}
