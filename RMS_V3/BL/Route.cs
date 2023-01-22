using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_V3.BL
{
    internal class RouteType
    {
        // for train names
        public string trainName;

        public List<string> station = new List<string>();

        public List<int> ath = new List<int>();
        public List<int> atm = new List<int>();

        public List<int> dth = new List<int>();
        public List<int> dtm = new List<int>();

       /* // for stations
        public string ts1; // for station 1
        public string ts2; // for station 2
        public string ts3; // for station 3
        public string ts4; // for station 4*/

        /*//  for trains arrival times on stations
        public int ts1_ath; // for station 1 arrival hour
        public int ts1_atm; // for station 1 arrival minutes
        public int ts2_ath; // for station 2 arrival hour
        public int ts2_atm; // for station 2 arrival minutes
        public int ts3_ath; // for station 3 arrival hour
        public int ts3_atm; // for station 3 arrival minutes
        public int ts4_ath; // for station 4 arrival hour
        public int ts4_atm; // for station 4 arrival minutes

        // for trains departure times from stations
        public int ts1_dth; // for station 1 departure hour
        public int ts1_dtm; // for station 1 departure minutes
        public int ts2_dth; // for station 2 departure hour
        public int ts2_dtm; // for station 2 departure minutes
        public int ts3_dth; // for station 3 departure hour
        public int ts3_dtm; // for station 3 departure minutes
        public int ts4_dth; // for station 4 departure hour
        public int ts4_dtm; // for station 4 departure minutes*/

        // for prices of trains tickets
        public int tticket;
        // for prices of trains freight rate
        public int tcargo;

        // -------------------  Member Function ----------------------------------
        public void print(int idx)
        {
            Console.WriteLine(" " + station[idx] + "\t" + ath[idx] + "\t" + atm[idx] + "\t" + dth[idx] + "\t" + dtm[idx]);
        }

        public void setTicketPrice( int price)
        {
            tticket = price;
        }

        public void setFreightRate(int price)
        {
            tcargo = price;
        }

        public void changeTrainName(string name)
        {
            trainName = name;
        }

        public bool findStation(string st)
        {
            for (int y = 0; y < station.Count; y++)
            {
                if (st == station[y]) // it search the required station in ts1 array
                {
                    Console.WriteLine(trainName + "\t\t" + ath[y] + ":" + atm[y] + "\t" + dth[y] + ":" + dtm[y]);
                    return true;
                }
            }
            return false;
        }
    }
}
