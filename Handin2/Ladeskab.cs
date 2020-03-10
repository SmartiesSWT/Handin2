using Handin2.RfidReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handin2.Display;
using Ladeskab;
using UsbSimulator;

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

            StationControl stationControl = new StationControl(door,charger,display, rfidReader);

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OnDoorOpen();
                        Console.WriteLine("Press any button to simulate connecting phone");
                        Console.ReadLine();
                        charger.SimulateConnected(true);
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

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
