using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace Laderskab.Test.Unit
{
    [TestFixture]
    internal class DisplayUnitTests
    {
        private DoorControlSystem.DoorControl _uut;
        private IAlarm _Alarm;
        private IUserValidation _UserValidation;
        private IEntryNotification _EntryNotification;
        private IDoor _Door;

        [SetUp]
        public void SetUp()
        {
            _Alarm = Substitute.For<IAlarm>();
            _UserValidation = Substitute.For<IUserValidation>();
            _EntryNotification = Substitute.For<IEntryNotification>();
            _Door = Substitute.For<IDoor>(); ;

            _uut = new DoorControlSystem.DoorControl(_Alarm, _UserValidation, _EntryNotification, _Door);
        }

        [Test]
        public void RequestEntry_IDisValid_OpenDoorCalled()
        {
            // Setup
            const string id = "Jesper";
            _UserValidation.ValidateEntryRequest(id).Returns(true);

            //Act
            _uut.RequestEntry(id);
            //Assert
            _Door.Received(1).Open();
        }
    }
}
