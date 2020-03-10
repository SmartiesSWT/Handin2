using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handin2
{

    public class DoorEventArgs : EventArgs
    {
        // Open = true, closed = false
        public bool IsDoorOpen { set; get; }
    }
    public interface IDoor
    {
        event EventHandler<DoorEventArgs> DoorEvent;
        void LockDoor();

        void UnlockDoor(); 
    }
}
