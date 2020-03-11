using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using LaderSkab.Logger;
using NUnit.Framework;

namespace Laderskab.Test.Unit
{
    //The Logger
    [TestFixture]
    class LoggerUnitTests
    {
        //Private fields
        private Logger _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Logger();
        }

        [Test]
        public void OnConstruction_LenghtIsZero()
        {
            var testlogEntry = new ClosetLog
            {
                Date = DateTime.Now,
                Log = "Test"
            };

            //Act
            _uut.Add(testlogEntry);

            Assert.That(File.Exists(_uut.Get()), Is.EqualTo(true));
        }

        [Test]
        public void OnConstructisdon_LenghtIsZero()
        {
            var testlogEntry = new ClosetLog
            {
                Date = DateTime.Now,
                Log = "Test"
            };

            //Act
            _uut.Clear();

            Assert.That(File.Exists(_uut.Get()), Is.EqualTo(false));
        }

        //[Test]
        //public void OnConstruction_LenghtIsZero()
        //{
        //    Assert.That(_uut.Length, Is.EqualTo(0));
        //}

        //[Test]
        //public void Add_ItemInList()
        //{
        //    var item = new ClosetLog();
        //    _uut.Add(item);
        //    Assert.That(_uut.Get().Contains(item), Is.EqualTo(true));
        //}

        //[Test]
        //public void Add_LengthIncreasedByOne()
        //{
        //    var item = new ClosetLog();
        //    _uut.Add(item);
        //    Assert.That(_uut.Length, Is.EqualTo(1));
        //}

        //[Test]
        //public void Clear_ItemListIsEmpty()
        //{
        //    var item = new ClosetLog();
        //    _uut.Add(item);
        //    _uut.Clear();
        //    Assert.That(_uut.Get().IsNullOrEmpty(), Is.EqualTo(true));
        //}

        //[Test]
        //public void Clear_LoggerLengthIsZero()
        //{
        //    var item = new ClosetLog();
        //    _uut.Add(item);
        //    _uut.Clear();
        //    Assert.That(_uut.Length, Is.EqualTo(0));
        //}
    }

    //ClosetLog
    [TestFixture]
    public class ClosetLogUnitTest
    {
        //Private field
        private ClosetLog _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new ClosetLog();
        }

        [Test]
        public void Date_SetDate()
        {
            _uut.Date = new DateTime(10,10,10);
            Assert.That(_uut.Date == new DateTime(10,10,10));
        }

        [Test]
        public void Log_GetSetLog()
        {
            _uut.Log = "Log thingy";
            Assert.That(_uut.Log == "Log thingy");
        }
    }
}
