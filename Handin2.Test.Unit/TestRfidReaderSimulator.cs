using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handin2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Handin2.Test.Unit
{   
    [TestClass]
    public class TestRfidReaderSimulator
    {
       
        private RfidReaderSimulator _uut;
        private RfidEventArgs _recievedRfidEventArgs;

        [SetUp]
        public void Setup()
        {
            _recievedRfidEventArgs = null;
            _uut = new RfidReaderSimulator();

            _uut.RfidEvent += (o, args) =>
            {
                _recievedRfidEventArgs = args;
            };
        }

        [Test]
        public void TestRfidRaiseEvent()
        {
            _uut.OnRfidRead(1234);
            Assert.That(_recievedRfidEventArgs, Is.Not.Null);
        }

        [Test]
        public void TestRfidReaderRegisterGivenId()
        {
            _uut.OnRfidRead(1234);

            Assert.That(_recievedRfidEventArgs.RfidTag,
                Is.EqualTo(1234));

        }

    }
}
