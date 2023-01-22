
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RMS_V2.BL;

namespace RMS_V1
{
    internal class Program
    {
        

                  

        //-----------------------------  main function------------------------------------------
        static void Main(string[] args)
        {
            //______________________ Data structure _______________
            string password = "";
            string notice = "No_notice";

            List<RouteType> route = new List<RouteType>();
            List<TicketType> ticket = new List<TicketType>();
            List<BookingType> cargo = new List<BookingType>();
            List<string> stations = new List<string>();
           

            // -------------------- Actual Program Runs From Here

            load_data(route,ref password, stations, ticket, cargo);
            string user;

            while (true) // loop for main menu
            {

                // calling login function
                user = login_page(password);

                // __________________________ Admin Portion ________________________
                if (user == "admin")
                {
                    char option;

                    while (true) // loop for admin option
                    {
                        Console.Clear();
                        head();
                        // calling admin menu
                        option = Admin_Menu();
                        Console.Clear();
                        int sub_op;

                        if (option == '1')
                        {
                            // admin menu option 1 starts

                            // calling function for printing list of trains
                            sub_op = list_of_trains("Admin", "View Train Route", route);
                            // calling function for further train detail
                            view_train_route_detail("Admin", "View Train Route", sub_op, route);

                            // admin menu option 1 ends
                        }
                        else if (option == '2')
                        {
                            // admin menu option 2 starts

                            
                             // function for adding train
                                add_train_route(route, stations);
                           

                            // admin menu option 2 ends
                        }
                        else if (option == '3')
                        {
                            // admin menu option 3 starts

                            sub_op = edit_route(); // menu of edit route

                            if (sub_op == '1') // for option 1
                            {
                                delete_route(route); // deleting route
                            }
                            else if (sub_op == '2') // for option 2
                            {
                                sub_op = modify_route(); // modifying route

                                if (sub_op == '1')
                                {
                                    change_train_name(route); // changing train name
                                }
                                else if (sub_op == '2')
                                {
                                    change_train_stations(route, stations); // changing train station
                                }
                            }

                            // admin menu option 3 ends
                        }
                        else if (option == '4')
                        {
                            // admin menu option 3 starts

                            sub_op = list_of_trains("Admin", "View Train Route", route);
                            set_ticket_price(sub_op,route);

                            // admin menu option 3 ends
                        }
                        else if (option == '5')
                        {
                            // admin menu option 4 starts

                            sub_op = list_of_trains("Admin", "View Train Route", route);
                            set_freight_rate(sub_op,route);

                            // admin menu option 4 ends
                        }
                        else if (option == '6')
                        {
                            // admin menu option 5 starts

                            sub_op = station_schedule_menu("Admin", stations);
                            train_station_check("Admin", sub_op,route, stations);

                            // admin menu option 5 ends
                        }
                        else if (option == '7')
                        {
                            // admin menu option 6 starts

                            add_notice(notice);

                            // admin menu option 6 ends
                        }
                        else if (option == '8')
                        {
                            // admin menu option 7 starts

                            view_employers_data();

                            // admin menu option 7 ends
                        }
                        else if (option == '9')
                        {
                            // admin menu exit point
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input !");
                            Console.Write("Press any key for continue....");
                            Console.ReadKey();
                            Console.WriteLine();
                        }
                    }
                }
                // ________________________ User Portion ______________________________________
                else if (user == "user")
                {
                    string option;
                    while (true)
                    {
                        Console.Clear();
                        head();
                        option = user_menu();
                        Console.Clear();
                        int sub_op;

                        if (option == "1")
                        {
                            // user menu option 1 starts

                            sub_op = list_of_trains("User", "View Train Route", route);
                            view_train_route_detail("User", "View Train Route", sub_op, route);

                            // user menu option 1 ends
                        }
                        else if (option == "2")
                        {
                            // user menu option 2 starts

                            sub_op = station_schedule_menu("User", stations);
                            train_station_check("User", sub_op, route, stations);

                            // user menu option 2 ends
                        }
                        else if (option == "3")
                        {
                            // user menu option 3 starts

                            view_tickets_price(route);

                            // user menu option 3 ends
                        }
                        else if (option == "4")
                        {
                            // user menu option 4 starts

                            
                                sub_op = list_of_trains("User", "Buy Ticket", route);
                                buy_ticket(sub_op, route, ticket);
                            

                            // user menu option 4 ends
                        }
                        else if (option == "5")
                        {
                            // user menu option 5 starts

                            my_tickets(ticket);

                            // user menu option 5 ends
                        }
                        else if (option == "6")
                        {
                            // user menu option 6 starts

                            view_freight_rate(route);

                            // user menu option 6 ends
                        }
                        else if (option == "7")
                        {
                            // user menu option 7 starts

                                sub_op = list_of_trains("User", "Book Cargo", route);
                                book_cargo(sub_op, route, cargo);
                            
                            // user menu option 7 ends
                        }
                        else if (option == "8")
                        {
                            // user menu option 8 starts

                            my_booked_cargo(cargo);

                            // user menu option 8 ends
                        }
                        else if (option == "9")
                        {
                            // user menu option 9 starts

                            view_notice(notice);

                            // user menu option 9 ends
                        }
                        else if (option == "10")
                        {
                            // user menu exit point
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input !");
                            Console.WriteLine("Press any key for continue....");
                            Console.ReadKey();
                            Console.WriteLine();
                        }
                    }
                }
                // _____________________________ Logout statement ___________________________
                else if (user == "logout")
                {
                    break;
                }
                // if invalid input
                else
                {
                    Console.WriteLine("Invalid Input!");
                }
            }
            store_data(route, password, stations, ticket, cargo);
            developer();
        }

        // ________________________________ Function Definitions _____________________________________________________________________

        // Railway management system head function
        static void head()
        {
            Console.WriteLine("*************************************************************");
            Console.WriteLine("*                  RAILWAY MANAGEMENT SYSTEM                *");
            Console.WriteLine("*************************************************************");
            Console.WriteLine();
        }


        // login page function
        static string login_page(string password)
        {
            while (true) // loop run until user enter wrong value
            {
                Console.Clear();
                head();

                string login;

                Console.WriteLine(" Login Page >>");
                Console.WriteLine("_____________________________________________________________");
                Console.WriteLine();
                Console.WriteLine(" 1- Admin ");
                Console.WriteLine(" 2- User ");
                Console.WriteLine(" 3- Exit ");
                Console.Write("Your Option.....");
                login = Console.ReadLine();

                if (login == "1") // if user press 1
                {
                    // lines for taking password input
                    Console.Write("Enter Password.........(123)..........:");
                    string pwd;
                    pwd = Console.ReadLine();
                    if (password == pwd) // if password is correct
                    {
                        return "admin";
                    }
                    else // if password is wrong
                    {
                        Console.WriteLine("Invalid Password!");
                        Console.Write("Press any key for continue....");
                        Console.ReadKey();
                        Console.WriteLine();
                    }
                }
                else if (login == "2") // if user press 2 it return user
                {
                    return "user";
                }
                else if (login == "3") // if user press 3 it return logout
                {
                    return "logout";
                }
                else // for wrong input
                {
                    Console.WriteLine("Invalid Input!");
                }


            }

        }

        // Admin menu page function
        static char Admin_Menu()
        {
            char option;
            Console.WriteLine(" Admin >> Menu");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Select one of the following options........");
            Console.WriteLine();
            Console.WriteLine(" 1. View of all route of trains");
            Console.WriteLine(" 2. Add new route of train");
            Console.WriteLine(" 3. Edit Route ");
            Console.WriteLine(" 4. Set tickets prices");
            Console.WriteLine(" 5. Set Freight rates");
            Console.WriteLine(" 6. View schedule of stations");
            Console.WriteLine(" 7. Add important notices");
            Console.WriteLine(" 8. View employers data");
            Console.WriteLine(" 9. EXit");
            Console.WriteLine();
            Console.Write("Select any option........:");
            option = char.Parse(Console.ReadLine());
            return option;
        }

