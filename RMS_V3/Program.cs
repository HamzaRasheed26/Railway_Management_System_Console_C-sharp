using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RMS_V3.BL;

namespace RMS_V3
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
            List<TrainTicket> ticket = new List<TrainTicket>();
            List<TrainCargo> cargo = new List<TrainCargo>();
            List<string> stations = new List<string>();


            // -------------------- Actual Program Runs From Here

            load_data(route, ref password, stations, ticket, cargo);
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
                            set_ticket_price(sub_op, route);

                            // admin menu option 3 ends
                        }
                        else if (option == '5')
                        {
                            // admin menu option 4 starts

                            sub_op = list_of_trains("Admin", "View Train Route", route);
                            set_freight_rate(sub_op, route);

                            // admin menu option 4 ends
                        }
                        else if (option == '6')
                        {
                            // admin menu option 5 starts

                            sub_op = station_schedule_menu("Admin", stations);
                            train_station_check("Admin", sub_op, route, stations);

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
                    store_data(route, password, stations, ticket, cargo);
                    break;
                }
                // if invalid input
                else
                {
                    Console.WriteLine("Invalid Input!");
                }
            }
            
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
                    // lines for taking password input
                    Console.Write("Enter Password.........(123)..........:");
                    string pwd;
                    pwd = Console.ReadLine();
                    if (password == pwd) // if password is correct
                    {
                        return "user";
                    }
                    else // if password is wrong
                    {
                        Console.WriteLine("Invalid Password!");
                        Console.Write("Press any key for continue....");
                        Console.ReadKey();
                        Console.WriteLine();
                    }
                    
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
                Console.WriteLine(" {0}. {1} ", a, route[idx].trainName);
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
            Console.WriteLine(" Train Name : " + route[index].trainName);
            Console.WriteLine();

            // printing the stations name their arrival times and departure time
            Console.WriteLine(" Stations\tArrival\t\tDeparture ");
            Console.WriteLine();

            for (int idx = 0; idx < route[index].station.Count; idx++)
            {
                route[index].print(idx);
            }

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
            int st = 0;
            while (flag == '1') // loop run until user want to add routeCount
            {
                Console.Clear();
                head();
                Console.WriteLine(" Admin >> Add new Train Route");
                Console.WriteLine("_____________________________________________________________");
                Console.WriteLine();
                Console.Write(" Enter train name :"); // train name
                takeData.trainName = Console.ReadLine();

                bool entry = true;
                int count = 1;
                while(entry)
                {
                    Console.Write("\n Station-" + count + " name :"); // station 1 name
                    string ST = Console.ReadLine();
                    takeData.station.Add(ST);
                    add_station_to_array(stations, ST); // calling an functin for adding station to stations array

                    st++;
                    Console.WriteLine(" Note : use 24 hours format for input time ");

                    while (true) // validation on correcrt time
                    {
                        Console.Write(" Arrival Time( hh:mm ) :"); // arrival time station 1
                        int hour = int.Parse(Console.ReadLine());
                        takeData.ath.Add(hour);             // hour
                        int min = int.Parse(Console.ReadLine());
                        takeData.atm.Add( min);             // minute
                        if (hour >= 1 && hour <= 24 && min >= 0 && min <= 59)
                        {
                            break;
                        }
                        Console.WriteLine(" Invalid Time ! ");
                    }
                    while (true) // validation on correcrt time
                    {
                        Console.Write(" Departure Time( hh:mm ) :"); // arrival time station 1
                        int hour = int.Parse(Console.ReadLine());
                        takeData.dth.Add( hour);               // hour
                        int min = int.Parse(Console.ReadLine());
                        takeData.dtm.Add( min);               // minute
                        if (hour >= 1 && hour <= 24 && min >= 0 && min <= 59)
                        {
                            break;
                        }
                        Console.WriteLine(" Invalid Time ! ");
                    }
                    count++;
                    if( count > 4)
                    {
                        Console.WriteLine("\n Do You want to add another station...(y/n)...:");
                        string op = Console.ReadLine();
                        if( op != "y" && op != "Y")
                        {
                            break;
                        }
                        
                    }
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
                    takeData.tcargo = int.Parse(Console.ReadLine());

                    if (takeData.tcargo <= 500 && takeData.tcargo > 0) // must be less than 500 and greater than 0
                    {
                        break;
                    }
                    Console.WriteLine(" Price must be less than 500 per kg and greater than 0.");
                }

                route.Add(takeData); // readed data is added into list of route

                Console.WriteLine();
                Console.WriteLine("*** New Route Successfully Added ***");

                Console.Write(" Press 1 for adding another route or any other for exit : ");
                flag = char.Parse(Console.ReadLine());
            }
        }

        // set ticket prices function
        static void set_ticket_price(int index, List<RouteType> route)
        {
            index--;
            Console.Clear();
            head();
            Console.WriteLine(" Admin >> Set Ticket Prices");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Train Name : " + route[index].trainName);
            Console.WriteLine(" Old ticket price is : " + route[index].tticket); // old price of ticket
            while (true)
            {
                Console.Write(" Enter new ticket price :"); // taking input new price of ticket
                int price = int.Parse(Console.ReadLine());

                if (price <= 2000 && price >= 100) // must be less than 2000 and greater than 100
                {
                    route[index].setTicketPrice(price);
                    break;
                }
                Console.WriteLine(" You can not enter price more than 2000 and less than 100. ");
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
            Console.WriteLine("Train Name : " + route[index].trainName);
            Console.WriteLine();
            Console.WriteLine("Old cargo rate of train : " + route[index].tcargo); // old price of cargo
            while (true)
            {
                Console.Write("Enter new cargo rate per kg :"); // taking input new price of cargo
                int price = int.Parse(Console.ReadLine());
                if (price <= 500 && price > 0) // must be less than 500 and greater than 0
                {
                    route[index].setFreightRate(price);
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
            for (int idx = 0; idx < stations.Count; idx++)
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
                route[idx].findStation(stations[index]);
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
                Console.WriteLine(" " + a + ". " + route[idx].trainName + "\t\t" + route[idx].tticket);
                a++;
            }
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for buying ticket of train
        static void buy_ticket(int index, List<RouteType> route, List<TrainTicket> ticket)
        {
            TrainTicket buy = new TrainTicket();
            index--;
            buy.t_name = route[index].trainName;

            Console.Clear();
            head();

            Console.WriteLine(" User >> Buy Tickets ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name :" + buy.t_name);

            // this line print the stations name that are available on this train route
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Stations available :");
            for(int idx = 0; idx < route[index].station.Count; idx++)
            {
                Console.WriteLine("\t" + (idx+1) + ". " + route[index].station[idx] );
            }
            
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Select the station from above...");
            Console.WriteLine(" ");

            while (true) // this loop run until user enter correct value
            {
                Console.Write(" From Station : ");
                buy.from = Console.ReadLine();
                int a = 0;
                // check station name entered by user is valid or not
                for(int idx = 0; idx < route[index].station.Count; idx++)
                {
                    if (buy.from == route[index].station[idx] )
                    {
                        a = 1;
                        break;
                    }
                } 
                if( a == 0) // if station name does not match
                {
                    Console.WriteLine(" Invalid Station Name !");
                    Console.WriteLine(" Again Input ");
                }
                else
                {
                    break;
                }
            }
            while (true) // this loop run until user enter correct value
            {
                Console.Write(" To Station :");
                buy.to = Console.ReadLine();
                int a = 0;
                // check station name entered by user is valid or not
                for (int idx = 0; idx < route[index].station.Count; idx++)
                {
                    if (buy.to == route[index].station[idx] )
                    {
                        a = 1;
                        break;
                    }
                }
                if( a == 1)
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
                buy.month = int.Parse(Console.ReadLine());
                buy.year = int.Parse(Console.ReadLine());

                // check on year
                if (buy.year == 2022)
                {
                    // check on month
                    if (buy.month == 1 || buy.month == 3 || buy.month == 5 || buy.month == 7 || buy.month == 8 || buy.month == 10 || buy.month == 12)
                    {
                        // check on day range from 1 to 31
                        if (buy.day >= 1 && buy.day <= 31)
                        {
                            break;
                        }
                    }
                    // check on month
                    else if (buy.month == 4 || buy.month == 6 || buy.month == 9 || buy.month == 11)
                    {
                        // check on  day range from 1 to 30
                        if (buy.day >= 1 && buy.day <= 30)
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

                if (buy.quantity > 12 || buy.quantity <= 0) // quantity cannot be greater than 12
                {
                    Console.WriteLine(" Error You cannot buy more than 12 quantity ! ");
                }
                else
                {
                    break;
                }
            }

            buy.price = route[index].tticket * buy.quantity;

            Console.WriteLine("Total price for " + buy.quantity + " tickets :" + buy.price);
            // confirming for buying ticket
            Console.Write("You want to buy Ticket (1 for yes, 0 for not) :");
            char op;
            op = char.Parse(Console.ReadLine());

            buying_ticket_message(op, buy, ticket);
        }

        // function for printing on screen that ticket buyed
        static void buying_ticket_message(char flag, TrainTicket buy, List<TrainTicket> ticket)
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

                buy.ticket_no = ticket.Count + 1;
                buy.print();

                Console.WriteLine(" ****Thanks for buying Ticket****");

                buy.calculateDate();

                ticket.Add(buy);
            }
            else // if not buyed than 
            {
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
                Console.WriteLine(" " + a + ". " + route[idx].trainName + "\t" + route[idx].tcargo);
                a++;
            }

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for booking cargo
        static void book_cargo(int index, List<RouteType> route, List<TrainCargo> cargo)
        {
            TrainCargo read = new TrainCargo();
            index--;
            Console.Clear();
            head();
            Console.WriteLine(" User >> Book Cargo ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name :" + route[index].trainName);
            read.cargo_train = route[index].trainName;

            // this line print the stations name that are available on this train route
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Stations available :");
            for (int idx = 0; idx < route[index].station.Count; idx++)
            {
                Console.WriteLine("\t" + (idx + 1) + ". " + route[index].station[idx]);
            }
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Select the station from above...\n");

            while (true) // this loop run until user enter correct value
            {
                Console.Write(" From Station :");
                read.book_from = Console.ReadLine();
                int a = 0;
                // check station name entered by user is valid or not
                for (int idx = 0; idx < route[index].station.Count; idx++)
                {
                    if (read.book_from == route[index].station[idx] )
                    {
                        a = 1;
                        break;
                    }
                }
                if(a == 1)
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
                int a = 0;
                // check station name entered by user is valid or not
                for (int idx = 0; idx < route[index].station.Count; idx++)
                {
                    if (read.book_to == route[index].station[idx] )
                    {
                        a = 1;
                        break;
                    }
                }
                if (a == 1)
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
            Console.WriteLine("Price per kg :" + route[index].tcargo);

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
            read.c_price = route[index].tcargo * read.weight;

            Console.WriteLine();
            Console.WriteLine("You have to pay :" + read.c_price);
            Console.Write("You want to book cargo (1 for yes, 0 for not) :");
            char op;
            op = char.Parse(Console.ReadLine());
            confirming_book_cargo(op, cargo, read);
        }

        // function for printing on screen that cargo booked or not
        static void confirming_book_cargo(char flag, List<TrainCargo> cargo, TrainCargo read)
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

                read.booking_no = cargo.Count + 1;
                read.print();

                Console.WriteLine(" **** Your cargo succesfully booked ***");

                read.calculateDate();

                cargo.Add(read);

                Console.WriteLine();
            }
            else // if not booked creating arrays index null
            {
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
        static void my_tickets(List<TrainTicket> ticket)
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
        static void sort_my_tickets(List<TrainTicket> ticket)
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
        static void swaping_ticket(int value, int idx, List<TrainTicket> ticket)
        {
            // temporary variables
            //float temp;
            //int temp2;
            //string change;

            TrainTicket temp1 = ticket[idx];
            ticket[idx] = ticket[value];
            ticket[value] = temp1;
            /*temp1 = ticket[idx];
            ticket.Insert(idx, ticket[value]);
            ticket.Insert(value, temp1);*/

            /*// swaping date
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
            ticket[value].to = change;

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
            ticket[value].ticket_no = temp2;*/
        }

        // function for displaying tickets on screen
        static void print_tickets(List<TrainTicket> ticket)
        {
            int flag = 0;
            for (int idx = 0; idx < ticket.Count; idx++) // loop run for buyed ticket in list
            {
                // if ticket is buyed
                if (ticket[idx].date != 0F) // if date is not equal to zero
                {
                    ticket[idx].print();
                    flag++;
                }
            }

            // if no ticket is buyed
            if (flag == 0)
            {
                Console.WriteLine("  You Have No Tickets ! ");
            }
        }

        static void my_booked_cargo(List<TrainCargo> cargo)
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

        static void sort_my_cargo(List<TrainCargo> cargo)
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
                        min = cargo[x].book_date;
                        index = x; // storing the index at which we find minimum
                    }
                }

                swaping_cargo_booked_arrays(idx, index, cargo); // calling function for swaping
            }
        }

        static void swaping_cargo_booked_arrays(int value, int index, List<TrainCargo> cargo)
        {
            // temporary variables
            // float temp;
            //int temp2;
            //string change;

            TrainCargo temp1 = cargo[index];
            cargo[index] = cargo[value];
            cargo[value] = temp1;


            /*temp1 = cargo[index];
            cargo.RemoveAt(index);
            cargo.Insert(index, cargo[value]);
            cargo.RemoveAt(value);
            cargo.Insert(value, temp1);*/

            /*// swaping date
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
            cargo[value].c_price = temp2;*/
        }

        // function for displaying booked cargo on screen
        static void print_booked_cargo(List<TrainCargo> cargo)
        {
            int flag = 0;
            for (int idx = 0; idx < cargo.Count; idx++) // loop run for booked cargo
            {
                // if ticket is buyed
                if (cargo[idx].book_date != 0) // if date is not equal to zero
                {
                    cargo[idx].print();
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
        static void delete_route(List<RouteType> route)
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
                Console.WriteLine("\n Route : " + route[op].trainName);
                Console.WriteLine(" *** Deleted Successfully *** ");

                route.RemoveAt(op);

            }
            Console.WriteLine();
            Console.WriteLine("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

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
            Console.WriteLine(" Old Train Name " + route[idx].trainName); // old train name

            while (true)
            {
                a = 1;
                Console.WriteLine(" Enter New train name ");
                string trainName = Console.ReadLine(); // taking input of new trin name

                for (int x = 0; x < route.Count; x++)
                {
                    if (x == idx) // if user input same old name than continue
                    {
                        break;
                    }
                    if (route[x].trainName == trainName) // if the name alredy exist in array give error
                    {
                        Console.WriteLine();
                        Console.WriteLine(" This Train already exist !");
                        Console.WriteLine(" Enter another name ");
                        a = 0;
                    }
                }

                if (a == 1) // if name met all conditions than change name
                {
                    route[idx].changeTrainName(trainName);
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
            Console.WriteLine("Train Name : " + route[idx].trainName);

            for(int y = 0; y < route[idx].station.Count; y++)
            {
                Console.WriteLine("Old Station " + (y+1) + " Name : " + route[idx].station[y]);
                Console.Write("New Station " + (y+1) + " name :"); // station 1 name
                string newST = Console.ReadLine();
                route[idx].station.Add(newST);
                add_station_to_array(stations, newST);
                
                Console.WriteLine("Note : use 24 hours format for input time ");

                while (true)
                {
                    Console.Write("Arrival Time( hh:mm ) :"); // arrival time station 1
                    int hour = int.Parse(Console.ReadLine());
                    route[idx].ath.Add(hour);               // hour
                    int min = int.Parse(Console.ReadLine());
                    route[idx].atm.Add(min);               // minute
                    if (hour >= 1 && hour <= 24 && min >= 0 && min <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }

                while (true)
                {
                    Console.Write("Departure Time( hh:mm ) :"); // arrival time station 1
                    int hour = int.Parse(Console.ReadLine());
                    route[idx].dth.Add(hour);                 // hour
                    int min = int.Parse(Console.ReadLine());
                    route[idx].dtm.Add(min);                 // minute
                    if (hour >= 1 && hour <= 24 && min >= 0 && min <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }
            }

            
            Console.WriteLine(" Train Stations changed Succesfully.");
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for adding new station in array
        static void add_station_to_array(List<string> stations, string st)
        {
            
            int n1 = 0;

            for (int i = 0; i < stations.Count; i++) // loop for checking is name already exist or not
            {

                if (stations[i] != st) // checking name in station 1 array
                {
                    n1++;
                    if (n1 == stations.Count) // if name does not find
                    {
                        stations.Add(st); // than add it in array

                    }
                }
            }


        }

        // function for loading data from file to arrays and variables
        static void load_data(List<RouteType> route, ref string password, List<string> stations, List<TrainTicket> ticket, List<TrainCargo> cargo)
        {
            string path1 = "Train_Routes_Data.txt";
            string line;

            if (File.Exists(path1))
            {
                StreamReader file = new StreamReader(path1);
                string temp = "";
                while ((line = file.ReadLine()) != null)
                {
                    RouteType read = new RouteType(); // temporary for reding from files

                    int x = 1;
                    read.trainName = parse_data(line, x); // reading train names
                    x++;
                    int count = int.Parse(parse_data(line, x));
                    x++;

                    for(int i = 0; i < count; i++ )
                    {
                        read.station.Add(parse_data(line, x));
                        x++;
                        temp = parse_data(line, x);
                        read.ath.Add(int.Parse(temp));
                        x++;
                        temp = parse_data(line, x);
                        read.atm.Add(int.Parse(temp));
                        x++;
                        temp = parse_data(line, x);
                        read.dth.Add(int.Parse(temp));
                        x++;
                        temp = parse_data(line, x);
                        read.dtm.Add(int.Parse(temp));
                        x++;
                    }

                    read.tticket = int.Parse(parse_data(line, x)); // raeding trains tickets prices
                    x++;
                    read.tcargo = int.Parse(parse_data(line, x));  // reading cargo route of trains
                    x++;
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
                    stations.Add(line);
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
                    TrainTicket temp = new TrainTicket(); // temporary for reding from files

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
                    TrainCargo read = new TrainCargo(); // temporary for reding from files

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
        static void store_data(List<RouteType> route, string password, List<string> stations, List<TrainTicket> ticket, List<TrainCargo> cargo)
        {
            string path1 = "Train_Routes_Data.txt";

            // writing data of train routeCount into file
            StreamWriter file = new StreamWriter(path1);
            //file.open("Train_routeCount_Data.txt", ios::out); // opening file for writing
            for (int idx = 0; idx < route.Count; idx++)        // changing index of arrays
            {
                file.Write(route[idx].trainName + ",");
                file.Write(route[idx].station.Count + ",");
                for (int x = 0; x < route[idx].station.Count; x++)
                {
                    
                    file.Write( route[idx].station[x] + ",");
                    file.Write(route[idx].ath[x] + "," + route[idx].atm[x] + "," + route[idx].dth[x] + "," + route[idx].dtm[x] + ",");
                }
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
            for (int idx = 0; idx < stations.Count; idx++)
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
                newFile.Write(ticket[idx].t_name + "," + ticket[idx].from + "," + ticket[idx].to + ",");
                newFile.Write(ticket[idx].quantity + "," + ticket[idx].ticket_no + "," + ticket[idx].price + ",");
                newFile.Write(ticket[idx].day + "," + ticket[idx].month + "," + ticket[idx].year + "," + ticket[idx].date + ",");
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
            for (int idx = 0; idx < cargo.Count; idx++)
            {
                cargo_File.Write(cargo[idx].cargo_train + "," + cargo[idx].book_from + "," + cargo[idx].book_to + ",");
                cargo_File.Write(cargo[idx].weight + "," + cargo[idx].c_price + "," + cargo[idx].booking_no + ",");
                cargo_File.Write(cargo[idx].book_day + "," + cargo[idx].book_month + "," + cargo[idx].book_year + "," + cargo[idx].book_date + ",");

                if (idx < cargo.Count - 1)
                {
                    cargo_File.WriteLine();
                }
            }
            cargo_File.Close(); // closing file
        }

    }
}
