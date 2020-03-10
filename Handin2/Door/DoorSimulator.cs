using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handin2
{
    class DoorSimulator : IDoor
    {

        public event EventHandler<DoorEventArgs> DoorEvent;

        public bool Open { get; private set; }
        public bool Islocked { get; set; }

        public DoorSimulator()
        {
            Open = false;
        }

        public void LockDoor()
        {
            Islocked = true;
        }

        public void UnlockDoor()
        {
            Islocked = false;
        }

        public void OnDoorOpen()
        {
            if (Islocked)
            {
                Console.WriteLine("The door is locked");
                return;
            }
            Open = true;
            Console.WriteLine("The Door is now open");
            DoorEvent?.Invoke(this, new DoorEventArgs() { IsDoorOpen = this.Open });
        }

        public void OnDoorClose()
        {
            
            Open = false;
            DoorEvent?.Invoke(this,new DoorEventArgs(){IsDoorOpen = this.Open});
        }
    }
}
