using System;
using System.ComponentModel;
using NSubstitute;
using NUnit.Framework;
using Laderskab.Door;

namespace Laderskab.Test.Unit
{
    //Door implements IDoor, which contains 2 events, and 2 methods.
    //Door contains two methods which invoke the two events. There are 4 methods which must be tested.
    //- OnDoorOpen()
    //- OnDoorClose()
    //- LockDoor()
    //- UnlockDoor()
    //The TestFixture uses two event handlers to keep track of events invoked by the door

    [TestFixture]
    public class DoorUnitTests
    {
        private Door.Door _uut;
        private uint _openEventCount;
        private uint _closeEventCount;


        //Create new door before each test
        [SetUp]
        public void Setup()
        {
            _uut = new Door.Door();
            _openEventCount = 0;
            _closeEventCount = 0;

            //Handler for event
            _uut.DoorOpenedEvent += OnEventOpen;
            _uut.DoorClosedEvent += OnEventClosed;
        }

        //Test OnDoorOpen, expected to invoke event 
        [Test]
        public void OnDoorOpenCall_DoorOpenEventInvoked()
        {
            _uut.OnDoorOpen();
            Assert.That(_openEventCount, Is.EqualTo(1));
        }

        [Test]
        public void OnDoorOpenCall_DoorClosedEventNotInvoked()
        {
            _uut.OnDoorOpen();
            Assert.That(_closeEventCount, Is.EqualTo(0));
        }

        [Test]
        public void OnDoorCloseCall_DoorCloseEventInvoked()
        {
            _uut.OnDoorClose();
            Assert.That(_closeEventCount, Is.EqualTo(1));
        }

        [Test]
        public void OnDoorCloseCall_DoorOpenedEventNotInvoked()
        {
            _uut.OnDoorClose();
            Assert.That(_openEventCount, Is.EqualTo(0));
        }

        [Test]
        public void LockDoorCall_DoorIsLocked()
        {
            _uut.LockDoor();
            Assert.That(_uut.IsLocked, Is.EqualTo(true));
        }

        [Test]
        public void UnlockDoorCall_DoorIsUnlocked()
        {
            _uut.UnlockDoor();
            Assert.That(_uut.IsLocked, Is.EqualTo(false));
        }

        [Test]
        public void OnConstruction_DoorIsUnlocked()
        {
            Assert.That(_uut.IsLocked, Is.EqualTo(false));
        }

        //Event handlers for Open and Close Event
        public void OnEventOpen(object sender, EventArgs e)
        {
            _openEventCount++;
        }

        public void OnEventClosed(object sender, EventArgs e)
        {
            _closeEventCount++;
        }
    }
}


