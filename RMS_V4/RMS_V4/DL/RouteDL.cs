using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS_V4.BL;
using System.IO;

namespace RMS_V4.DL
{
    internal class RouteDL
    {
        private static List<Route> routeList = new List<Route>();

        public static List<Route> getRouteList()
        {
            return routeList;
        }

        public static Route GetSingleRouteByIndex(int idx)
        {
            if(idx >= 0 && idx < routeList.Count)
            {
                return routeList[idx];
            }
            return null;
        }

        public static void addIntoList(Route r)
        {
            routeList.Add(r);
        }

        public static void removeFromList(int index)
        {
            routeList.RemoveAt(index);
        }

        public static bool isTrainNameExist(string trainName)
        {
            foreach(Route r in routeList)
            {
                if(trainName == r.TrainName)
                {
                    return true;
                }
            }
            return false;
        }

        public static void LoadDataFromFile(string path)
        {
            string line;
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    string[] splittedRecord = line.Split(',');

                    int x = 0;
                    string trainName = splittedRecord[x]; // reading train names
                    x++;
                    int count = int.Parse(splittedRecord[x]);
                    x++;

                    List<Station> stations = new List<Station>();

                    for (int i = 0; i < count; i++)
                    {
                        string stationName = splittedRecord[x];
                        x++;
                        int ath = int.Parse(splittedRecord[x]);
                        //read.ath.Add(int.Parse(temp));
                        x++;
                        int atm = int.Parse(splittedRecord[x]);
                        //read.atm.Add(int.Parse(temp));
                        x++;
                        int dth = int.Parse(splittedRecord[x]);
                        //read.dth.Add(int.Parse(temp));
                        x++;
                        int dtm = int.Parse(splittedRecord[x]);
                        //read.dtm.Add(int.Parse(temp));
                        x++;
                        stations.Add(new Station(stationName, ath, atm, dth, dtm));
                    }

                    int ticketPrice = int.Parse(splittedRecord[x]); // raeding trains tickets prices
                    x++;
                    int cargoPrice = int.Parse(splittedRecord[x]);  // reading cargo route of trains
                    x++;

                    Route readRoute = new Route(trainName, stations, ticketPrice, cargoPrice); // temporary for reding from files

                    addIntoList(readRoute); // adding in the list of routes of 
                }
                file.Close(); // closing file after reading data
            }
            else
            {
                Console.WriteLine("File Not Exist");
            }

        }

        public static void storeDataIntoFlie(string path)
        {

            // writing data of train routeCount into file
            StreamWriter file = new StreamWriter(path);
            int idx = 0;
            foreach (Route r in routeList)        // changing index of arrays
            {
                file.Write(r.TrainName + ",");
                file.Write(r.Stations.Count + ",");
                foreach (Station st in r.Stations)
                {

                    file.Write(st.StationName + ",");
                    file.Write(st.Ath + "," + st.Atm + "," + st.Dth + "," + st.Dtm + ",");
                }
                file.Write(r.TicketPrice + "," + r.CargoPrice + ",");
                
                if (idx < routeList.Count - 1)
                {
                    file.WriteLine();
                }
                idx++;
            }
            file.Close(); // closing file

        }

        

    }
}
