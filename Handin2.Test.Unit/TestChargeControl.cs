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


        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(100)]
        [TestCase(501)]
        public void TestCurrentEvent_WithDifCurrents(double current)
        {
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() {Current = current});

            if (0 < current && current <= 5)
            {
                _display.Received().print("Charge completed");
            }else if (5 < current && current < 500)
            {
                _display.Received().print("Ladning igang");
            }
            else if (current > 500)
            {
                _display.Received().print("Fejl i ladning. frakobl straks");
            }
            else
            {
                _display.DidNotReceive().print(Arg.Any<string>());
            }
           
            
        }
    }
}
