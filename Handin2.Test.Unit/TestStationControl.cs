using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NSubstitute;
using Assert = NUnit.Framework.Assert;

namespace Handin2.Test.Unit
{
    [TestClass]
    public class TestStationControl
    {
        private StationControl _uut;
        private IDoor _door;
        private IDisplay _display;
        private IRfidReader _rfid;
        private IChargeControl _chargeControl;

        [SetUp]
        public void Setup()
        {
            
            _door = Substitute.For<IDoor>();
            _display = Substitute.For<IDisplay>();
            _rfid = Substitute.For<IRfidReader>();
            _chargeControl = Substitute.For<IChargeControl>();

            _uut = new StationControl(_door, _chargeControl, _display, _rfid);

        }

        [Test]
        public void TestRfidDetected_Available_testDoorlocking()
        {
            _chargeControl.IsConnected().Returns(true);

            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() {RfidTag = 1234});

            _door.Received().LockDoor();
        }

        [Test]
        public void TestRfidDetected_Available_testStartCharging()
        {
            _chargeControl.IsConnected().Returns(true);

            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { RfidTag = 1234 });

            _chargeControl.Received().StartCharge();
        }

        [Test]
        public void TestRfidDetected_StateLocked_testStopCharging()
        {
            //Act
            _chargeControl.IsConnected().Returns(true);

            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { RfidTag = 1234 });
            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { RfidTag = 1234 });

            //Assert
            _chargeControl.Received().StopCharge();
        }

        
        [Test]
        public void TestRfidDetected_Available_testNotConnected()
        {
            _chargeControl.IsConnected().Returns(false);

            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { RfidTag = 1234 });

            _display.Received().print("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

        [Test]
        public void TestRfidDetected_Locked_WrongRfidTag()
        {
            _chargeControl.IsConnected().Returns(true);

            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { RfidTag = 1234 });

            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { RfidTag = 1231 });

            _display.Received().print("Forkert RFID tag.");
        }

        [Test]
        public void Test_DoorOpen_NoConnect_ConnectPhone()
        {
            //Act 
            // Vi vil gerne teste at når der
            // Åbnes for døren, uden at være tilsluttet en telefon 
            // vil der printes fra display

            //Simulerer at ingen telefon er tilsluttet
            _chargeControl.IsConnected().Returns(false);

            //Raiser et doorevent så døren åbnes
            _door.DoorEvent +=
                Raise.EventWith(new DoorEventArgs() { IsDoorOpen = true });

            _display.Received().print("Tilslut din telefon.");
        }

        [Test]
        public void Test_RFIDEVENT_DoorOpen_NoDisplay()
        {
            //Act 
            // Vi vil gerne teste at når der
            // Bliver læst et RFID event, printes der ikke noget


            //Raiser et doorevent så døren åbnes
            _door.DoorEvent +=
                Raise.EventWith(new DoorEventArgs() { IsDoorOpen = true });

            _display.ClearReceivedCalls();

            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { RfidTag = 1234 });


            
            _display.DidNotReceive().print(Arg.Any<string>());
        }

    }
}
