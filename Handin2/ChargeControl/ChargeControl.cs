using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handin2
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger _charger;
        private IDisplay _display;

        private double oldcurrent { get; set; }

        public ChargeControl( IUsbCharger charger, IDisplay display)
        {
            _charger = charger;
            _display = display;

            _charger.CurrentValueEvent += CurrentEvent;
        }

        public void StartCharge()
        {
            _charger.StartCharge();
        }

        public void StopCharge()
        {
            _charger.StopCharge();
        }

        public bool IsConnected()
        {
            return _charger.Connected;
        }

        private void CurrentEvent(Object sender, CurrentEventArgs e)
        {
           
            if (0 < e.Current && e.Current <= 5 && !(0 < oldcurrent && oldcurrent <= 5))
            {
                _display.print("Charge completed");
                oldcurrent = e.Current;
            }
            else if (5 < e.Current && e.Current < 500 && !(5 < oldcurrent && oldcurrent < 500))
            {
                _display.print("Ladning igang");
                oldcurrent = e.Current;
            }
            else if (e.Current > 500 && !(oldcurrent > 500))
            {
                _display.print("Fejl i ladning. frakobl straks");
                oldcurrent = e.Current;
            }
            
        }

    }
}
