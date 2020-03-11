using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laderskab.ChargeControl;
using Laderskab.Display;
using Laderskab.Door;
using Laderskab.RFIDReader;
using NSubstitute;
using NUnit.Framework;

namespace Laderskab.Test.Unit
{
    [TestFixture]
    public class RFIDReaderUnitTests
    {
        private RFIDReader.RFIDReader _uut;
        private IRFIDReader _RFIReader;
        private RFIDDataEventArgs receivedArgs;
        private int NumberOfEvents;

        [SetUp]
        public void SetUp()
        {
            _uut = new RFIDReader.RFIDReader();
            receivedArgs = null;
            NumberOfEvents = 0;

            _uut.RFIDEvent +=
                (s, a) =>
                {
                    receivedArgs = a;
                    ++NumberOfEvents;
                };
        }

        [Test]
        public void RFID_EventTriggered()
        {
            _uut.OnRfidRead(0);
            Assert.That(receivedArgs, Is.Not.Null);
        }

        [TestCase(0)] //Zero Value
        [TestCase(1)] //One value
        [TestCase(10)] //Many value
        [TestCase(int.MaxValue)] //Boundary value

        public void SetRFID_CorrectData(int rfid)
        {
            _uut.OnRfidRead(rfid);
            Assert.That(receivedArgs.RFIDtag, Is.EqualTo(rfid));
        }

        [Test]
        public void SetRFID_0_IfNegative()
        {
            _uut.OnRfidRead(-4);
            Assert.That(receivedArgs.RFIDtag, Is.EqualTo(0));
        }
    }
}
