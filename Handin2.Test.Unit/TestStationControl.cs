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
        private IChargeControl _usb;

        [SetUp]
        public void Setup()
        {
            _uut = new StationControl(_door, _usb, _display, _rfid);
            _door = Substitute.For<IDoor>();
            _display = Substitute.For<IDisplay>();
            _rfid = Substitute.For<IRfidReader>();
            _usb = Substitute.For<IChargeControl>();
        }



    }
}
