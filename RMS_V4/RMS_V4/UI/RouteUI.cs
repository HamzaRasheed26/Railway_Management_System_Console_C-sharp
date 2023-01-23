using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS_V4.BL;
using RMS_V4.DL;

namespace RMS_V4.UI
{
    internal class RouteUI
    {
        // List of trains for selection of only one train from list function
        public static int list_of_trains(string name, string title, List<Route> routeList)
        {

            int a = 1, op;

            RMSUI.head();
            // in place of "name" there we pass "user/admin" from function call
            // or in place of "title" we pass the title accordimg to our need in program
            Console.WriteLine(" {0} >> {1} ", name, title);
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Select any Train from the following.....");
            Console.WriteLine();
            Console.WriteLine("Train names:");
            Console.WriteLine();

            foreach (Route route in routeList) // for printing train names from array
            {
                Console.WriteLine(" {0}. {1} ", a, route.TrainName);
                a++;
            }

            while (true) // loop run until user eneter correct value
            {
                Console.WriteLine();
                Console.Write("Select one option.....:");
                op = int.Parse(Console.ReadLine());

                if (op > a - 1 || op <= 0) // if user enter wrong value
                {
                    Console.WriteLine("\nInvalid Option !");
                    Console.WriteLine("Again Input ");
                }
                else // if correct value than break
                {
                    break;
                }
            }
            op--;
            return op;
        }

