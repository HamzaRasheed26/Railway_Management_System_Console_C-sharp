using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_V4.BL
{
    internal class Route
    {
        // for train names
        private string trainName;
        private List<Station> stations;

        /*public List<string> station = new List<string>();
         public List<int> ath = new List<int>();
         public List<int> atm = new List<int>();
         public List<int> dth = new List<int>();
         public List<int> dtm = new List<int>();*/

        // for prices of trains tickets
        private int ticketPrice;
        // for prices of trains freight rate
        private int cargoPrice;

        public Route(string trainName, List<Station> stations, int ticketPrice, int cargoPrice)
        {
            this.trainName = trainName;
            this.stations = stations;
            this.ticketPrice = ticketPrice;
            this.cargoPrice = cargoPrice;
        }

        public string TrainName { get => trainName; set => trainName = value; }
        public List<Station> Stations { get => stations; set => stations = value; }
        public int TicketPrice { get => ticketPrice; set => ticketPrice = value; }
        public int CargoPrice { get => cargoPrice; set => cargoPrice = value; }

        // -------------------  Member Function ----------------------------------
        

        public bool setTicketPrice(int price)
        {
            if (price <= 2000 && price >= 100) // must be less than 2000 and greater than 100
            {
                ticketPrice = price;
                return true;
            }
            return false;
        }

        public bool setCargoPrice(int price)
        {
            if (price <= 500 && price > 0) // must be less than 500 and greater than 0
            {
                cargoPrice = price;
                return true;
            }
            return false;
        }

        /*public void setTrainName(string name)
        {
            trainName = name;
        }*/

        public bool isStationExist(string name)
        {
            foreach(Station st in stations)
            {
                if (name == st.StationName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool findStation(string st)
        {
            foreach (Station station in stations)
            {
                if (st == station.StationName) // it search the required station in ts1 array
                {
                    Console.WriteLine(trainName + "\t\t" + station.Ath+ ":" + station.Atm + "\t" + station.Dth + ":" + station.Dtm);
                    return true;
                }
            }
            return false;
        }
    }
}
