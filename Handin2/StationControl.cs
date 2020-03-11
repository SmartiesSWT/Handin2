using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handin2;
using Handin2.RfidReader;
using UsbSimulator;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IRfidReader _rfidReader;
        private IUsbCharger _charger;
        private IDoor _door;
        private IDisplay _display;
        private int _oldId;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IDoor door, IUsbCharger charger, IDisplay display, IRfidReader rfidReader)
        {
            _charger = charger;
            _display = display;
            _door = door;
            _rfidReader = rfidReader;

           

            _door.DoorEvent += DoorEvent;
            _rfidReader.RfidEvent += RfidEvent;

        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren.");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag.");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere

        private void DoorEvent(Object sender, DoorEventArgs e)
        {
            if (e.IsDoorOpen == true)
            {
                DoorOpened(e);
            }
            else
            {
                DoorClosed(e);
            }
        }

        private void DoorOpened(DoorEventArgs e)
        {
            
            Console.WriteLine("Tilslut din telefon til ladestikket.");
            e.IsDoorOpen = true;
            _state = LadeskabState.DoorOpen;
        }

        private void DoorClosed(DoorEventArgs e)
        {
            if (_charger.Connected)
            {
                Console.WriteLine("Indlæs RFID.");
                var input = Console.ReadLine();
                e.IsDoorOpen = false;
                _state = LadeskabState.Available;
                int intinput = Int16.Parse(input);
                RfidDetected(intinput);
            }
            else
            {
                Console.WriteLine("Ingen telefon connected");
            }
            
        }

        private void RfidEvent(Object sender, RfidEventArgs e)
        {
           RfidDetected(e.RfidTag);
        }

    }
}
