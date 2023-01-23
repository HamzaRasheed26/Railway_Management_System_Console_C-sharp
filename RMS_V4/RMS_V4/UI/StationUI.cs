using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS_V4.BL;

namespace RMS_V4.UI
{
    internal class StationUI
    {
        public static Station takeStationInput()
        {
            string stationName;
            int ath, atm, dth, dtm;

            Console.Write("\n Station name :"); // station  name
            stationName = Console.ReadLine();

            Console.WriteLine(" Note : use 24 hours format for input time ");

            while (true) // validation on correcrt time
            {
                Console.Write(" Arrival Time( hh:mm ) :"); // arrival time station 
                ath = int.Parse(Console.ReadLine()); // hour

                atm = int.Parse(Console.ReadLine()); // minute

                if (ath >= 1 && ath <= 24 && atm >= 0 && atm <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }
            while (true) // validation on correcrt time
            {
                Console.Write(" Departure Time( hh:mm ) :"); // arrival time station 
                dth = int.Parse(Console.ReadLine());  // hour

                dtm = int.Parse(Console.ReadLine()); // minute

                if (dth >= 1 && dth <= 24 && dtm >= 0 && dtm <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            return new Station(stationName, ath, atm, dth, dtm);
        }
    }
}