        // view train routeCount station name and arrival departure times function
        public static void view_train_route_detail(string name, string title, Route route)
        {

            // in place of "name" there we pass "user/admin" from function call
            // or in place of "title" we pass the title accordimg to our need in program
            Console.WriteLine(" " + name + " >> " + title);
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Train Name : " + route.TrainName);
            Console.WriteLine();

            // printing the stations name their arrival times and departure time
            Console.WriteLine(" Stations\tArrival\t\tDeparture ");
            Console.WriteLine();

            foreach(Station st in route.Stations)
            {
                st.print();
            }

            Console.WriteLine();
            Console.Write(" Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // Add new train route function
        public static Route add_train_route()
        {
            //Route takeData = new Route();

            string trainName;
            int ticketPrice, cargoPrice;

            List<Station> stations = new List<Station>();

            Console.WriteLine(" Admin >> Add new Train Route");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.Write(" Enter train name :"); // train name
            trainName = Console.ReadLine();

            bool entry = true;
            int count = 1;
            while (entry)
            {
                Console.WriteLine(" *** For Station " + count);

                Station station = StationUI.takeStationInput();

                StationDL.add_station_to_array(station.StationName); // calling an functin for adding station to stations array
                stations.Add(station);
                count++;
                if (count > 4)
                {
                    Console.WriteLine("\n Do You want to add another station...(y/n)...:");
                    string op = Console.ReadLine();
                    if (op != "y" && op != "Y")
                    {
                        break;
                    }
                }

            }

            while (true) // validation on correcrt ticket price
            {
                Console.Write(" Set Ticket Price :"); // ticket price for train
                ticketPrice = int.Parse(Console.ReadLine());

                if (ticketPrice <= 2000 && ticketPrice > 100) // must be less than 2000 and greater than 100
                {
                    break;
                }
                Console.WriteLine(" Train Ticket Price Cannot be greater than 2000 Rs and cannot be less than 100 Rs. ");
            }

            while (true) // validation on correcrt cargo price
            {
                Console.Write(" Set cargo rate per kg :"); // cargo rate for that train
                cargoPrice = int.Parse(Console.ReadLine());

                if (cargoPrice <= 500 && cargoPrice > 0) // must be less than 500 and greater than 0
                {
                    break;
                }
                Console.WriteLine(" Price must be less than 500 per kg and greater than 0.");
            }

            Console.WriteLine();
            Console.WriteLine("*** New Route Successfully Added ***");

            Route takeData = new Route(trainName, stations, ticketPrice, cargoPrice);

            return takeData;
        }

        // function for daleting already exist route
        public static bool delete_route(string trainName)
        {
            char flag;
            bool check;

            Console.WriteLine();
            // asking are you sure
            Console.WriteLine("Are you sure you want to delete the route! ");
            Console.Write("Press 1 for Yes or Press any key for Not ");
            flag = char.Parse(Console.ReadLine());

            if (flag == '1') // if he want to delete route
            {
                Console.WriteLine("\n Route : " + trainName);
                Console.WriteLine(" *** Deleted Successfully *** ");
                check = true;
            }
            else
            {
                check = false;
            }
            Console.WriteLine();
            Console.WriteLine("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
            return check;
        }

        // Function for changing already exist Train name
        public static string  change_train_name(string oldName)
        {
            
            
            Console.WriteLine("\n\n");
            Console.WriteLine(" Old Train Name " + oldName); // old train name
            string trainName;
            while (true)
            {
                
                Console.WriteLine(" Enter New train name ");
                trainName = Console.ReadLine(); // taking input of new trin name

                if(trainName == oldName)
                {
                    break;
                }
                else if(RouteDL.isTrainNameExist(trainName))
                {
                    Console.WriteLine();
                    Console.WriteLine(" This Train already exist !");
                    Console.WriteLine(" Enter another name ");
                }
                else  // if name met all conditions than change name
                {
                    //routeList[idx].changeTrainName(trainName);
                    break;
                }
            }

            Console.WriteLine(" Train name changed Succesfully.");
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
            return trainName;
        }

        // function for changing stations of alraedy exist train route
        public static List<Station> change_train_stations(Route route)
        {
            

            Console.WriteLine();
            Console.WriteLine("Train Name : " + route.TrainName);

            List<Station> stationList = new List<Station>();

            for (int y = 0; y < route.Stations.Count; y++)
            {
                Console.WriteLine(" *** For Station " + (y + 1));
                Console.WriteLine("Old Station " + (y + 1 ) + " Name : " + route.Stations[y]);

                Station station = StationUI.takeStationInput();

                StationDL.add_station_to_array(station.StationName); // calling an functin for adding station to stations array
                stationList.Add(station);
            }

            Console.WriteLine(" Train Stations changed Succesfully.");
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();

            return stationList;
        }

        // set ticket prices function
        public static void set_ticket_price(Route route)
        {
            Console.WriteLine(" Admin >> Set Ticket Prices");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Train Name : " + route.TrainName);
            Console.WriteLine(" Old ticket price is : " + route.TicketPrice); // old price of ticket
            while (true)
            {
                Console.Write(" Enter new ticket price :"); // taking input new price of ticket
                int price = int.Parse(Console.ReadLine());

                if (route.setTicketPrice(price)) // must be less than 2000 and greater than 100
                {
                    break;
                }
                Console.WriteLine(" You can not enter price more than 2000 and less than 100. ");
            }
        }

        // set freight prices of trains function
        public static void set_freight_rate(Route route)
        {

            Console.WriteLine(" Admin >> Set Freight Rate ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name : " + route.TrainName);
            Console.WriteLine();
            Console.WriteLine("Old cargo rate of train : " + route.CargoPrice); // old price of cargo
            while (true)
            {
                Console.Write("Enter new cargo rate per kg :"); // taking input new price of cargo
                int price = int.Parse(Console.ReadLine());
                if (route.setCargoPrice(price)) // must be less than 500 and greater than 0
                {
                    break;
                }
                Console.WriteLine("You cannot enter rate more than 500 per kg and less than 0.");
            }
        }

        // function for viewing tickets pries of trains
        public static void view_tickets_price(List<Route> routeList)
        {
            RMSUI.head();

            Console.WriteLine(" User >> View Tickets Price ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Train Name\t\tTicket Price");
            Console.WriteLine();

            int a = 1;
            // prints all train names with their tickets prices
            foreach (Route route in routeList)
            {
                Console.WriteLine(" " + a + ". " + route.TrainName + "\t\t" + route.TicketPrice);
                a++;
            }
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }



        // function for displaying prices of freight/cargo
        public static void view_freight_rate(List<Route> routeList)
        {
            RMSUI.head();
            Console.WriteLine(" User >> View Freight Rates ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name\t\tRate/kg   ");
            Console.WriteLine();

            int a = 1;
            // prints all train names with their cargo/freight prices per kg
            foreach (Route route in routeList)
            {
                Console.WriteLine(" " + a + ". " + route.TrainName + "\t" + route.CargoPrice);
                a++;
            }

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for edit already exist route
        public static char edit_route()
        {
            char option;
            RMSUI.head();
            Console.WriteLine(" Admin >> Edit Route ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();

            Console.WriteLine(" 1. Delete Route ");
            Console.WriteLine(" 2. Modify Route ");
            Console.WriteLine(" 3. Exit ");

            while (true) // validation on option
            {
                Console.Write("\n Your Option : ");
                option = char.Parse(Console.ReadLine());

                if (option >= '1' && option <= '3')
                {
                    return option; // return option selected
                }
                Console.WriteLine(" Invalid Option! ");
                Console.WriteLine(" Again Input ");
            }
        }



        // function for mofdify all ready exist train route
        public static char modify_route()
        {
            Console.Clear();
            RMSUI.head();
            Console.WriteLine(" Admin >> Modify Route ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" 1. Change Train Name ");
            Console.WriteLine(" 2. Change Stations ");
            Console.WriteLine(" 3. Exit ");
            char op;
            while (true) // validation on option
            {
                Console.Write(" Your Option... : ");
                op = char.Parse(Console.ReadLine());

                if (op >= '1' && op <= '3')
                {
                    return op;
                }
            }
        }
    }
}
