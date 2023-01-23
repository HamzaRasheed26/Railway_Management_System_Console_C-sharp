using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS_V4.BL;
using RMS_V4.DL;

namespace RMS_V4.UI
{
    internal class TrainCargoUI
    {
        // function for booking cargo
        public static TrainCargo book_cargo(Route route)
        {
            //TrainCargo read = new TrainCargo();

            string trainName, from, to;
            float price, weight;
            int day, month, year;

            Console.Clear();
            RMSUI.head();
            Console.WriteLine(" User >> Book Cargo ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name :" + route.TrainName);
            trainName = route.TrainName;

            // this line print the stations name that are available on this train route
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Stations available :");
            int idx = 1;
            foreach (Station st in route.Stations)
            {
                Console.WriteLine("\t" + idx  + ". " + st.StationName);
                idx++;
            }
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Select the station from above...\n");

            while (true) // this loop run until user enter correct value
            {
                Console.Write(" From Station :");
                from = Console.ReadLine();
                
                // check station name entered by user is valid or not
                if (route.isStationExist(from))
                {
                    break;
                }
                else // if station name does not match
                {
                    Console.WriteLine(" Invalid Station Name !");
                    Console.WriteLine(" Again Input ");
                }
            }
            while (true) // this loop run until user enter correct value
            {
                Console.Write(" To Station :");
                to = Console.ReadLine();
               
                // check station name entered by user is valid or not
                if (route.isStationExist(to))
                {
                    break;
                }
                else // if station name does not match
                {
                    Console.WriteLine(" Invalid Station Name !");
                    Console.WriteLine(" Again Input ");
                }
            }

            while (true) // validation for date
            {
                Console.Write(" Enter date ( dd mm yyyy) :");
                day = int.Parse(Console.ReadLine());
                month = int.Parse(Console.ReadLine());
                year = int.Parse(Console.ReadLine());

                if(TrainCargoDL.isDateValid(day, month, year))
                {
                    break;
                }
                Console.WriteLine("\n Invalid Date ! ");
                Console.WriteLine(" Again enter date please.");
            }
            Console.WriteLine();
            Console.WriteLine("Price per kg :" + route.CargoPrice);

            while (true) // validation on weight
            {
                Console.Write("Enter the cargo weight (kg) :");
                weight = float.Parse(Console.ReadLine());

                if (weight > 500 || weight <= 0) // user canot enter more than 500 kg weight
                {
                    Console.WriteLine("You can not add weight more than 500 kg ! ");
                    Console.WriteLine("Again Input ");
                }
                else // if less than 500
                {
                    break;
                }
            }

            // calculkating cargo price by formula
            price = route.CargoPrice * weight;

            Console.WriteLine();
            Console.WriteLine("You have to pay :" + price);
            Console.Write("You want to book cargo (1 for yes, 0 for not) :");
            char op;
            op = char.Parse(Console.ReadLine());

            TrainCargo readData = new TrainCargo(trainName, from, to, weight, price, 0, day, month, year);

            if (confirming_book_cargo(op, readData))
            {
                return readData;
            }
            else
            {
                return null;
            }
        }

        // function for printing on screen that cargo booked or not
        public static bool confirming_book_cargo(char flag, TrainCargo read)
        {
            bool check;
            if (flag == '1') // message of cargo booked
            {
                Console.WriteLine();
                Console.Clear();
                RMSUI.head();
                Console.WriteLine(" User >> Booked cargo ");
                Console.WriteLine("_____________________________________________________________");
                Console.WriteLine();
                Console.WriteLine(" Your Cargo Booked Succesfully ***");
                Console.WriteLine();


                read.print();

                Console.WriteLine(" **** Your cargo succesfully booked ***");

                Console.WriteLine();
                check = true;
            }
            else // if not booked creating arrays index null
            {
                Console.WriteLine();
                Console.WriteLine(" Your cargo not booked ! ");
                Console.WriteLine();
                check = false;
            }

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
            return check;
        }

        public static void my_booked_cargo(List<TrainCargo> sortedCargoList)
        {
            RMSUI.head();
            Console.WriteLine(" User >> My Booked Cargo ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();

            bool check = false;
            if (sortedCargoList != null)
            {

                check = TrainCargoDL.print_booked_cargo(sortedCargoList); // calling function for displaying tickets
            }
            if(check == false)
            {
                Console.WriteLine("  You Have No Cargo Booked ! ");
            }
            Console.WriteLine();
            Console.Write("  Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}