        // List of trains for selection of only one train from list function
        static int list_of_trains(string name, string title, List<RouteType> route)
        {

            int a = 1, op;

            head();
            // in place of "name" there we pass "user/admin" from function call
            // or in place of "title" we pass the title accordimg to our need in program
            Console.WriteLine(" {0} >> {1} ", name, title);
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Select any Train from the following.....");
            Console.WriteLine();
            Console.WriteLine("Train names:");
            Console.WriteLine();

            for (int idx = 0; idx < route.Count; idx++) // for printing train names from array
            {
                Console.WriteLine(" {0}. {1} ", a, route[idx].train);
                //cout << " " << a << ". " << train[idx] << endl;
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

            return op;
        }

        // view train routeCount station name and arrival departure times function
        static void view_train_route_detail(string name, string title, int index, List<RouteType> route)
        {
            Console.Clear();
            index = index - 1;
            head();
            // in place of "name" there we pass "user/admin" from function call
            // or in place of "title" we pass the title accordimg to our need in program
            Console.WriteLine(" " + name + " >> " + title);
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Train Name : " + route[index].train);
            Console.WriteLine();

            // printing the stations name their arrival times and departure time
            Console.WriteLine(" Stations\tArrival\t\tDeparture ");
            Console.WriteLine();
            Console.WriteLine(" " + route[index].ts1 + "\t" + route[index].ts1_ath + ":" + route[index].ts1_atm + "\t\t" + route[index].ts1_dth + ":" + route[index].ts1_dtm);
            Console.WriteLine(" " + route[index].ts2 + " \t" + route[index].ts2_ath + ":" + route[index].ts2_atm + "\t\t" + route[index].ts2_dth + ":" + route[index].ts2_dtm);
            Console.WriteLine(" " + route[index].ts3 + " \t" + route[index].ts3_ath + ":" + route[index].ts3_atm + "\t\t" + route[index].ts3_dth + ":" + route[index].ts3_dtm);
            Console.WriteLine(" " + route[index].ts4 + "\t" + route[index].ts4_ath + ":" + route[index].ts4_atm + "\t\t" + route[index].ts4_dth + ":" + route[index].ts4_dtm);
            Console.WriteLine();
            Console.Write(" Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // Add new train route function
        static void add_train_route(List<RouteType> route, List<string> stations)
        {
            RouteType takeData = new RouteType();
            char flag = '1';
            while (flag == '1') // loop run until user want to add routeCount
            {
                Console.Clear();
                head();
                Console.WriteLine(" Admin >> Add new Train Route");
                Console.WriteLine("_____________________________________________________________");
                Console.WriteLine();
                Console.Write(" Enter train name :"); // train name
                takeData.train = Console.ReadLine();
                Console.Write("\n Station-1 name :"); // station 1 name
                takeData.ts1 = Console.ReadLine();
                Console.WriteLine(" Note : use 24 hours format for input time ");

                while (true) // validation on correcrt time
                {
                    Console.Write(" Arrival Time( hh:mm ) :"); // arrival time station 1
                    takeData.ts1_ath = int.Parse(Console.ReadLine());             // hour
                    takeData.ts1_atm = Console.Read();             // minute
                    if (takeData.ts1_ath  >= 1 && takeData.ts1_ath  <= 24 && takeData.ts1_atm  >= 0 && takeData.ts1_atm  <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }
                while (true) // validation on correcrt time
                {
                    Console.Write(" Departure Time( hh:mm ) :"); // arrival time station 1
                    takeData.ts1_dth = int.Parse(Console.ReadLine());               // hour
                    takeData.ts1_dtm= int.Parse(Console.ReadLine());               // minute
                    if (takeData.ts1_dth>= 1 && takeData.ts1_dth <= 24 && takeData.ts1_dtm >= 0 && takeData.ts1_dtm <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }

                Console.Write("\n Station-2 name :"); // station 2 name
                takeData.ts2 = Console.ReadLine();
                while (true) // validation on correcrt time
                {
                    Console.Write(" Arrival Time( hh:mm ) :"); // arrival time station 2
                    takeData.ts2_ath  = int.Parse(Console.ReadLine());             // hour
                    takeData.ts2_atm  = int.Parse(Console.ReadLine());             // minute
                    if (takeData.ts2_ath >= 1 && takeData.ts2_ath <= 24 && takeData.ts2_atm >= 0 && takeData.ts2_atm <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }
                while (true) // validation on correcrt time
                {
                    Console.Write(" Departure Time( hh:mm ) :"); // arrival time station 2
                    takeData.ts2_dth = int.Parse(Console.ReadLine());               // hour
                    takeData.ts2_dtm = int.Parse(Console.ReadLine());               // minute
                    if (takeData.ts2_dth  >= 1 && takeData.ts2_dth <= 24 && takeData.ts2_dtm >= 0 && takeData.ts2_dtm <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }

                Console.Write("\n Station-3 name :"); // station 3 name
                takeData.ts3 = Console.ReadLine();
                while (true) // validation on correcrt time
                {
                    Console.Write(" Arrival Time( hh:mm ) :"); // arrival time station 3
                    takeData.ts3_ath = int.Parse(Console.ReadLine());             // hour
                    takeData.ts3_atm = int.Parse(Console.ReadLine());             // minute
                    if (takeData.ts3_ath >= 1 && takeData.ts3_ath <= 24 && takeData.ts3_atm >= 0 && takeData.ts3_atm <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }
                while (true) // validation on correcrt time
                {
                    Console.Write(" Departure Time( hh:mm ) :"); // arrival time station 3
                    takeData.ts3_dth = int.Parse(Console.ReadLine());               // hour
                    takeData.ts3_dtm = int.Parse(Console.ReadLine());               // minute
                    if (takeData.ts3_dth >= 1 && takeData.ts3_dth <= 24 && takeData.ts3_dtm >= 0 && takeData.ts3_dtm <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }

                Console.Write("\n Station-4 name :"); // station 4 name
                takeData.ts4 = Console.ReadLine();
                while (true) // validation on correcrt time
                {
                    Console.Write(" Arrival Time( hh:mm ) :"); // arrival time station 2
                    takeData.ts4_ath = int.Parse(Console.ReadLine());             // hour
                    takeData.ts4_atm = int.Parse(Console.ReadLine());             // minute
                    if (takeData.ts4_ath >= 1 && takeData.ts4_ath <= 24 && takeData.ts4_atm >= 0 && takeData.ts4_atm <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }
                while (true) // validation on correcrt time
                {
                    Console.Write(" Departure Time( hh:mm ) :"); // arrival time station 2
                    takeData.ts4_dth = int.Parse(Console.ReadLine());               // hour
                    takeData.ts4_dtm = int.Parse(Console.ReadLine());               // minute
                    if (takeData.ts4_dth >= 1 && takeData.ts4_dth <= 24 && takeData.ts4_dtm >= 0 && takeData.ts4_dtm <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }

                while (true) // validation on correcrt ticket price
                {
                    Console.Write(" Set Ticket Price :"); // ticket price for train
                    takeData.tticket = int.Parse(Console.ReadLine());

                    if (takeData.tticket <= 2000 && takeData.tticket > 100) // must be less than 2000 and greater than 100
                    {
                        break;
                    }
                    Console.WriteLine(" Train Ticket Price Cannot be greater than 2000 Rs and cannot be less than 100 Rs. ");
                }

                while (true) // validation on correcrt cargo price
                {
                    Console.Write(" Set cargo rate per kg :"); // cargo rate for that train
                    takeData.tcargo= int.Parse(Console.ReadLine());

                    if (takeData.tcargo <= 500 && takeData.tcargo > 0) // must be less than 500 and greater than 0
                    {
                        break;
                    }
                    Console.WriteLine(" Price must be less than 500 per kg and greater than 0.");
                }

                route.Add(takeData); // readed data is added into list of route

                Console.WriteLine();
                Console.WriteLine("*** New Route Successfully Added ***");
                add_station_to_array(stations, route, route.Count); // calling an functin for adding station to stations array

                //routeCount++; // increament in route counter

                Console.Write(" Press 1 for adding another route or any other for exit : ");
                flag = char.Parse(Console.ReadLine());
            }
        }

        // set ticket prices function
        static void set_ticket_price(int index , List<RouteType> route)
        {
            index--;
            Console.Clear();
            head();
            Console.WriteLine(" Admin >> Set Ticket Prices");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name : " + route[index].train);
            Console.WriteLine("Old ticket price is : " + route[index].tticket ); // old price of ticket
            while (true)
            {
                Console.Write("Enter new ticket price :"); // taking input new price of ticket
                route[index].tticket  = int.Parse(Console.ReadLine());

                if (route[index].tticket  <= 2000 && route[index].tticket  >= 100) // must be less than 2000 and greater than 100
                {

                    break;
                }
                Console.WriteLine("You can not enter price more than 2000 and less than 100. ");
            }
        }

        // set freight prices of trains function
        static void set_freight_rate(int index, List<RouteType> route)
        {
            index--;
            Console.Clear();
            head();
            Console.WriteLine(" Admin >> Set Freight Rate ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name : " + route[index].train );
            Console.WriteLine();
            Console.WriteLine("Old cargo rate of train : " + route[index].tcargo ); // old price of cargo
            while (true)
            {
                Console.Write("Enter new cargo rate per kg :"); // taking input new price of cargo
                route[index].tcargo = int.Parse(Console.ReadLine());
                if (route[index].tcargo <= 500 && route[index].tcargo > 0) // must be less than 500 and greater than 0
                {
                    break;
                }
                Console.WriteLine("You cannot enter rate more than 500 per kg and less than 0.");
            }
        }

        // view station schedule that wich trains come on station function
        static int station_schedule_menu(string name, List<string> stations)
        {

            head();
            // in place of "name" there we pass "user/admin" from function call
            Console.WriteLine(" " + name + " >> View Station Schedule  ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Select any from available stations......");
            // stations name available
            int a = 1;

            // loop for showing all stations name from array
            for (int idx = 0; idx < stations.Count ; idx++)
            {
                Console.WriteLine(" " + a + ". " + stations[idx]);
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
        static void train_station_check(string name, int index, List<RouteType> route, List<string> stations)
        {
            index--;
            Console.Clear();
            head();
            // in place of "name" there we pass "user/admin" from function call
            Console.WriteLine(" " + name + " >> View Station Schedule ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Station Name : " + stations[index]);
            Console.WriteLine();
            Console.WriteLine("Train Name\t\tArrival\tDeparture ");

            for (int idx = 0; idx < route.Count; idx++) // loop run for all train station array
            {
                if (stations[index] == route[idx].ts1) // it search the required station in ts1 array
                {
                    Console.WriteLine(route[idx].train + "\t\t" + route[idx].ts1_ath + ":" + route[idx].ts1_atm + "\t" + route[idx].ts1_dth + ":" + route[idx].ts1_dtm);
                }
                if (stations[index] == route[idx].ts2) // it search the required station in ts2 array
                {
                    Console.WriteLine(route[idx].train + "\t\t" + route[idx].ts2_ath  + ":" + route[idx].ts2_atm  + "\t" + route[idx].ts2_dth  + ":" + route[idx].ts2_dtm);
                }
                if (stations[index] == route[idx].ts3 ) // it search the required station in ts3 array
                {
                    Console.WriteLine(route[idx].train + "\t\t" + route[idx].ts3_ath + ":" + route[idx].ts3_atm + "\t" + route[idx].ts3_dth + ":" + route[idx].ts3_dtm);
                }
                if (stations[index] == route[idx].ts4) // it search the required station in ts4 array
                {
                    Console.WriteLine(route[idx].train + "\t\t" + route[idx].ts4_ath + ":" + route[idx].ts4_atm + "\t" + route[idx].ts1_dth + ":" + route[idx].ts1_dtm);
                }
            }
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // Function for posting notices for user
        static void add_notice(string notice)
        {

            head();

            Console.WriteLine(" Admin >> Add Notice ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Write your notice here.....:");
            Console.WriteLine();

            notice = Console.ReadLine(); // string varaible for taking  notice as input

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // this function is hardcode its only print data of employers
        // we cannot edit this data
        static void view_employers_data()
        {
            head();

            Console.WriteLine(" Admin >> View Employers Data ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("      Train Drivers  ");
            Console.WriteLine(" 1. Ahmed       2. Sajid   ");
            Console.WriteLine(" 3. Ali         4. Akhtar  ");
            Console.WriteLine(" 5. Hamid       6. Asif    ");
            Console.WriteLine();
            Console.WriteLine("      Train Police");
            Console.WriteLine(" 1. Inspector Hassan");
            Console.WriteLine(" 2. Sub Inspector Faheem");
            Console.WriteLine(" 3. Sub Inspector Qasim");
            Console.WriteLine(" 4. Sub Inspector Umar");
            Console.WriteLine(" 5. Sub Inspector Taha");
            Console.WriteLine();
            Console.WriteLine("     Station Incharge");
            Console.WriteLine(" 1. Babar");
            Console.WriteLine(" 2. Rizwan");
            Console.WriteLine(" 3. Fakhar");
            Console.WriteLine(" 4. Asim");
            Console.WriteLine(" 5. Zia");
            Console.WriteLine(" 6. Zohaib");
            Console.WriteLine(" 7. Talha");

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for showing user menu on screen
        static string user_menu()
        {
            string option;
            Console.WriteLine(" User >> Menu");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Select one of the following options........");
            Console.WriteLine();
            Console.WriteLine(" 1. View of all route of trains");
            Console.WriteLine(" 2. View Stations Schedule");
            Console.WriteLine(" 3. View tickets prices");
            Console.WriteLine(" 4. Buy Tickets");
            Console.WriteLine(" 5. View My Tickets");
            Console.WriteLine(" 6. View Freight Rates");
            Console.WriteLine(" 7. Book Cargo");
            Console.WriteLine(" 8. View My Booked Cargo");
            Console.WriteLine(" 9. View Notices");
            Console.WriteLine(" 10. EXit");
            Console.WriteLine();
            Console.Write("Select any option........:");
            option = Console.ReadLine();
            return option;
        }

        // function for viewing tickets pries of trains
        static void view_tickets_price(List<RouteType> route)
        {
            head();

            Console.WriteLine(" User >> View Tickets Price ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Train Name\t\tTicket Price");
            Console.WriteLine();

            int a = 1;
            // prints all train names with their tickets prices
            for (int idx = 0; idx < route.Count; idx++)
            {
                Console.WriteLine(" " + a + ". " + route[idx].train + "\t\t" + route[idx].tticket);
                a++;
            }
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for buying ticket of train
        static void buy_ticket(int index , List<RouteType> route, List<TicketType> ticket)
        {
            TicketType buy = new TicketType();
            index--;
            buy.t_name = route[index].train;

            Console.Clear();
            head();

            Console.WriteLine(" User >> Buy Tickets ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name :" + buy.t_name );

            // this line print the stations name that are available on this train route
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Stations available :");
            Console.WriteLine(" 1." + route[index].ts1 + "\t 2." + route[index].ts2);
            Console.WriteLine(" 3." + route[index].ts3+ "\t 4." + route[index].ts4);
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Select the station from above...");
            Console.WriteLine(" ");

            while (true) // this loop run until user enter correct value
            {
                Console.Write(" From Station : ");
                buy.from  = Console.ReadLine();
                // check station name entered by user is valid or not
                if (buy.from == route[index].ts1 || buy.from  == route[index].ts2 || buy.from  == route[index].ts3 || buy.from == route[index].ts4)
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
                buy.to  = Console.ReadLine();
                // check station name entered by user is valid or not
                if (buy.to  == route[index].ts1 || buy.to  == route[index].ts2 || buy.to  == route[index].ts3 || buy.to  == route[index].ts4)
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
                buy.day = int.Parse(Console.ReadLine());
                buy.month  = int.Parse(Console.ReadLine());
                buy.year  = int.Parse(Console.ReadLine());

                // check on year
                if (buy.year == 2022)
                {
                    // check on month
                    if (buy.month == 1 || buy.month  == 3 || buy.month  == 5 || buy.month  == 7 || buy.month  == 8 || buy.month  == 10 || buy.month == 12)
                    {
                        // check on day range from 1 to 31
                        if (buy.day  >= 1 && buy.day <= 31)
                        {
                            break;
                        }
                    }
                    // check on month
                    else if (buy.month  == 4 || buy.month  == 6 || buy.month  == 9 || buy.month  == 11)
                    {
                        // check on  day range from 1 to 30
                        if (buy.day >= 1 && buy.day  <= 30)
                        {
                            break;
                        }
                    }
                    // check on month of febuary
                    else if (buy.month == 2)
                    {
                        // check on day range from 1 to 28
                        if (buy.day >= 1 && buy.day <= 28)
                        {
                            break;
                        }
                    }
                }
                Console.WriteLine("\n Invalid Date ! ");
                Console.WriteLine(" Again enter date please.");
            }

            Console.WriteLine(" Ticket price is :" + route[index].tticket);

            while (true) // validation for quantity
            {
                Console.Write(" Enter quantity of tickets :");
                buy.quantity = int.Parse(Console.ReadLine());

                if (buy.quantity  > 12 || buy.quantity  <= 0) // quantity cannot be greater than 12
                {
                    Console.WriteLine(" Error You cannot buy more than 12 quantity ! ");
                }
                else
                {
                    break;
                }
            }

            buy.price = route[index].tticket * buy.quantity;

            Console.WriteLine("Total price for " + buy.quantity  + " tickets :" + buy.price);
            // confirming for buying ticket
            Console.Write("You want to buy Ticket (1 for yes, 0 for not) :");
            char op;
            op = char.Parse(Console.ReadLine());

            buying_ticket_message(op, buy, ticket);
        }

        // function for printing on screen that ticket buyed
        static void buying_ticket_message(char flag, TicketType buy, List<TicketType> ticket)
        {
            if (flag == '1') // message of buying ticket
            {
                Console.Clear();
                head();
                Console.WriteLine(" User >> Buy Tickets ");
                Console.WriteLine("_____________________________________________________________");
                Console.WriteLine();
                Console.WriteLine(" You buy Ticket Succesfully ***");
                Console.WriteLine();
                Console.WriteLine("  Train Name :" + buy.t_name );
                Console.WriteLine("  From station :" + buy.from);
                Console.WriteLine("  To station :" + buy.to);
                Console.WriteLine("  Date  :" + buy.day+ "-" + buy.month + "-" + buy.year );
                Console.WriteLine("  Quantity :" + buy.quantity);
                Console.WriteLine("  Price :" + buy.price );
                Console.WriteLine(); 
                 Console.WriteLine(" ****Thanks for buying Ticket****");
                buy.date  = buy.day   + (buy.month  * (float)30.417);

                buy.ticket_no = ticket.Count + 1;

                ticket.Add(buy);

                //t++;
            }
            else // if not buyed than 
            {
/*                buy.t_name = " ";
                buy.from = " ";
                buy.to = " ";
                buy.day = 0;
                buy.month  = 0;
                buy.year  = 0;
                buy.date = 0;*/
                Console.WriteLine();
                Console.WriteLine(" Ticket not Buyed !");
            }

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for displaying prices of freight/cargo
        static void view_freight_rate(List<RouteType> route)
        {
            head();
            Console.WriteLine(" User >> View Freight Rates ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name\t\tRate/kg   ");
            Console.WriteLine();

            int a = 1;
            // prints all train names with their cargo/freight prices per kg
            for (int idx = 0; idx < route.Count; idx++)
            {
                Console.WriteLine(" " + a + ". " + route[idx].train + "\t" + route[idx].tcargo );
                a++;
            }

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for booking cargo
        static void book_cargo(int index,List<RouteType> route, List<BookingType> cargo)
        {
            BookingType read = new BookingType();
            index--;
            Console.Clear();
            head();
            Console.WriteLine(" User >> Book Cargo ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name :" + route[index].train);
            read.cargo_train = route[index].train;

            // this line print the stations name that are available on this train route
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Stations available :");
            Console.WriteLine(" 1." + route[index].ts1 + "\t 2." + route[index].ts2);
            Console.WriteLine(" 3." + route[index].ts3 + "\t 4." + route[index].ts4);
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Select the station from above...\n");

            while (true) // this loop run until user enter correct value
            {
                Console.Write(" From Station :");
                read.book_from = Console.ReadLine();
                // check station name entered by user is valid or not
                if (read.book_from == route[index].ts1 || read.book_from == route[index].ts2 || read.book_from == route[index].ts3 || read.book_from == route[index].ts4)
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
                read.book_to = Console.ReadLine();
                // check station name entered by user is valid or not
                if (read.book_to == route[index].ts1 || read.book_to == route[index].ts2 || read.book_to == route[index].ts3 || read.book_to == route[index].ts4 )
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
                read.book_day = int.Parse(Console.ReadLine());
                read.book_month = int.Parse(Console.ReadLine());
                read.book_year = int.Parse(Console.ReadLine());

                float d = read.book_day;
                float m = read.book_month;
                float y = read.book_year;

                // check on year
                if (y == 2022)
                {
                    // check on month
                    if (m == 1 || m == 3 || m == 5 || m == 7 || m == 8 || m == 10 || m == 12)
                    {
                        // check on day range from 1 to 31
                        if (d >= 1 && d <= 31)
                        {
                            break;
                        }
                    }
                    // check on month
                    else if (m == 4 || m == 6 || m == 9 || m == 11)
                    {
                        // check on  day range from 1 to 30
                        if (d >= 1 && d <= 30)
                        {
                            break;
                        }
                    }
                    // check on month of febuary
                    else if (m == 2)
                    {
                        // check on day range from 1 to 28
                        if (d >= 1 && d <= 28)
                        {
                            break;
                        }
                    }
                }
                Console.WriteLine("\n Invalid Date ! ");
                Console.WriteLine(" Again enter date please.");
            }
            Console.WriteLine();
            Console.WriteLine("Price per kg :" + route[index].tcargo );

            while (true) // validation on weight
            {
                Console.Write("Enter the cargo weight (kg) :");
                read.weight = int.Parse(Console.ReadLine());

                if (read.weight > 500 || read.weight <= 0) // user canot enter more than 500 kg weight
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
            read.c_price = route[index].tcargo  * read.weight;

            Console.WriteLine();
            Console.WriteLine("You have to pay :" + read.c_price);
            Console.Write("You want to book cargo (1 for yes, 0 for not) :");
            char op;
            op = char.Parse(Console.ReadLine());
            confirming_book_cargo(op, cargo, read);
        }

        // function for printing on screen that cargo booked or not
        static void confirming_book_cargo(char flag, List<BookingType> cargo, BookingType read)
        {
            if (flag == '1') // message of cargo booked
            {
                Console.WriteLine();
                Console.Clear();
                head();
                Console.WriteLine(" User >> Booked cargo ");
                Console.WriteLine("_____________________________________________________________");
                Console.WriteLine();
                Console.WriteLine(" Your Cargo Booked Succesfully ***");
                Console.WriteLine();
                Console.WriteLine("  Train Name   :" + read.cargo_train);
                Console.WriteLine("  From station :" + read.book_from);
                Console.WriteLine("  To station   :" + read.book_to);
                Console.WriteLine("  Date         :" + read.book_day + "-" + read.book_month + "-" + read.book_year);
                Console.WriteLine("  Weight       :" + read.weight);
                Console.WriteLine("  Price        :" + read.c_price);
                Console.WriteLine();
                Console.WriteLine(" **** Your cargo succesfully booked ***");
                read.book_date = read.book_day + (read.book_month * (float)30.417);
                read.booking_no = cargo.Count + 1;
                cargo.Add(read);
                Console.WriteLine();
                //book++;
            }
            else // if not booked creating arrays index null
            {
                read.weight = 0;
                read.c_price = 0;
                Console.WriteLine();
                Console.WriteLine(" Your cargo not booked ! ");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for viewing notice
        static void view_notice(string notice)
        {
            head();
            Console.WriteLine(" User >> View Notice ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Notice Board......");
            Console.WriteLine();
            Console.WriteLine(notice); // string notice variable

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for viewing my tickets
        static void my_tickets(List<TicketType> ticket)
        {
            head();
            Console.WriteLine(" User >> My Tickets ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();

            sort_my_tickets(ticket); // calling function for sorting

            print_tickets(ticket); // calling function for displaying tickets

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // this function sort the tickets according to the date which comes first
        static void sort_my_tickets(List<TicketType> ticket)
        {
            float min;
            int index = 0;
            for (int idx = 0; idx < ticket.Count; idx++) // it takes value from array and compare with inner loop value
            {
                min = 112122221; // imaginary minimum vlue
                for (int x = idx; x < ticket.Count; x++)
                {
                    if (min > ticket[x].date) // if the selected array value is minimum
                    {
                        min = ticket[x].date;
                        index = x; // storing the index at which we find minimum
                    }
                }

                swaping_ticket(idx, index, ticket); // calling function for swaping
            }
        }

        // function for swaping the values of parallel arrays
        static void swaping_ticket(int value, int idx, List<TicketType> ticket)
        {
            // temporary variables
            float temp;
            int temp2;
            string change;

            /*TicketType temp1 = new TicketType();
            temp1 = ticket[idx];
            ticket.Insert(idx, ticket[value]);
            ticket.Insert(value, temp1);*/

            // swaping date
            temp = ticket[idx].date;
            ticket[idx].date = ticket[value].date;
            ticket[value].date = temp;

            // swaping train name
            change = ticket[idx].t_name;
            ticket[idx].t_name = ticket[value].t_name;
            ticket[value].t_name = change;

            // swaping departure station
            change = ticket[idx].from;
            ticket[idx].from = ticket[value].from;
            ticket[value].from = change;

            // swaping arrival station
            change = ticket[idx].to;
            ticket[idx].to = ticket[value].to;
            ticket[value].to= change;

            // swaping day
            temp = ticket[idx].day;
            ticket[idx].day = ticket[value].day;
            ticket[value].day = temp;

            // swaping month
            temp = ticket[idx].month;
            ticket[idx].month = ticket[value].month;
            ticket[value].month = temp;

            // swaping year
            temp = ticket[idx].year;
            ticket[idx].year = ticket[value].year;
            ticket[value].year = temp;

            // swaping quantity
            temp2 = ticket[idx].quantity;
            ticket[idx].quantity = ticket[value].quantity;
            ticket[value].quantity = temp2;

            // swaping price
            temp2 = ticket[idx].price;
            ticket[idx].price = ticket[value].price;
            ticket[value].price = temp2;

            // swaping ticket number
            temp2 = ticket[idx].ticket_no;
            ticket[idx].ticket_no = ticket[value].ticket_no;
            ticket[value].ticket_no  = temp2;
        }

        // function for displaying tickets on screen
        static void print_tickets(List<TicketType> ticket)
        {
            int flag = 0;
            for (int idx = 0; idx < ticket.Count ; idx++) // loop run for buyed ticket in list
            {
                // if ticket is buyed
                if (ticket[idx].date != 0F) // if date is not equal to zero
                {
                    Console.WriteLine("  *** Ticket no. " + ticket[idx].ticket_no+ " ***");
                    Console.WriteLine("   Train    : " + ticket[idx].t_name);
                    Console.WriteLine("   From     : " + ticket[idx].from);
                    Console.WriteLine("   To       : " + ticket[idx].to);
                    Console.WriteLine("   Date     : " + ticket[idx].day + "-" + ticket[idx].month + "-" + ticket[idx].year);
                    Console.WriteLine("   Quantity : " + ticket[idx].quantity);
                    Console.WriteLine("   Price    : " + ticket[idx].price);
                    Console.WriteLine("\n");
                    Console.WriteLine();
                    flag++;
                }
            }

            // if no ticket is buyed
            if (flag == 0)
            {
                Console.WriteLine("  You Have No Tickets ! ");
            }
        }

        static void my_booked_cargo(List<BookingType> cargo)
        {
            head();
            Console.WriteLine(" User >> My Booked Cargo ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();

            sort_my_cargo(cargo); // calling function for sorting

            print_booked_cargo(cargo); // calling function for displaying tickets

            Console.WriteLine();
            Console.Write("  Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        static void sort_my_cargo(List<BookingType> cargo)
        {
            float min;
            int index = 0;
            for (int idx = 0; idx < cargo.Count; idx++) // it takes value from array and compare with inner loop value
            {
                min = 112122221; // imaginary minimum value
                for (int x = idx; x < cargo.Count; x++)
                {
                    if (min > cargo[x].book_date) // if the selected array value is minimum
                    {
                        min = cargo[x].book_date ;
                        index = x; // storing the index at which we find minimum
                    }
                }

                swaping_cargo_booked_arrays(idx, index, cargo); // calling function for swaping
            }
        }

        static void swaping_cargo_booked_arrays(int value, int index, List<BookingType> cargo)
        {
            // temporary variables
            float temp;
            int temp2;
            string change;

            /*BookingType temp1 = new BookingType();
            temp1 = cargo[index];
            cargo.RemoveAt(index);
            cargo.Insert(index, cargo[value]);
            cargo.RemoveAt(value);
            cargo.Insert(value, temp1);*/

            // swaping date
            temp = cargo[index].book_date;
            cargo[index].book_date = cargo[value].book_date;
            cargo[value].book_date = temp;

            // swaping train name
            change = cargo[index].cargo_train;
            cargo[index].cargo_train = cargo[value].cargo_train;
            cargo[value].cargo_train = change;

            // swaping departure station
            change = cargo[index].book_from;
            cargo[index].book_from = cargo[value].book_from;
            cargo[value].book_from = change;

            // swaping arrival station
            change = cargo[index].book_to;
            cargo[index].book_to = cargo[value].book_to;
            cargo[value].book_to = change;

            // swaping day
            temp = cargo[index].book_day;
            cargo[index].book_day = cargo[value].book_day;
            cargo[value].book_day = temp;

            // swaping month
            temp = cargo[index].book_month;
            cargo[index].book_month = cargo[value].book_month;
            cargo[value].book_month = temp;

            // swaping year
            temp = cargo[index].book_year;
            cargo[index].book_year = cargo[value].book_year;
            cargo[value].book_year = temp;

            // swaping quantity
            temp2 = cargo[index].weight;
            cargo[index].weight = cargo[value].weight;
            cargo[value].weight = temp2;

            // swaping price
            temp2 = cargo[index].c_price;
            cargo[index].c_price = cargo[value].c_price;
            cargo[value].c_price = temp2;
        }

        // function for displaying booked cargo on screen
        static void print_booked_cargo(List<BookingType> cargo)
        {
            int flag = 0;
            for (int idx = 0; idx < cargo.Count ; idx++) // loop run for booked cargo
            {
                // if ticket is buyed
                if (cargo[idx].book_date != 0) // if date is not equal to zero
                {
                    Console.WriteLine("  *** Booking no. " + cargo[idx].booking_no + " ***");
                    Console.WriteLine("   Train  : " + cargo[idx].cargo_train);
                    Console.WriteLine("   From   : " + cargo[idx].book_from);
                    Console.WriteLine("   To     : " + cargo[idx].book_to);
                    Console.WriteLine("   Date   : " + cargo[idx].book_day + "-" + cargo[idx].book_month + "-" + cargo[idx].book_year);
                    Console.WriteLine("   Weight : " + cargo[idx].weight);
                    Console.WriteLine("   Price  : " + cargo[idx].c_price);
                    Console.WriteLine("\n");
                    flag++;
                }
            }

            // if no ticket is buyed
            if (flag == 0)
            {
                Console.WriteLine("  You Have No Cargo Booked ! ");
            }
        }

        // functoin for developer name
        static void developer()
        {
            Console.Clear();
            Console.WriteLine("\n\n");
            Console.WriteLine("************** THANKS FOR USING RAILWAY MANAGEMENT SYSTEM ***************");
            Console.WriteLine("*                                                                       *");
            Console.WriteLine("*            Developer : *** Hamza Rasheed 2021-CS-26  ***              *");
            Console.WriteLine("*                                                                       *");
            Console.WriteLine("*************************************************************************");
            Console.WriteLine();
        }

        // function for edit already exist route
        static char edit_route()
        {
            char option;
            head();
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

        // function for daleting already exist route
        static void delete_route( List<RouteType> route)
        {
            Console.Clear();
            int op;
            char flag;

            // calling lists of routeCount
            op = list_of_trains("Admin", "Delete Route", route);

            op = op - 1;

            Console.WriteLine();
            // asking are you sure
            Console.WriteLine("Are you sure you want to delete the route! ");
            Console.Write("Press 1 for Yes or Press any key for Not ");
            flag = char.Parse(Console.ReadLine());

            if (flag == '1') // if he want to delete route
            {
                Console.WriteLine("\n Route : " + route[op].train);
                Console.WriteLine(" *** Deleted Successfully *** ");

                route.RemoveAt(op);

               // route[op].train = "\0"; // making train name null

               /* // making stations name null
                route[op].ts1 = "\0";
                route[op].ts2 = "\0";
                route[op].ts3 = "\0";
                route[op].ts4 = "\0";

                // making station 1 times zero
                ts1_ath[op] = 0;
                ts1_atm[op] = 0;
                ts1_dth[op] = 0;
                ts1_dtm[op] = 0;

                // making station 2 times zero
                ts2_ath[op] = 0;
                ts2_atm[op] = 0;
                ts2_dth[op] = 0;
                ts2_dtm[op] = 0;

                // making station 3 times zero
                ts3_ath[op] = 0;
                ts3_atm[op] = 0;
                ts3_dth[op] = 0;
                ts3_dtm[op] = 0;

                // making station 4 times zero
                ts4_ath[op] = 0;
                ts4_atm[op] = 0;
                ts4_dth[op] = 0;
                ts4_dtm[op] = 0;

                // making prices null
                tticket[op] = 0;
                tcargo[op] = 0;*/


                //update_array(op); // and updating all arrays
            }
            Console.WriteLine();
            Console.WriteLine("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for updating all arrays after deleting one route
       /* static void update_array(int op)
        {
            for (int x = op; x < routeCount - 1; x++)
            {
                // intializing deleted index route with its next index route

                // train names
                train[op] = train[op + 1];

                // stations names
                ts1[op] = ts1[op + 1];
                ts2[op] = ts2[op + 1];
                ts3[op] = ts3[op + 1];
                ts4[op] = ts4[op + 1];

                // station 1 times
                ts1_ath[op] = ts1_ath[op + 1];
                ts1_atm[op] = ts1_atm[op + 1];
                ts1_dth[op] = ts1_dth[op + 1];
                ts1_dtm[op] = ts1_dtm[op + 1];

                // station 2 times
                ts2_ath[op] = ts2_ath[op + 1];
                ts2_atm[op] = ts2_atm[op + 1];
                ts2_dth[op] = ts2_dth[op + 1];
                ts2_dtm[op] = ts2_dtm[op + 1];

                // station 3 times
                ts3_ath[op] = ts3_ath[op + 1];
                ts3_atm[op] = ts3_atm[op + 1];
                ts3_dth[op] = ts3_dth[op + 1];
                ts3_dtm[op] = ts3_dtm[op + 1];

                // station 4 times
                ts4_ath[op] = ts4_ath[op + 1];
                ts4_atm[op] = ts4_atm[op + 1];
                ts4_dth[op] = ts4_dth[op + 1];
                ts4_dtm[op] = ts4_dtm[op + 1];

                // prices
                tticket[op] = tticket[op + 1];
                tcargo[op] = tcargo[op + 1];
            }
            routeCount--;
        }*/

        // function for mofdify all ready exist train route
        static char modify_route()
        {
            Console.Clear();
            head();
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

        // Function for changing already exist Train name
        static void change_train_name(List<RouteType> route)
        {
            Console.Clear();
            int idx, a = 1;
            idx = list_of_trains("Admin", "Moify Route", route);

            idx--;
            Console.WriteLine("\n\n");
            Console.WriteLine(" Old Train Name " + route[idx].train); // old train name

            while (true)
            {
                a = 1;
                Console.WriteLine(" Enter New train name ");
                route[idx].train = Console.ReadLine(); // taking input of new trin name

                for (int x = 0; x < route.Count; x++)
                {
                    if (x == idx) // if user input same old name than continue
                    {
                        break;
                    }
                    if (route[idx].train == route[idx].train) // if the name alredy exist in array give error
                    {
                        Console.WriteLine();
                        Console.WriteLine(" This Train already exist !");
                        Console.WriteLine(" Enter another name ");
                        a = 0;
                    }
                }

                if (a == 1) // if name met all conditions than change name
                {
                    break;
                }
            }

            Console.WriteLine(" Train name changed Succesfully.");
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for changing stations of alraedy exist train route
        static void change_train_stations(List<RouteType> route, List<string> stations)
        {
            Console.Clear();
            int idx;
            idx = list_of_trains("Admin", "Moify Route", route);
            idx = idx - 1;

            Console.WriteLine();
            Console.WriteLine("Train Name : " + route[idx].train );

            Console.WriteLine("Old Station 1 Name : " + route[idx].ts1);
            Console.Write("New Station-1 name :"); // station 1 name
            route[idx].ts1 = Console.ReadLine();
            Console.WriteLine("Note : use 24 hours format for input time ");

            while (true)
            {
                Console.Write("Arrival Time( hh:mm ) :"); // arrival time station 1
                route[idx].ts1_ath = int.Parse(Console.ReadLine());               // hour
                route[idx].ts1_atm = int.Parse(Console.ReadLine());               // minute
                if (route[idx].ts1_ath >= 1 && route[idx].ts1_ath <= 24 && route[idx].ts1_atm >= 0 && route[idx].ts1_atm <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            while (true)
            {
                Console.Write("Departure Time( hh:mm ) :"); // arrival time station 1
                route[idx].ts1_dth = int.Parse(Console.ReadLine());                 // hour
                route[idx].ts1_dtm = int.Parse(Console.ReadLine());                 // minute
                if (route[idx].ts1_dth >= 1 && route[idx].ts1_dth <= 24 && route[idx].ts1_dtm >= 0 && route[idx].ts1_dtm  <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            Console.WriteLine("Old Station 2 Name : " + route[idx].ts2);
            Console.Write("New Station-2 name :"); // station 2 name
            route[idx].ts2 = Console.ReadLine();

            while (true)
            {
                Console.Write("Arrival Time( hh:mm ) :"); // arrival time station 1
                route[idx].ts2_ath = int.Parse(Console.ReadLine());               // hour
                route[idx].ts2_atm = int.Parse(Console.ReadLine());               // minute
                if (route[idx].ts2_ath >= 1 && route[idx].ts2_ath <= 24 && route[idx].ts2_atm >= 0 && route[idx].ts2_atm <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            while (true)
            {
                Console.Write("Departure Time( hh:mm ) :"); // arrival time station 1
                route[idx].ts2_dth = int.Parse(Console.ReadLine());                 // hour
                route[idx].ts2_dtm = int.Parse(Console.ReadLine());                 // minute
                if (route[idx].ts2_dth >= 1 && route[idx].ts2_dth <= 24 && route[idx].ts2_dtm >= 0 && route[idx].ts2_dtm <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            Console.WriteLine("Old Station 3 Name : " + route[idx].ts2 );
            Console.Write("New Station-3 name :"); // station 3 name
            route[idx].ts3  = Console.ReadLine();

            while (true)
            {
                Console.Write("Arrival Time( hh:mm ) :"); // arrival time station 1
                route[idx].ts3_ath = int.Parse(Console.ReadLine());               // hour
                route[idx].ts3_atm = int.Parse(Console.ReadLine());               // minute
                if (route[idx].ts3_ath >= 1 && route[idx].ts3_ath <= 24 && route[idx].ts3_atm >= 0 && route[idx].ts3_atm <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            while (true)
            {
                Console.Write("Departure Time( hh:mm ) :"); // arrival time station 1
                route[idx].ts2_dth = int.Parse(Console.ReadLine());                 // hour
                route[idx].ts2_dtm = int.Parse(Console.ReadLine());                 // minute
                if (route[idx].ts2_dth >= 1 && route[idx].ts2_dth <= 24 && route[idx].ts2_dtm >= 0 && route[idx].ts2_dtm <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            Console.WriteLine("Old Station 4 Name : " + route[idx].ts2);
            Console.Write("Station-4 name :"); // station 4 name
            route[idx].ts4 = Console.ReadLine();

            while (true)
            {
                Console.Write("Arrival Time( hh:mm ) :"); // arrival time station 1
                route[idx].ts4_ath = int.Parse(Console.ReadLine());               // hour
                route[idx].ts4_atm = int.Parse(Console.ReadLine());               // minute
                if (route[idx].ts4_ath >= 1 && route[idx].ts4_ath <= 24 && route[idx].ts4_atm >= 0 && route[idx].ts4_atm <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            while (true)
            {
                Console.Write("Departure Time( hh:mm ) :"); // arrival time station 1
                route[idx].ts2_dth = int.Parse(Console.ReadLine());                 // hour
                route[idx].ts2_dtm = int.Parse(Console.ReadLine());                 // minute
                if (route[idx].ts2_dth >= 1 && route[idx].ts2_dth <= 24 && route[idx].ts2_dtm >= 0 && route[idx].ts2_dtm <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }
            add_station_to_array(stations ,route, idx);
            Console.WriteLine(" Train Stations changed Succesfully.");
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for adding new station in array
        static void add_station_to_array(List<string> stations  ,List<RouteType> route, int index)
        {
            int n1 = 0, n2 = 0, n3 = 0, n4 = 0;
            for (int i = 0; i < stations.Count ; i++) // loop for checking is name already exist or not
            {
                if (stations[i] != route[index].ts1) // checking name in station 1 array
                {
                    n1++;
                    if (n1 == stations.Count) // if name does not find
                    {
                        stations.Add( route[index].ts1); // than add it in array
                        //st++;
                        //break;
                    }
                }
                if (stations[i] != route[index].ts2 ) // checking name in station 2 array
                {
                    n2++;
                    if (n2 == stations.Count) // if name does not find
                    {
                        stations.Add(route[index].ts2); // than add it in array
                        //st++;
                        //break;
                    }
                }
                if (stations[i] != route[index].ts3 ) // checking name in station 3 array
                {
                    n3++;
                    if (n3 == stations.Count) // if name does not find
                    {
                        stations.Add(route[index].ts3); // than add it in array
                        //st++;
                        //break;
                    }
                }
                if (stations[i] != route[index].ts4 ) // checking name in station 4 array
                {
                    n4++;
                    if (n4 == stations.Count) // if name does not find
                    {
                        stations.Add(route[index].ts1); // than add it in array
                        //st++;
                        //break;
                    }
                }
            }
        }

        // function for loading data from file to arrays and variables
        static void load_data( List<RouteType> route,ref string password, List<string> stations, List<TicketType> ticket, List<BookingType> cargo)
        {
            string path1 = "Train_Routes_Data.txt";
            string line;

            if (File.Exists(path1))
            {
                StreamReader file = new StreamReader(path1);
                while ((line = file.ReadLine()) != null)
                {
                    RouteType read = new RouteType(); // temporary for reding from files

                    read.train = parse_data(line, 1); // reading train names

                    read.ts1 = parse_data(line, 2); // reading trains station 1 name
                    read.ts2 = parse_data(line, 3); // reading trains station 2 name
                    read.ts3 = parse_data(line, 4); // reading trains station 3 name
                    read.ts4 = parse_data(line, 5); // reading trains station 4 name

                    read.ts1_ath = int.Parse(parse_data(line, 6)); // reading station 1 arrival time hour
                    read.ts1_atm = int.Parse(parse_data(line, 7)); // reading station 1 arrival time minutes
                    read.ts1_dth = int.Parse(parse_data(line, 8)); // reading station 1 departure time hour
                    read.ts1_dtm = int.Parse(parse_data(line, 9)); // reading station 1 departure time minutes

                    read.ts2_ath = int.Parse(parse_data(line, 10)); // reading station 2 arrival time hour
                    read.ts2_atm = int.Parse(parse_data(line, 11)); // reading station 2 arrival time minutes
                    read.ts2_dth = int.Parse(parse_data(line, 12)); // reading station 2 departure time hour
                    read.ts2_dtm = int.Parse(parse_data(line, 13)); // reading station 2 departure time minutes

                    read.ts3_ath = int.Parse(parse_data(line, 14)); // reading station 3 arrival time hour
                    read.ts3_atm = int.Parse(parse_data(line, 15)); // reading station 3 arrival time minutes
                    read.ts3_dth = int.Parse(parse_data(line, 16)); // reading station 3 departure time hour
                    read.ts3_dtm = int.Parse(parse_data(line, 17)); // reading station 3 departure time minutes

                    read.ts4_ath = int.Parse(parse_data(line, 18)); // reading station 4 arrival time hour
                    read.ts4_atm = int.Parse(parse_data(line, 19)); // reading station 4 arrival time minutes
                    read.ts4_dth = int.Parse(parse_data(line, 20)); // reading station 4 departure time hour
                    read.ts4_dtm = int.Parse(parse_data(line, 21)); // reading station 4 departure time minutes

                    read.tticket = int.Parse(parse_data(line, 22)); // raeding trains tickets prices
                    read.tcargo = int.Parse(parse_data(line, 23));  // reading cargo route of trains

                    route.Add(read); // adding in the list of routes of trains

                    //routeCount++;
                }
                file.Close(); // closing file after reading data
            }
            else
            {
                Console.WriteLine("File Not Exist");
            }

            string path2 = "user_password.txt";

            // reading password from file
            if (File.Exists(path2))
            {
                StreamReader pwd = new StreamReader(path2);
                while ((line = pwd.ReadLine()) != null)
                {
                    password = line;
                }
                pwd.Close();// closing file
            }

            string path3 = "stations.txt";

            // reading stations names from file
            if (File.Exists(path3))
            {
                StreamReader st_file = new StreamReader(path3);// variable for reading stations name
                while ((line = st_file.ReadLine()) != null)
                {
                    stations.Add( line);
                    //st++; // increment in stations count
                }
                st_file.Close(); // closing file
            }

            string path4 = "tickets_data.txt";

            // reading tickets data from files
            if (File.Exists(path4))
            {
                StreamReader newFile = new StreamReader(path4);
                while ((line = newFile.ReadLine()) != null)
                {
                    TicketType temp = new TicketType(); // temporary for reding from files

                    temp.t_name = parse_data(line, 1);          // ticket train name
                    temp.from = parse_data(line, 2);            // departure station
                    temp.to = parse_data(line, 3);              // arrival station
                    temp.quantity = int.Parse(parse_data(line, 4));  // quantity of tickets
                    temp.ticket_no = int.Parse(parse_data(line, 5)); // ticket number
                    temp.price = int.Parse(parse_data(line, 6));     // price of tickets
                    temp.day = float.Parse(parse_data(line, 7));       // day of ticket
                    temp.month = float.Parse(parse_data(line, 8));     // month of ticket
                    temp.year = float.Parse(parse_data(line, 9));      // year of ticket
                    temp.date = float.Parse(parse_data(line, 10));     // calculated date for applying conditions
                    ticket.Add(temp); // adding in the list of buyed tickets
                    //t++;                                      // increment in tickets count
                }
                newFile.Close(); // closing file
            }

            string path5 = "booked_cargo_data.txt";

            // reding cargo booked data from file
            if (File.Exists(path5))
            {
                StreamReader cargo_File = new StreamReader(path5);
                while ((line = cargo_File.ReadLine()) != null)
                {
                    BookingType read = new BookingType(); // temporary for reding from files

                    read.cargo_train = parse_data(line, 1);      // cargo booked train name
                    read.book_from = parse_data(line, 2);        // departure station
                    read.book_to = parse_data(line, 3);          // arrival station
                    read.weight = int.Parse(parse_data(line, 4));     // weight of cargo
                    read.c_price = int.Parse(parse_data(line, 5));    // cargo booking price
                    read.booking_no = int.Parse(parse_data(line, 6)); // booking number
                    read.book_day = float.Parse(parse_data(line, 7));   // booking day
                    read.book_month = float.Parse(parse_data(line, 8)); // booking month
                    read.book_year = float.Parse(parse_data(line, 9));  // booking year
                    read.book_date = float.Parse(parse_data(line, 10)); // calculated date for applying conditions

                    cargo.Add(read); // adding into list of bookaed cargos

                    //book++; // increment in cargo booked
                }
                cargo_File.Close(); // closing file
            }
        }


        // function for separarting data readed from file one by one as required
        static string parse_data(string record, int field)
        {
            int commaCount = 1; // initial comma count
            string item = "";
            for (int x = 0; x < record.Length; x++)
            {
                if (record[x] == ',') // if find comma in file
                {
                    commaCount++; // increment
                }
                else if (commaCount == field) // if comma Count and field required become same
                {
                    item = item + record[x]; // than read data character by character
                }
            }
            return item; // and returning readed item
        }

        // function for storing data into file
        static void store_data( List<RouteType> route,string password, List<string> stations, List<TicketType> ticket, List<BookingType> cargo)
        {
            string path1 = "Train_Routes_Data.txt";

            // writing data of train routeCount into file
            StreamWriter file = new StreamWriter(path1);
            //file.open("Train_routeCount_Data.txt", ios::out); // opening file for writing
            for (int idx = 0; idx < route.Count; idx++)        // changing index of arrays
            {
                file.Write(route[idx].train + "," + route[idx].ts1 + "," + route[idx].ts2 + "," + route[idx].ts3 + "," + route[idx].ts4 + ",");
                file.Write(route[idx].ts1_ath + "," + route[idx].ts1_atm + "," + route[idx].ts1_dth + "," + route[idx].ts1_dtm + ",");
                file.Write(route[idx].ts2_ath + "," + route[idx].ts2_atm + "," + route[idx].ts2_dth + "," + route[idx].ts2_dtm + ",");
                file.Write(route[idx].ts3_ath + "," + route[idx].ts3_atm + "," + route[idx].ts3_dth + "," + route[idx].ts3_dtm + ",");
                file.Write(route[idx].ts4_ath + "," + route[idx].ts4_atm + "," + route[idx].ts4_dth + "," + route[idx].ts4_dtm + ",");
                file.Write(route[idx].tticket + "," + route[idx].tcargo + ",");

                if (idx < route.Count - 1)
                {
                    file.WriteLine();
                }
            }
            file.Close(); // closing file

            string path2 = "user_password.txt";
            // writing password into file
            StreamWriter pwd = new StreamWriter(path2);
            //pwd.open("user_password.txt", ios::out);
            pwd.Write(password);
            pwd.Close();

            string path3 = "stations.txt";
            // writing stations names into file
            StreamWriter st_file = new StreamWriter(path3);
            //st_file.open("stations.txt", ios::out);
            for (int idx = 0; idx < stations.Count ; idx++)
            {
                st_file.Write(stations[idx]);
                if (idx < stations.Count - 1)
                {
                    st_file.WriteLine();
                }
            }
            st_file.Close();

            string path4 = "tickets_data.txt";
            // writing tickets data into file
            StreamWriter newFile = new StreamWriter(path4);
            // newFile.open("tickets_data.txt", ios::out); // opening file
            for (int idx = 0; idx < ticket.Count; idx++)
            {
                newFile.Write(ticket[idx].t_name + "," + ticket[idx].from[idx] + "," + ticket[idx].to[idx] + ",");
                newFile.Write(ticket[idx].quantity + "," + ticket[idx].ticket_no + "," + ticket[idx].price + ",");
                newFile.Write(ticket[idx].day+ "," + ticket[idx].month + "," + ticket[idx].year + "," + ticket[idx].date + ",");
                if (idx < ticket.Count - 1)
                {
                    newFile.WriteLine();
                }
            }
            newFile.Close(); // closing file

            string path5 = "booked_cargo_data.txt";
            // writing cargo booking data into file
            StreamWriter cargo_File = new StreamWriter(path5);
            // cargo_File.open("booked_cargo_data.txt", ios::out); // opening file
            for (int idx = 0; idx < cargo.Count ; idx++)
            {
                cargo_File.Write(cargo[idx].cargo_train + "," + cargo[idx].book_from + "," + cargo[idx].book_to + ",");
                cargo_File.Write(cargo[idx].weight + "," + cargo[idx].c_price + "," + cargo[idx].booking_no + ",");
                cargo_File.Write(cargo[idx].book_day + "," + cargo[idx].book_month + "," + cargo[idx].book_year + "," + cargo[idx].book_date + "," );

                if (idx < cargo.Count - 1)
                {
                    cargo_File.WriteLine();
                }
            }
            cargo_File.Close(); // closing file
        }

    }
}
