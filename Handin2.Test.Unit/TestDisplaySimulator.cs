using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Handin2.Test.Unit
{
    [TestClass]
    public class TestDisplaySimulator
    {
        private DisplaySimulator _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new DisplaySimulator();
        }

        [Test]
        public void TestOpeningAClosedDoor()
        {
            _uut.print("hej");
            Assert.That(_uut.test, Is.EqualTo(1));
        }
    }
}





