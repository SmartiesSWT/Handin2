using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace Handin2.Test.Unit
{
    [TestClass]
    public class TestDoorSimulator
    {

        private DoorSimulator _uut;
        private DoorEventArgs _recievedDoorEventArgs;

        [SetUp]
        public void Setup()
        {
            _recievedDoorEventArgs = null;
            _uut = new DoorSimulator();

            _uut.DoorEvent += (o, args) =>
            {
                _recievedDoorEventArgs = args;
            };
        }

        [Test]
        public void TestOpeningAClosedDoor()
        {
            _uut.Open = false;
            _uut.Islocked = false;

            _uut.OnDoorOpen();
            Assert.That(_recievedDoorEventArgs, Is.Not.Null);
        }

        [Test]
        public void TestingOpeningAClosedDoor_GotNewValue()
        {
            _uut.Open = false;
            _uut.Islocked = false;

            _uut.OnDoorOpen();
            Assert.That(_recievedDoorEventArgs.IsDoorOpen, 
                Is.EqualTo(true));
        }

        [Test]
        public void TestingOpeningAOpenDoor()
        {
            _uut.Open = true;
            _uut.Islocked = false;

            _uut.OnDoorOpen();
            Assert.That(_recievedDoorEventArgs, Is.Null);
           
        }

        [Test]
        public void TestingOpeningALockedDoor()
        {
            _uut.Open = false;
            _uut.Islocked = true;

            _uut.OnDoorOpen();
            Assert.That(_recievedDoorEventArgs, Is.Null);
            
        }

        [Test]
        public void TestingOpeningALockedDoor_IsClosed()
        {
            _uut.Open = false;
            _uut.Islocked = true;

            _uut.OnDoorOpen();
            
            Assert.That(_uut.Open, Is.EqualTo(false));
        }

        [Test]
        public void TestingClosingAOpenDoor()
        {
            _uut.Open = true;
            

            _uut.OnDoorClose();
            Assert.That(_recievedDoorEventArgs, Is.Not.Null);
        }

        [Test]
        public void TestingClosingAOpenDoor_GotNewValue()
        {
            _uut.Open = true;


            _uut.OnDoorClose();
            Assert.That(_recievedDoorEventArgs.IsDoorOpen, Is.EqualTo(false));
        }

        [Test]
        public void TestingClosingAClosedDoor()
        {
            _uut.Open = false;


            _uut.OnDoorClose();
            Assert.That(_recievedDoorEventArgs, Is.Null);
        }

        [Test]
        public void TestingLockDoor()
        {
            _uut.LockDoor();

            Assert.That(_uut.Islocked, Is.EqualTo(true) );
        }

        [Test]
        public void TestingUnLockDoor()
        {
            _uut.UnlockDoor();

            Assert.That(_uut.Islocked, Is.EqualTo(false));
        }

    }
}
