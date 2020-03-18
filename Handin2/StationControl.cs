using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handin2;


namespace Handin2
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

                        Console.WriteLine("Åben lågen og tag din telefon.");
                        
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

            switch (_state)
            {
                case LadeskabState.Available:
                    DoorOpened(e);
                    break;
                case LadeskabState.DoorOpen:
                    DoorClosed(e);
                    break;
                case LadeskabState.Locked:
                    _display.print("Hov hov, døren er låst du");
                    break;
                
            }

        }

        private void DoorOpened(DoorEventArgs e)
        {
            
            if (e.IsDoorOpen)
            {
                _display.print("Tilslut din telefon.");
                _state = LadeskabState.DoorOpen;
            }
            else
            {
                e.IsDoorOpen = true;
                _display.print("Tag din telefon");
            }
            
        }

        private void DoorClosed(DoorEventArgs e)
        {
            if (e.IsDoorOpen) 
            {
               _display.print("Der skete en fejl i lukning af døren.");
            }
           else 
            {
                
                _display.print("Døren er lukket, men ikke låst: Indlaes RFID ved at trykke R os så indtast nummer");
                _state = LadeskabState.Available;
            }
        }

        private void RfidEvent(Object sender, RfidEventArgs e)
        {
           RfidDetected(e.RfidTag);
        }

    }
}
