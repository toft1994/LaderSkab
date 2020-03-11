using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laderskab.ChargeControl;
using Laderskab.Display;
using Laderskab.Door;
using LaderSkab.Logger;
using Laderskab.RFIDReader;
using Laderskab.StationControl;
using NSubstitute;
using NSubstitute.Routing.Handlers;
using NUnit.Framework;

namespace Laderskab.Test.Unit
{
    [TestFixture]
    internal class StationControlUnitTests
    {
        private IStationControl _uut;
        private IDoor _door;
        private IDisplay _display;
        private IRFIDReader _rfidReader;
        private IChargeControl _chargeControl;
        private ILogger _logger;

        [SetUp]
        public void SetUp()
        {
            _door = Substitute.For<IDoor>();
            _display = Substitute.For<IDisplay>();
            _rfidReader = Substitute.For<IRFIDReader>();
            _chargeControl = Substitute.For<IChargeControl>();
            _logger = Substitute.For<ILogger>();
            _uut = new StationControl.StationControl(_door,_display,_rfidReader,_chargeControl,_logger);
        }

        [Test]
        public void HandleDoorOpenedEvent_DoorIsOpened_UpdateDisplay()
        {
            //Act
            _door.DoorOpenedEvent += Raise.Event();

            //Assert
            _display.Received(1).UpdateDisplay();
        }

        [Test]
        public void HandleDoorOpenedEvent_DoorIsOpened_IDisSet()
        {
            //Act
            _door.DoorOpenedEvent += Raise.Event();

            //Assert
            Assert.That(_display.CurrentMessageId.Equals(DisplayMessageId.ConnectPhone));
        }

        [Test]
        public void HandleDoorClosedEvent_DoorIsClosed_UpdateDisplay()
        {
            //Act
            _door.DoorClosedEvent += Raise.Event();

            //Assert
            _display.Received(1).UpdateDisplay();
        }

        [Test]
        public void HandleDoorClosedEvent_DoorIsClosed_IDisSet()
        {
            //Act
            _door.DoorClosedEvent += Raise.Event();

            //Assert
            Assert.That(_display.CurrentMessageId.Equals(DisplayMessageId.Nothing));
        }

        [Test]
        public void HandleRfidDataEvent_PhoneNotConnected_UpdateDisplayCalled()
        {
            //Setup
            _chargeControl.IsConnected().Returns(false);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });

            //Assert
            _display.Received(1).UpdateDisplay();
        }

        [Test]
        public void HandleRfidDataEvent_PhoneNotConnected_MessageIdSet()
        {
            //Setup
            _chargeControl.IsConnected().Returns(false);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });

            //Assert
            Assert.That(_display.CurrentMessageId.Equals(DisplayMessageId.ConnectionError));
        }

        [Test]
        public void HandleRfidDataEvent_PhoneConnected_UpdateDisplayCalled()
        {
            //Setup
            _chargeControl.IsConnected().Returns(true);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith( new RFIDDataEventArgs{RFIDtag = 123});

            //Assert
            _display.Received(1).UpdateDisplay();
        }

        [Test]
        public void HandleRfidDataEvent_PhoneConnected_MessageIdSet()
        {
            //Setup
            _chargeControl.IsConnected().Returns(true);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });

            //Assert
            Assert.That(_display.CurrentMessageId.Equals(DisplayMessageId.SlotTaken));
        }

        [Test]
        public void HandleRfidDataEvent_PhoneConnected_LockDoorCalled()
        {
            //Setup
            _chargeControl.IsConnected().Returns(true);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });

            //Assert
            _door.Received(1).LockDoor();
        }

        [Test]
        public void HandleRfidDataEvent_PhoneConnected_StartChargeCalled()
        {
            //Setup
            _chargeControl.IsConnected().Returns(true);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });

            //Assert
            _chargeControl.Received(1).StartCharge();
        }

        [Test]
        public void HandleRfidDataEvent_PhoneConnected_LogAddCalled()
        {
            //Setup
            _chargeControl.IsConnected().Returns(true);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });

            //Assert
            _logger.Received(1).Add(Arg.Any<ClosetLog>());
        }

        [Test]
        public void HandleRfidDataEvent_WrongId_MessageIdSet()
        {
            //Setup
            _chargeControl.IsConnected().Returns(true);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 23 });

            //Assert
            Assert.That(_display.CurrentMessageId.Equals(DisplayMessageId.RfidError));
        }

        [Test]
        public void HandleRfidDataEvent_WrongId_UpdateDisplayCalled()
        {
            _chargeControl.IsConnected().Returns(true);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });

            //Assert
            _display.Received(2).UpdateDisplay();
        }

        [Test]
        public void HandleRfidDataEvent_CorrectId_StopCharge()
        {
            //Setup
            _chargeControl.IsConnected().Returns(true);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });

            //Assert
            _chargeControl.Received(1).StopCharge();
        }

        [Test]
        public void HandleRfidDataEvent_CorrectId_UnlockDoor()
        {
            //Setup
            _chargeControl.IsConnected().Returns(true);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });

            //Assert
            _door.Received(1).UnlockDoor();
        }

        [Test]
        public void HandleRfidDataEvent_CorrectId_AddCalled()
        {
            //Setup
            _chargeControl.IsConnected().Returns(true);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });

            //Assert
            //_door.Received(1).UnlockDoor();
        }

        [Test]
        public void HandleRfidDataEvent_CorrectId_UpdateDisplay()
        {
            //Setup
            _chargeControl.IsConnected().Returns(true);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });

            //Assert
            _display.Received(2).UpdateDisplay();
        }

        [Test]
        public void HandleRfidDataEvent_CorrectId_MessageIdSet()
        {
            //Setup
            _chargeControl.IsConnected().Returns(true);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });

            //Assert
            Assert.That(_display.CurrentMessageId.Equals(DisplayMessageId.RemovePhone));
        }

        [Test]
        public void HandleRfidDataEvent_CorrectId_LogAddCalled()
        {
            //Setup
            _chargeControl.IsConnected().Returns(true);

            //Act
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });
            _rfidReader.RFIDEvent += Raise.EventWith(new RFIDDataEventArgs { RFIDtag = 123 });

            //Assert
            _logger.Received(2).Add(Arg.Any<ClosetLog>());
        }
    }
}
