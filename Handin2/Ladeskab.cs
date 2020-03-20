using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handin2;

namespace Handin2
{
    class Ladeskab
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            DoorSimulator door = new DoorSimulator();
            RfidReaderSimulator rfidReader = new RfidReaderSimulator();
            
            UsbChargerSimulator charger = new UsbChargerSimulator();
            DisplaySimulator display = new DisplaySimulator();
            ChargeControl chargeControl = new ChargeControl(charger, display);
            StationControl stationControl = new StationControl(door,chargeControl,display, rfidReader);

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E (Exit), O (Open), C (Close), R (Rfid id), T (Tag telefon), P (Tilslut telefon): ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                     
                            door.OnDoorOpen();
                        
                        break;

                    case 'C':
                        door.OnDoorClose();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.OnRfidRead(id);
                        break;

                    case 'T':
                        charger.SimulateConnected(false);
                        //display.print("Du har taget din telefon");
                        
                        break;

                    case 'P':
                        charger.SimulateConnected(true);
                        break;
                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
