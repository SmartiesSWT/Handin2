using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handin2
{
    public interface IChargeControl
    {

        bool IsConnected();

        // Start charging
        void StartCharge();
        // Stop charging
        void StopCharge();
    }
}
