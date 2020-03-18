using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handin2
{
    public class RfidEventArgs : EventArgs
    {
        public int RfidTag { set; get; }
    }
    public interface IRfidReader
    {
        event EventHandler<RfidEventArgs> RfidEvent;

        void OnRfidRead(int id);
    }
}
