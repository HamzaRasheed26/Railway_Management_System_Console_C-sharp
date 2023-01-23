using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_V4.BL
{
    internal class Station
    {
        private string stationName;

        private int ath;
        private int atm;

        private int dth;
        private int dtm;

        public Station(string stationName, int ath, int atm, int dth, int dtm)
        {
            this.stationName = stationName;
            this.ath = ath;
            this.atm = atm;
            this.dth = dth;
            this.dtm = dtm;
        }

        public string StationName { get => stationName; set => stationName = value; }
        public int Ath { get => ath; set => ath = value; }
        public int Atm { get => atm; set => atm = value; }
        public int Dth { get => dth; set => dth = value; }
        public int Dtm { get => dtm; set => dtm = value; }

        public void print()
        {
            Console.WriteLine(" " + stationName + "\t" + Ath + "\t" + Atm + "\t" + Dth + "\t" + Dtm);
        }
    }
}
