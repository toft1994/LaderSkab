﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laderskab
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            Door.Door door = new Door.Door();
            RFIDReader.RFIDReader rfidReader = new RFIDReader.RFIDReader();

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