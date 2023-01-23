using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RMS_V4.BL;

namespace RMS_V4.DL
{
    internal class StationDL
    {
        private static List<string> stationsList = new List<string>();

        public static List<string> getList()
        {
            return stationsList;
        }

        public static string GetSingleSingleByIndex(int idx)
        {
            if (idx >= 0 && idx < stationsList.Count)
            {
                return stationsList[idx];
            }
            return null;
        }

        public static void addIntoList(string station)
        {
            stationsList.Add(station);
        }

        // function for adding new station in array
        public static void add_station_to_array( string st)
        {
            int n1 = 0;

            for (int i = 0; i < stationsList.Count; i++) // loop for checking is name already exist or not
            {

                if (stationsList[i] != st) // checking name in station 1 array
                {
                    n1++;
                    if (n1 == stationsList.Count) // if name does not find
                    {
                        stationsList.Add(st); // than add it in array

                    }
                }
            }
        }

        public static void loadDataFromFile(string path)
        {
            string line;
            // reading stations names from file
            if (File.Exists(path))
            {
                StreamReader st_file = new StreamReader(path);// variable for reading stations name
                while ((line = st_file.ReadLine()) != null)
                {
                   addIntoList(line);
                }
                st_file.Close(); // closing file
            }
        }

        public static void storeDataIntoFile(string path)
        {
            // writing stations names into file
            StreamWriter st_file = new StreamWriter(path);
           
            for (int idx = 0; idx < stationsList.Count; idx++)
            {
                st_file.Write(stationsList[idx]);
                if (idx < stationsList.Count - 1)
                {
                    st_file.WriteLine();
                }
            }
            st_file.Close();
        }

        // view station schedule that wich trains come on station function
        public static int station_schedule_menu(string name)
        {
            // in place of "name" there we pass "user/admin" from function call
            Console.WriteLine(" " + name + " >> View Station Schedule  ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Select any from available stations......");
            // stations name available
            int a = 1;

            // loop for showing all stations name from array
            for (int idx = 0; idx < stationsList.Count; idx++)
            {
                Console.WriteLine(" " + a + ". " + stationsList[idx]);
                a++;
            }
            char sub_op;

            while (true) // loop run until user enter correct option
            {
                Console.Write("Your Option.....:");
                sub_op = char.Parse(Console.ReadLine());

                if (sub_op >= '1' && sub_op <= a + 47) // if option is correct than break
                {
                    break;
                }
                else // if option is incorrect than take input again
                {
                    Console.WriteLine("\nInvalid option ! ");
                    Console.WriteLine("Again select the option ");
                }
            }
            int op = sub_op - 48;
            return op;
        }

        // function for serching the given stations in the stations of trains
        public static void train_station_check(string name, List<Route> route, string station)
        {
            // in place of "name" there we pass "user/admin" from function call
            Console.WriteLine(" " + name + " >> View Station Schedule ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Station Name : " + station);
            Console.WriteLine();
            Console.WriteLine("Train Name\t\tArrival\tDeparture ");

            for (int idx = 0; idx < route.Count; idx++) // loop run for all train station array
            {
                route[idx].findStation(station);
            }
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}
