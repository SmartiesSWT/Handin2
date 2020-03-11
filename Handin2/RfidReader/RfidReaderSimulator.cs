using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handin2.RfidReader
{
    public class RfidReaderSimulator : IRfidReader
    {
        public event EventHandler<RfidEventArgs> RfidEvent;

        public void OnRfidRead(int id)
        {
            RfidEvent?.Invoke(this, new RfidEventArgs() { RfidTag = id });
        }
    }
}
