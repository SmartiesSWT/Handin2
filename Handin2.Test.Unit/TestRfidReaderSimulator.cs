using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handin2.RfidReader;
using NUnit.Framework;

namespace Handin2.Test.Unit
{
    class TestRfidReaderSimulator
    {
        private RfidReaderSimulator _uut;
        private RfidEventArgs _recievedRfidEventArgs;

        [SetUp]
        public void Setup()
        {
            _recievedRfidEventArgs = null;
            _uut = new RfidReaderSimulator();
        }
    }
}
