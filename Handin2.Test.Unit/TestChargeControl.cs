using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Handin2.Test.Unit
{
    [TestClass]
    public class TestChargeControl
    {
        private ChargeControl _uut;
        private IDisplay _display;
        private IUsbCharger _usbCharger;

        [SetUp]
        public void Setup()
        {
            _display = Substitute.For<IDisplay>();
            _usbCharger = Substitute.For<IUsbCharger>();

            _uut = new ChargeControl(_usbCharger, _display);
        }

        
        [Test]
        public void TestStartCharge()
        {
            _uut.StartCharge();

            _usbCharger.Received().StartCharge();
        }

        [Test]
        public void TestStopCharge()
        {
            _uut.StopCharge();

            _usbCharger.Received().StopCharge();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void TestIsConnected(bool boolsk)
        {
            _usbCharger.Connected.Returns(boolsk);
            Assert.That(_uut.IsConnected, Is.EqualTo(boolsk));
        }

        [Test]
        public void TestCurrentEvent_WithLowCurrent()
        {
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() { Current = 1 });

            _display.Received().print("Charge completed");
        }

        [Test]
        public void TestCurrentEvent_WithMediumCurrent()
        {
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() { Current = 100 });

            _display.Received().print("Ladning igang");
        }

        [Test]
        public void TestCurrentEvent_WithHighCurrent()
        {
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() { Current = 550 });

            _display.Received().print("Fejl i ladning. frakobl straks");
        }

        [Test]
        public void TestCurrentEvent_WithCurrentOutofRange()
        {
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() { Current = -1 });

            _display.DidNotReceive().print(Arg.Any<string>());
        }

    }
}
