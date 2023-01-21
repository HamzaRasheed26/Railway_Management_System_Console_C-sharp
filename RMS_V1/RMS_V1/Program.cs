using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RMS_V1
{
    internal class Program
    {
        //______________________ Data structure ____________________________________________________________________

        static string password;
        static string notice = "No_notice";

        // array size default set
        static int array_size = 20;

        // array for train names
        static string[] train = new string[array_size];

        // all stations that syatem have built in
        const int st_size = 50;
        static string[] stations = new string[st_size];

        // arrays for stations
        static string[] ts1 = new string[array_size]; // for station 1
        static string[] ts2 = new string[array_size]; // for station 2
        static string[] ts3 = new string[array_size]; // for station 3
        static string[] ts4 = new string[array_size]; // for station 4

        // by default we have four routes
        static int routes = 0;
        static int st = 0;

        // arrays for trains arrival times on stations
        static int[] ts1_ath = new int[array_size]; // for station 1 arrival hour
        static int[] ts1_atm = new int[array_size]; // for station 1 arrival minutes
        static int[] ts2_ath = new int[array_size]; // for station 2 arrival hour
        static int[] ts2_atm = new int[array_size]; // for station 2 arrival minutes
        static int[] ts3_ath = new int[array_size]; // for station 3 arrival hour
        static int[] ts3_atm = new int[array_size]; // for station 3 arrival minutes
        static int[] ts4_ath = new int[array_size]; // for station 4 arrival hour
        static int[] ts4_atm = new int[array_size]; // for station 4 arrival minutes

        // arrays for trains departure times from stations
        static int[] ts1_dth = new int[array_size]; // for station 1 departure hour
        static int[] ts1_dtm = new int[array_size]; // for station 1 departure minutes
        static int[] ts2_dth = new int[array_size]; // for station 2 departure hour
        static int[] ts2_dtm = new int[array_size]; // for station 2 departure minutes
        static int[] ts3_dth = new int[array_size]; // for station 3 departure hour
        static int[] ts3_dtm = new int[array_size]; // for station 3 departure minutes
        static int[] ts4_dth = new int[array_size]; // for station 4 departure hour
        static int[] ts4_dtm = new int[array_size]; // for station 4 departure minutes

        // array for prices of trains tickets
        static int[] tticket = new int[array_size];
        // array for prices of trains freight rate
        static int[] tcargo = new int[array_size];

        // t means tickets buy book means cargo booked
        static int t = 0, book = 0;

        // tickets array user can only buy four tickets
        static int ticket_a = 20;

        // arrays that used for buying tickets
        static string[] t_name = new string[ticket_a]; // for train name
        static string[] from = new string[ticket_a];   // for departure station
        static string[] to = new string[ticket_a];     // for arrival station
        static int[] quantity = new int[ticket_a];  // for quantity of tickets
        static int[] ticket_no = new int[ticket_a]; // for the number of ticket
        static int[] price = new int[ticket_a];     // for price of tickets user have to pay

        // arrays for time date of ticket
        static float[] day = new float[ticket_a], month = new float[ticket_a], year = new float[ticket_a];
        static float[] date = new float[ticket_a];

        // arrays that used for booking cargo
        static string[] cargo_train = new string[ticket_a]; // for storing the name of train in which cargo is booked
        static string[] book_from = new string[ticket_a];   // for storing the name of departure station
        static string[] book_to = new string[ticket_a];     // for storing the name of arrival station
        static int[] weight = new int[ticket_a];         // for weight of cargo
        static int[] c_price = new int[ticket_a];        // for cargo deleviring price user have to pay
        static int[] booking_no = new int[ticket_a];     // for the number of booking

        // arrays for date of booking
        static float[] book_day = new float[ticket_a], book_month = new float[ticket_a], book_year = new float[ticket_a];
        static float[] book_date = new float[ticket_a];

        //-----------------------------  main function------------------------------------------
        static void Main(string[] args)
        {
            load_data();
            string user;

            while (true) // loop for main menu
            {

                // calling login function
                user = login_page();

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
                            sub_op = list_of_trains("Admin", "View Train Route");
                            // calling function for further train detail
                            view_train_route_detail("Admin", "View Train Route", sub_op);

                            // admin menu option 1 ends
                        }
                        else if (option == '2')
                        {
                            // admin menu option 2 starts

                            if (routes < array_size) // if the added train is less than array size
                            {
                                // function for adding train
                                add_train_route();
                            }
                            else // if added train becomes greater than array size
                            {
                                head();
                                Console.WriteLine(" Admin >> Add new Train Route");
                                Console.WriteLine("_____________________________________________________________");
                                Console.WriteLine();
                                Console.WriteLine(" Sorry! Our system has limit of only 10 routes.");
                                Console.Write("Press any key for continue....");
                                Console.ReadKey();
                                Console.WriteLine();
                            }

                            // admin menu option 2 ends
                        }
                        else if (option == '3')
                        {
                            // admin menu option 3 starts

                            sub_op = edit_route(); // menu of edit route

                            if (sub_op == '1') // for option 1
                            {
                                delete_route(); // deleting route
                            }
                            else if (sub_op == '2') // for option 2
                            {
                                sub_op = modify_route(); // modifying route

                                if (sub_op == '1')
                                {
                                    change_train_name(); // changing train name
                                }
                                else if (sub_op == '2')
                                {
                                    change_train_stations(); // changing train station
                                }
                            }

                            // admin menu option 3 ends
                        }
                        else if (option == '4')
                        {
                            // admin menu option 3 starts

                            sub_op = list_of_trains("Admin", "View Train Route");
                            set_ticket_price(sub_op);

                            // admin menu option 3 ends
                        }
                        else if (option == '5')
                        {
                            // admin menu option 4 starts

                            sub_op = list_of_trains("Admin", "View Train Route");
                            set_freight_rate(sub_op);

                            // admin menu option 4 ends
                        }
                        else if (option == '6')
                        {
                            // admin menu option 5 starts

                            sub_op = station_schedule_menu("Admin");
                            train_station_check("Admin", sub_op);

                            // admin menu option 5 ends
                        }
                        else if (option == '7')
                        {
                            // admin menu option 6 starts

                            add_notice();

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

                            sub_op = list_of_trains("User", "View Train Route");
                            view_train_route_detail("User", "View Train Route", sub_op);

                            // user menu option 1 ends
                        }
                        else if (option == "2")
                        {
                            // user menu option 2 starts

                            sub_op = station_schedule_menu("User");
                            train_station_check("User", sub_op);

                            // user menu option 2 ends
                        }
                        else if (option == "3")
                        {
                            // user menu option 3 starts

                            view_tickets_price();

                            // user menu option 3 ends
                        }
                        else if (option == "4")
                        {
                            // user menu option 4 starts

                            if (t == ticket_a)
                            {
                                head();
                                Console.WriteLine(" User >> Buy Tickets ");
                                Console.WriteLine("_____________________________________________________________");
                                Console.WriteLine();
                                Console.WriteLine("You cannot buy more than " + ticket_a + " tickets ! ");
                                Console.Write("Press any key for continue....");
                                Console.ReadKey();
                                Console.WriteLine();
                            }
                            else
                            {
                                sub_op = list_of_trains("User", "Buy Ticket");
                                buy_ticket(sub_op);
                            }

                            // user menu option 4 ends
                        }
                        else if (option == "5")
                        {
                            // user menu option 5 starts

                            my_tickets();

                            // user menu option 5 ends
                        }
                        else if (option == "6")
                        {
                            // user menu option 6 starts

                            view_freight_rate();

                            // user menu option 6 ends
                        }
                        else if (option == "7")
                        {
                            // user menu option 7 starts

                            if (book == ticket_a)
                            {
                                head();
                                Console.WriteLine(" User >> Book Cargo ");
                                Console.WriteLine("_____________________________________________________________");
                                Console.WriteLine();
                                Console.WriteLine("You cannot book cargo  more than " + ticket_a + " times ! ");
                                Console.Write("Press any key for continue....");
                                Console.ReadKey();
                                Console.WriteLine();
                            }
                            else
                            {
                                sub_op = list_of_trains("User", "Book Cargo");
                                book_cargo(sub_op);
                            }
                            // user menu option 7 ends
                        }
                        else if (option == "8")
                        {
                            // user menu option 8 starts

                            my_booked_cargo();

                            // user menu option 8 ends
                        }
                        else if (option == "9")
                        {
                            // user menu option 9 starts

                            view_notice();

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
            store_data();
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
        static string login_page()
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
            Console.WriteLine(" 1. View of all routes of trains");
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
        static int list_of_trains(string name, string title)
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

            for (int idx = 0; idx < routes; idx++) // for printing train names from array
            {
                Console.WriteLine(" {0}. {1} ", a, train[idx]);
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

        // view train routes station name and arrival departure times function
        static void view_train_route_detail(string name, string title, int index)
        {
            Console.Clear();
            index = index - 1;
            head();
            // in place of "name" there we pass "user/admin" from function call
            // or in place of "title" we pass the title accordimg to our need in program
            Console.WriteLine(" " + name + " >> " + title);
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Train Name : " + train[index]);
            Console.WriteLine();

            // printing the stations name their arrival times and departure time
            Console.WriteLine(" Stations\tArrival\t\tDeparture ");
            Console.WriteLine();
            Console.WriteLine(" " + ts1[index] + "\t" + ts1_ath[index] + ":" + ts1_atm[index] + "\t\t" + ts1_dth[index] + ":" + ts1_dtm[index]);
            Console.WriteLine(" " + ts2[index] + " \t" + ts2_ath[index] + ":" + ts2_atm[index] + "\t\t" + ts2_dth[index] + ":" + ts2_dtm[index]);
            Console.WriteLine(" " + ts3[index] + " \t" + ts3_ath[index] + ":" + ts3_atm[index] + "\t\t" + ts3_dth[index] + ":" + ts3_dtm[index]);
            Console.WriteLine(" " + ts4[index] + "\t" + ts4_ath[index] + ":" + ts4_atm[index] + "\t\t" + ts4_dth[index] + ":" + ts4_dtm[index]);
            Console.WriteLine();
            Console.Write(" Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // Add new train route function
        static void add_train_route()
        {
            char flag = '1';
            while (flag == '1') // loop run until user want to add routes
            {
                Console.Clear();
                head();
                Console.WriteLine(" Admin >> Add new Train Route");
                Console.WriteLine("_____________________________________________________________");
                Console.WriteLine();
                Console.Write(" Enter train name :"); // train name
                train[routes] = Console.ReadLine();
                Console.Write("\n Station-1 name :"); // station 1 name
                ts1[routes] = Console.ReadLine();
                Console.WriteLine(" Note : use 24 hours format for input time ");

                while (true) // validation on correcrt time
                {
                    Console.Write(" Arrival Time( hh:mm ) :"); // arrival time station 1
                    ts1_ath[routes] = int.Parse(Console.ReadLine());             // hour
                    ts1_atm[routes] = Console.Read();             // minute
                    if (ts1_ath[routes] >= 1 && ts1_ath[routes] <= 24 && ts1_atm[routes] >= 0 && ts1_atm[routes] <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }
                while (true) // validation on correcrt time
                {
                    Console.Write(" Departure Time( hh:mm ) :"); // arrival time station 1
                    ts1_dth[routes] = int.Parse(Console.ReadLine());               // hour
                    ts1_dtm[routes] = int.Parse(Console.ReadLine());               // minute
                    if (ts1_dth[routes] >= 1 && ts1_dth[routes] <= 24 && ts1_dtm[routes] >= 0 && ts1_dtm[routes] <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }

                Console.Write("\n Station-2 name :"); // station 2 name
                ts2[routes] = Console.ReadLine();
                while (true) // validation on correcrt time
                {
                    Console.Write(" Arrival Time( hh:mm ) :"); // arrival time station 2
                    ts2_ath[routes] = int.Parse(Console.ReadLine());             // hour
                    ts2_atm[routes] = int.Parse(Console.ReadLine());             // minute
                    if (ts2_ath[routes] >= 1 && ts2_ath[routes] <= 24 && ts2_atm[routes] >= 0 && ts2_atm[routes] <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }
                while (true) // validation on correcrt time
                {
                    Console.Write(" Departure Time( hh:mm ) :"); // arrival time station 2
                    ts2_dth[routes] = int.Parse(Console.ReadLine());               // hour
                    ts2_dtm[routes] = int.Parse(Console.ReadLine());               // minute
                    if (ts2_dth[routes] >= 1 && ts2_dth[routes] <= 24 && ts2_dtm[routes] >= 0 && ts2_dtm[routes] <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }

                Console.Write("\n Station-3 name :"); // station 3 name
                ts3[routes] = Console.ReadLine();
                while (true) // validation on correcrt time
                {
                    Console.Write(" Arrival Time( hh:mm ) :"); // arrival time station 3
                    ts3_ath[routes] = int.Parse(Console.ReadLine());             // hour
                    ts3_atm[routes] = int.Parse(Console.ReadLine());             // minute
                    if (ts3_ath[routes] >= 1 && ts3_ath[routes] <= 24 && ts3_atm[routes] >= 0 && ts3_atm[routes] <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }
                while (true) // validation on correcrt time
                {
                    Console.Write(" Departure Time( hh:mm ) :"); // arrival time station 3
                    ts3_dth[routes] = int.Parse(Console.ReadLine());               // hour
                    ts3_dtm[routes] = int.Parse(Console.ReadLine());               // minute
                    if (ts3_dth[routes] >= 1 && ts3_dth[routes] <= 24 && ts3_dtm[routes] >= 0 && ts3_dtm[routes] <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }

                Console.Write("\n Station-4 name :"); // station 4 name
                ts4[routes] = Console.ReadLine();
                while (true) // validation on correcrt time
                {
                    Console.Write(" Arrival Time( hh:mm ) :"); // arrival time station 2
                    ts4_ath[routes] = int.Parse(Console.ReadLine());             // hour
                    ts4_atm[routes] = int.Parse(Console.ReadLine());             // minute
                    if (ts4_ath[routes] >= 1 && ts4_ath[routes] <= 24 && ts4_atm[routes] >= 0 && ts4_atm[routes] <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }
                while (true) // validation on correcrt time
                {
                    Console.Write(" Departure Time( hh:mm ) :"); // arrival time station 2
                    ts4_dth[routes] = int.Parse(Console.ReadLine());               // hour
                    ts4_dtm[routes] = int.Parse(Console.ReadLine());               // minute
                    if (ts4_dth[routes] >= 1 && ts4_dth[routes] <= 24 && ts4_dtm[routes] >= 0 && ts4_dtm[routes] <= 59)
                    {
                        break;
                    }
                    Console.WriteLine(" Invalid Time ! ");
                }

                while (true) // validation on correcrt ticket price
                {
                    Console.Write(" Set Ticket Price :"); // ticket price for train
                    tticket[routes] = int.Parse(Console.ReadLine());

                    if (tticket[routes] <= 2000 && tticket[routes] > 100) // must be less than 2000 and greater than 100
                    {
                        break;
                    }
                    Console.WriteLine(" Train Ticket Price Cannot be greater than 2000 Rs and cannot be less than 100 Rs. ");
                }

                while (true) // validation on correcrt cargo price
                {
                    Console.Write(" Set cargo rate per kg :"); // cargo rate for that train
                    tcargo[routes] = int.Parse(Console.ReadLine());

                    if (tcargo[routes] <= 500 && tcargo[routes] > 0) // must be less than 500 and greater than 0
                    {
                        break;
                    }
                    Console.WriteLine(" Price must be less than 500 per kg and greater than 0.");
                }

                Console.WriteLine();
                Console.WriteLine("*** New Route Successfully Added ***");
                add_station_to_array(); // calling an functin for adding station to stations array

                routes++; // increament in route counter

                Console.Write(" Press 1 for adding another route or any other for exit : ");
                flag = char.Parse(Console.ReadLine());
            }
        }

        // set ticket prices function
        static void set_ticket_price(int index)
        {
            index--;
            Console.Clear();
            head();
            Console.WriteLine(" Admin >> Set Ticket Prices");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name : " + train[index]);
            Console.WriteLine("Old ticket price is : " + tticket[index]); // old price of ticket
            while (true)
            {
                Console.Write("Enter new ticket price :"); // taking input new price of ticket
                tticket[index] = int.Parse(Console.ReadLine());

                if (tticket[index] <= 2000 && tticket[index] >= 100) // must be less than 2000 and greater than 100
                {

                    break;
                }
                Console.WriteLine("You can not enter price more than 2000 and less than 100. ");
            }
        }

        // set freight prices of trains function
        static void set_freight_rate(int index)
        {
            index--;
            Console.Clear();
            head();
            Console.WriteLine(" Admin >> Set Freight Rate ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name : " + train[index]);
            Console.WriteLine();
            Console.WriteLine("Old cargo rate of train : " + tcargo[index]); // old price of cargo
            while (true)
            {
                Console.Write("Enter new cargo rate per kg :"); // taking input new price of cargo
                tcargo[index] = int.Parse(Console.ReadLine());
                if (tcargo[index] <= 500 && tcargo[index] > 0) // must be less than 500 and greater than 0
                {
                    break;
                }
                Console.WriteLine("You cannot enter rate more than 500 per kg and less than 0.");
            }
        }

        // view station schedule that wich trains come on station function
        static int station_schedule_menu(string name)
        {

            head();
            // in place of "name" there we pass "user/admin" from function call
            Console.WriteLine(" " + name + " >> View Station Schedule  ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Select any from available stations......");
            // stations name available
            int a = 1;

            // loop for showing all stations name from array
            for (int idx = 0; idx < st; idx++)
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
        static void train_station_check(string name, int index)
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

            for (int idx = 0; idx < array_size; idx++) // loop run for all train station array
            {
                if (stations[index] == ts1[idx]) // it search the required station in ts1 array
                {
                    Console.WriteLine(train[idx] + "\t\t" + ts1_ath[idx] + ":" + ts1_atm[idx] + "\t" + ts1_dth[idx] + ":" + ts1_dtm[idx]);
                }
                if (stations[index] == ts2[idx]) // it search the required station in ts2 array
                {
                    Console.WriteLine(train[idx] + "\t\t" + ts1_ath[idx] + ":" + ts1_atm[idx] + "\t" + ts1_dth[idx] + ":" + ts1_dtm[idx]);
                }
                if (stations[index] == ts3[idx]) // it search the required station in ts3 array
                {
                    Console.WriteLine(train[idx] + "\t\t" + ts1_ath[idx] + ":" + ts1_atm[idx] + "\t" + ts1_dth[idx] + ":" + ts1_dtm[idx]);
                }
                if (stations[index] == ts4[idx]) // it search the required station in ts4 array
                {
                    Console.WriteLine(train[idx] + "\t\t" + ts1_ath[idx] + ":" + ts1_atm[idx] + "\t" + ts1_dth[idx] + ":" + ts1_dtm[idx]);
                }
            }
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // Function for posting notices for user
        static void add_notice()
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
            Console.WriteLine(" 1. View of all routes of trains");
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
        static void view_tickets_price()
        {
            head();

            Console.WriteLine(" User >> View Tickets Price ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Train Name\t\tTicket Price");
            Console.WriteLine();

            int a = 1;
            // prints all train names with their tickets prices
            for (int idx = 0; idx < routes; idx++)
            {
                Console.WriteLine(" " + a + ". " + train[idx] + "\t\t" + tticket[idx]);
                a++;
            }
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for buying ticket of train
        static void buy_ticket(int index)
        {
            index--;
            t_name[t] = train[index];

            Console.Clear();
            head();

            Console.WriteLine(" User >> Buy Tickets ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name :" + t_name[t]);

            // this line print the stations name that are available on this train route
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Stations available :");
            Console.WriteLine(" 1." + ts1[index] + "\t 2." + ts2[index]);
            Console.WriteLine(" 3." + ts3[index] + "\t 4." + ts4[index]);
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Select the station from above...");
            Console.WriteLine(" ");

            while (true) // this loop run until user enter correct value
            {
                Console.Write(" From Station : ");
                from[t] = Console.ReadLine();
                // check station name entered by user is valid or not
                if (from[t] == ts1[index] || from[t] == ts2[index] || from[t] == ts3[index] || from[t] == ts4[index])
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
                to[t] = Console.ReadLine();
                // check station name entered by user is valid or not
                if (to[t] == ts1[index] || to[t] == ts2[index] || to[t] == ts3[index] || to[t] == ts4[index])
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
                day[t] = int.Parse(Console.ReadLine());
                month[t] = int.Parse(Console.ReadLine());
                year[t] = int.Parse(Console.ReadLine());

                // check on year
                if (year[t] == 2022)
                {
                    // check on month
                    if (month[t] == 1 || month[t] == 3 || month[t] == 5 || month[t] == 7 || month[t] == 8 || month[t] == 10 || month[t] == 12)
                    {
                        // check on day range from 1 to 31
                        if (day[t] >= 1 && day[t] <= 31)
                        {
                            break;
                        }
                    }
                    // check on month
                    else if (month[t] == 4 || month[t] == 6 || month[t] == 9 || month[t] == 11)
                    {
                        // check on  day range from 1 to 30
                        if (day[t] >= 1 && day[t] <= 30)
                        {
                            break;
                        }
                    }
                    // check on month of febuary
                    else if (month[t] == 2)
                    {
                        // check on day range from 1 to 28
                        if (day[t] >= 1 && day[t] <= 28)
                        {
                            break;
                        }
                    }
                }
                Console.WriteLine("\n Invalid Date ! ");
                Console.WriteLine(" Again enter date please.");
            }

            Console.WriteLine(" Ticket price is :" + tticket[index]);

            while (true) // validation for quantity
            {
                Console.Write(" Enter quantity of tickets :");
                quantity[t] = int.Parse(Console.ReadLine());

                if (quantity[t] > 12 || quantity[t] <= 0) // quantity cannot be greater than 12
                {
                    Console.WriteLine(" Error You cannot buy more than 12 quantity ! ");
                }
                else
                {
                    break;
                }
            }

            price[t] = tticket[index] * quantity[t];

            Console.WriteLine("Total price for " + quantity[t] + " tickets :" + price[t]);
            // confirming for buying ticket
            Console.Write("You want to buy Ticket (1 for yes, 0 for not) :");
            char op;
            op = char.Parse(Console.ReadLine());

            buying_ticket_message(op);
        }

        // function for printing on screen that ticket buyed
        static void buying_ticket_message(char flag)
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
                Console.WriteLine("  Train Name :" + t_name[t]);
                Console.WriteLine("  From station :" + from[t]);
                Console.WriteLine("  To station :" + to[t]);
                Console.WriteLine("  Date  :" + day[t] + "-" + month[t] + "-" + year[t]);
                Console.WriteLine("  Quantity :" + quantity[t]);
                Console.WriteLine("  Price :" + price[t]);
                Console.WriteLine();
                Console.WriteLine(" ****Thanks for buying Ticket****");
                date[t] = day[t] + (month[t] * (float)30.417);

                ticket_no[t] = t + 1;

                t++;
            }
            else // if not buyed creating arrays values null
            {
                t_name[t] = " ";
                from[t] = " ";
                to[t] = " ";
                day[t] = 0;
                month[t] = 0;
                year[t] = 0;
                date[t] = 0;
                Console.WriteLine();
                Console.WriteLine(" Ticket not Buyed !");
            }

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for displaying prices of freight/cargo
        static void view_freight_rate()
        {
            head();
            Console.WriteLine(" User >> View Freight Rates ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name\t\tRate/kg   ");
            Console.WriteLine();

            int a = 1;
            // prints all train names with their tickets prices
            for (int idx = 0; idx < routes; idx++)
            {
                Console.WriteLine(" " + a + ". " + train[idx] + "\t" + tcargo[idx]);
                a++;
            }

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for booking cargo
        static void book_cargo(int index)
        {
            index--;
            Console.Clear();
            head();
            Console.WriteLine(" User >> Book Cargo ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name :" + train[index]);
            cargo_train[book] = train[index];

            // this line print the stations name that are available on this train route
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Stations available :");
            Console.WriteLine(" 1." + ts1[index] + "\t 2." + ts2[index]);
            Console.WriteLine(" 3." + ts3[index] + "\t 4." + ts4[index]);
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Select the station from above...\n");

            while (true) // this loop run until user enter correct value
            {
                Console.Write(" From Station :");
                book_from[book] = Console.ReadLine();
                // check station name entered by user is valid or not
                if (book_from[book] == ts1[index] || book_from[book] == ts2[index] || book_from[book] == ts3[index] || book_from[book] == ts4[index])
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
                book_to[book] = Console.ReadLine();
                // check station name entered by user is valid or not
                if (book_to[book] == ts1[index] || book_to[book] == ts2[index] || book_to[book] == ts3[index] || book_to[book] == ts4[index])
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
                book_day[book] = int.Parse(Console.ReadLine());
                book_month[book] = int.Parse(Console.ReadLine());
                book_year[book] = int.Parse(Console.ReadLine());

                float d = book_day[book];
                float m = book_month[book];
                float y = book_year[book];

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
            Console.WriteLine("Price per kg :" + tcargo[index]);

            while (true) // validation on weight
            {
                Console.Write("Enter the cargo weight (kg) :");
                weight[book] = int.Parse(Console.ReadLine());

                if (weight[book] > 500 || weight[book] <= 0) // user canot enter more than 500 kg weight
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
            c_price[book] = tcargo[index] * weight[book];

            Console.WriteLine();
            Console.WriteLine("You have to pay :" + c_price[book]);
            Console.Write("You want to book cargo (1 for yes, 0 for not) :");
            char op;
            op = char.Parse(Console.ReadLine());
            confirming_book_cargo(op);
        }

        // function for printing on screen that cargo booked or not
        static void confirming_book_cargo(char flag)
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
                Console.WriteLine("  Train Name   :" + cargo_train[book]);
                Console.WriteLine("  From station :" + book_from[book]);
                Console.WriteLine("  To station   :" + book_to[book]);
                Console.WriteLine("  Date         :" + book_day[book] + "-" + book_month[book] + "-" + book_year[book]);
                Console.WriteLine("  Weight       :" + weight[book]);
                Console.WriteLine("  Price        :" + c_price[book]);
                Console.WriteLine();
                Console.WriteLine(" **** Your cargo succesfully booked ***");
                book_date[book] = book_day[book] + (book_month[book] * (float)30.417);
                booking_no[book] = book + 1;

                Console.WriteLine();
                book++;
            }
            else // if not booked creating arrays index null
            {
                weight[book] = 0;
                c_price[book] = 0;
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
        static void view_notice()
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
        static void my_tickets()
        {
            head();
            Console.WriteLine(" User >> My Tickets ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();

            sort_my_tickets(); // calling function for sorting

            print_tickets(); // calling function for displaying tickets

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // this function sort the tickets according to the date which comes first
        static void sort_my_tickets()
        {
            float min;
            int index = 0;
            for (int idx = 0; idx < t; idx++) // it takes value from array and compare with inner loop value
            {
                min = 112122221; // imaginary minimum vlue
                for (int x = idx; x < t; x++)
                {
                    if (min > date[x]) // if the selected array value is minimum
                    {
                        min = date[x];
                        index = x; // storing the index at which we find minimum
                    }
                }

                swaping_ticket(idx, index); // calling function for swaping
            }
        }

        // function for swaping the values of parallel arrays
        static void swaping_ticket(int value, int idx)
        {
            // temporary variables
            float temp;
            int temp2;
            string change;

            // swaping date
            temp = date[idx];
            date[idx] = date[value];
            date[value] = temp;

            // swaping train name
            change = t_name[idx];
            t_name[idx] = t_name[value];
            t_name[value] = change;

            // swaping departure station
            change = from[idx];
            from[idx] = from[value];
            from[value] = change;

            // swaping arrival station
            change = to[idx];
            to[idx] = to[value];
            to[value] = change;

            // swaping day
            temp = day[idx];
            day[idx] = day[value];
            day[value] = temp;

            // swaping month
            temp = month[idx];
            month[idx] = month[value];
            month[value] = temp;

            // swaping year
            temp = year[idx];
            year[idx] = year[value];
            year[value] = temp;

            // swaping quantity
            temp2 = quantity[idx];
            quantity[idx] = quantity[value];
            quantity[value] = temp2;

            // swaping price
            temp2 = price[idx];
            price[idx] = price[value];
            price[value] = temp2;

            // swaping ticket number
            temp2 = ticket_no[idx];
            ticket_no[idx] = ticket_no[value];
            ticket_no[value] = temp2;
        }

        // function for displaying tickets on screen
        static void print_tickets()
        {
            int flag = 0;
            for (int idx = 0; idx < ticket_a; idx++) // loop run for booked cargo
            {
                // if ticket is buyed
                if (date[idx] != 0) // if date is not equal to zero
                {
                    Console.WriteLine("  *** Ticket no. " + ticket_no[idx] + " ***");
                    Console.WriteLine("   Train    : " + t_name[idx]);
                    Console.WriteLine("   From     : " + from[idx]);
                    Console.WriteLine("   To       : " + to[idx]);
                    Console.WriteLine("   Date     : " + day[idx] + "-" + month[idx] + "-" + year[idx]);
                    Console.WriteLine("   Quantity : " + quantity[idx]);
                    Console.WriteLine("   Price    : " + price[idx]);
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

        static void my_booked_cargo()
        {
            head();
            Console.WriteLine(" User >> My Booked Cargo ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();

            sort_my_cargo(); // calling function for sorting

            print_booked_cargo(); // calling function for displaying tickets

            Console.WriteLine();
            Console.Write("  Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        static void sort_my_cargo()
        {
            float min;
            int index = 0;
            for (int idx = 0; idx < t; idx++) // it takes value from array and compare with inner loop value
            {
                min = 112122221; // imaginary minimum value
                for (int x = idx; x < 4; x++)
                {
                    if (min > book_date[x]) // if the selected array value is minimum
                    {
                        min = book_date[x];
                        index = x; // storing the index at which we find minimum
                    }
                }

                swaping_cargo_booked_arrays(idx, index); // calling function for swaping
            }
        }

        static void swaping_cargo_booked_arrays(int value, int index)
        {
            // temporary variables
            float temp;
            int temp2;
            string change;

            // swaping date
            temp = book_date[index];
            book_date[index] = book_date[value];
            book_date[value] = temp;

            // swaping train name
            change = cargo_train[index];
            cargo_train[index] = cargo_train[value];
            cargo_train[value] = change;

            // swaping departure station
            change = book_from[index];
            book_from[index] = book_from[value];
            book_from[value] = change;

            // swaping arrival station
            change = book_to[index];
            book_to[index] = book_to[value];
            book_to[value] = change;

            // swaping day
            temp = book_day[index];
            book_day[index] = book_day[value];
            book_day[value] = temp;

            // swaping month
            temp = book_month[index];
            book_month[index] = book_month[value];
            book_month[value] = temp;

            // swaping year
            temp = book_year[index];
            book_year[index] = book_year[value];
            book_year[value] = temp;

            // swaping quantity
            temp2 = weight[index];
            weight[index] = weight[value];
            weight[value] = temp2;

            // swaping price
            temp2 = c_price[index];
            c_price[index] = c_price[value];
            c_price[value] = temp2;
        }

        // function for displaying booked cargo on screen
        static void print_booked_cargo()
        {
            int flag = 0;
            for (int idx = 0; idx < ticket_a; idx++) // loop run for booked cargo
            {
                // if ticket is buyed
                if (book_date[idx] != 0) // if date is not equal to zero
                {
                    Console.WriteLine("  *** Booking no. " + booking_no[idx] + " ***");
                    Console.WriteLine("   Train  : " + cargo_train[idx]);
                    Console.WriteLine("   From   : " + book_from[idx]);
                    Console.WriteLine("   To     : " + book_to[idx]);
                    Console.WriteLine("   Date   : " + book_day[idx] + "-" + book_month[idx] + "-" + book_year[idx]);
                    Console.WriteLine("   Weight : " + weight[idx]);
                    Console.WriteLine("   Price  : " + c_price[idx]);
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
        static void delete_route()
        {
            Console.Clear();
            int op;
            char flag;

            // calling lists of routes
            op = list_of_trains("Admin", "Delete Route");

            op = op - 1;

            Console.WriteLine();
            // asking are you sure
            Console.WriteLine("Are you sure you want to delete the route! ");
            Console.Write("Press 1 for Yes or Press any key for Not ");
            flag = char.Parse(Console.ReadLine());

            if (flag == '1') // if he want to delete route
            {
                Console.WriteLine("\n Route : " + train[op]);
                Console.WriteLine(" *** Deleted Successfully *** ");
                train[op] = "\0"; // making train name null

                // making stations name null
                ts1[op] = "\0";
                ts2[op] = "\0";
                ts3[op] = "\0";
                ts4[op] = "\0";

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
                tcargo[op] = 0;

                update_array(op); // and updating all arrays
            }
            Console.WriteLine();
            Console.WriteLine("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for updating all arrays after deleting one route
        static void update_array(int op)
        {
            for (int x = op; x < routes - 1; x++)
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
            routes--;
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
        static void change_train_name()
        {
            Console.Clear();
            int idx, a = 1;
            idx = list_of_trains("Admin", "Moify Route");

            idx--;
            Console.WriteLine("\n\n");
            Console.WriteLine(" Old Train Name " + train[idx]); // old train name

            while (true)
            {
                a = 1;
                Console.WriteLine(" Enter New train name ");
                train[idx] = Console.ReadLine(); // taking input of new trin name

                for (int x = 0; x < routes; x++)
                {
                    if (x == idx) // if user input same old name than continue
                    {
                        break;
                    }
                    if (train[x] == train[idx]) // if the name alredy exist in array give error
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
        static void change_train_stations()
        {
            Console.Clear();
            int idx;
            idx = list_of_trains("Admin", "Moify Route");
            idx = idx - 1;

            Console.WriteLine();
            Console.WriteLine("Train Name : " + train[idx]);

            Console.WriteLine("Old Station 1 Name : " + ts1[idx]);
            Console.Write("New Station-1 name :"); // station 1 name
            ts1[idx] = Console.ReadLine();
            Console.WriteLine("Note : use 24 hours format for input time ");

            while (true)
            {
                Console.Write("Arrival Time( hh:mm ) :"); // arrival time station 1
                ts1_ath[idx] = int.Parse(Console.ReadLine());               // hour
                ts1_atm[idx] = int.Parse(Console.ReadLine());               // minute
                if (ts1_ath[idx] >= 1 && ts1_ath[idx] <= 24 && ts1_atm[idx] >= 0 && ts1_atm[idx] <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            while (true)
            {
                Console.Write("Departure Time( hh:mm ) :"); // arrival time station 1
                ts1_dth[idx] = int.Parse(Console.ReadLine());                 // hour
                ts1_dtm[idx] = int.Parse(Console.ReadLine());                 // minute
                if (ts1_dth[idx] >= 1 && ts1_dth[idx] <= 24 && ts1_dtm[idx] >= 0 && ts1_dtm[idx] <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            Console.WriteLine("Old Station 2 Name : " + ts2[idx]);
            Console.Write("New Station-2 name :"); // station 2 name
            ts2[idx] = Console.ReadLine();

            while (true)
            {
                Console.Write("Arrival Time( hh:mm ) :"); // arrival time station 1
                ts2_ath[idx] = int.Parse(Console.ReadLine());               // hour
                ts2_atm[idx] = int.Parse(Console.ReadLine());               // minute
                if (ts2_ath[idx] >= 1 && ts2_ath[idx] <= 24 && ts2_atm[idx] >= 0 && ts2_atm[idx] <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            while (true)
            {
                Console.Write("Departure Time( hh:mm ) :"); // arrival time station 1
                ts2_dth[idx] = int.Parse(Console.ReadLine());                 // hour
                ts2_dtm[idx] = int.Parse(Console.ReadLine());                 // minute
                if (ts2_dth[idx] >= 1 && ts2_dth[idx] <= 24 && ts2_dtm[idx] >= 0 && ts2_dtm[idx] <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            Console.WriteLine("Old Station 3 Name : " + ts2[idx]);
            Console.Write("New Station-3 name :"); // station 3 name
            ts3[routes] = Console.ReadLine();

            while (true)
            {
                Console.Write("Arrival Time( hh:mm ) :"); // arrival time station 1
                ts3_ath[idx] = int.Parse(Console.ReadLine());               // hour
                ts3_atm[idx] = int.Parse(Console.ReadLine());               // minute
                if (ts3_ath[idx] >= 1 && ts3_ath[idx] <= 24 && ts3_atm[idx] >= 0 && ts3_atm[idx] <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            while (true)
            {
                Console.Write("Departure Time( hh:mm ) :"); // arrival time station 1
                ts2_dth[idx] = int.Parse(Console.ReadLine());                 // hour
                ts2_dtm[idx] = int.Parse(Console.ReadLine());                 // minute
                if (ts2_dth[idx] >= 1 && ts2_dth[idx] <= 24 && ts2_dtm[idx] >= 0 && ts2_dtm[idx] <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            Console.WriteLine("Old Station 4 Name : " + ts2[idx]);
            Console.Write("Station-4 name :"); // station 4 name
            ts4[routes] = Console.ReadLine();

            while (true)
            {
                Console.Write("Arrival Time( hh:mm ) :"); // arrival time station 1
                ts4_ath[idx] = int.Parse(Console.ReadLine());               // hour
                ts4_atm[idx] = int.Parse(Console.ReadLine());               // minute
                if (ts4_ath[idx] >= 1 && ts4_ath[idx] <= 24 && ts4_atm[idx] >= 0 && ts4_atm[idx] <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }

            while (true)
            {
                Console.Write("Departure Time( hh:mm ) :"); // arrival time station 1
                ts2_dth[idx] = int.Parse(Console.ReadLine());                 // hour
                ts2_dtm[idx] = int.Parse(Console.ReadLine());                 // minute
                if (ts2_dth[idx] >= 1 && ts2_dth[idx] <= 24 && ts2_dtm[idx] >= 0 && ts2_dtm[idx] <= 59)
                {
                    break;
                }
                Console.WriteLine(" Invalid Time ! ");
            }
            add_station_to_array();
            Console.WriteLine(" Train Stations changed Succesfully.");
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        // function for adding new station in array
        static void add_station_to_array()
        {
            int n1 = 0, n2 = 0, n3 = 0, n4 = 0;
            for (int i = 0; i < st; i++) // loop for checking is name already exist or not
            {
                if (stations[i] != ts1[routes]) // checking name in station 1 array
                {
                    n1++;
                    if (n1 == st) // if name does not find
                    {
                        stations[st] = ts1[routes]; // than add it in array
                        st++;
                        break;
                    }
                }
                if (stations[i] != ts2[routes]) // checking name in station 2 array
                {
                    n2++;
                    if (n2 == st) // if name does not find
                    {
                        stations[st] = ts1[routes]; // than add it in array
                        st++;
                        break;
                    }
                }
                if (stations[i] != ts3[routes]) // checking name in station 3 array
                {
                    n3++;
                    if (n3 == st) // if name does not find
                    {
                        stations[st] = ts1[routes]; // than add it in array
                        st++;
                        break;
                    }
                }
                if (stations[i] != ts4[routes]) // checking name in station 4 array
                {
                    n4++;
                    if (n4 == st) // if name does not find
                    {
                        stations[st] = ts1[routes]; // than add it in array
                        st++;
                        break;
                    }
                }
            }
        }

        // function for loading data from file to arrays and variables
        static void load_data()
        {
            string path1 = "Train_Routes_Data.txt";
            string line;

            if (File.Exists(path1))
            {
                StreamReader file = new StreamReader(path1);
                while ((line = file.ReadLine()) != null)
                {

                    train[routes] = parse_data(line, 1); // reading train names

                    ts1[routes] = parse_data(line, 2); // reading trains station 1 name
                    ts2[routes] = parse_data(line, 3); // reading trains station 2 name
                    ts3[routes] = parse_data(line, 4); // reading trains station 3 name
                    ts4[routes] = parse_data(line, 5); // reading trains station 4 name

                    ts1_ath[routes] = int.Parse(parse_data(line, 6)); // reading station 1 arrival time hour
                    ts1_atm[routes] = int.Parse(parse_data(line, 7)); // reading station 1 arrival time minutes
                    ts1_dth[routes] = int.Parse(parse_data(line, 8)); // reading station 1 departure time hour
                    ts1_dtm[routes] = int.Parse(parse_data(line, 9)); // reading station 1 departure time minutes

                    ts2_ath[routes] = int.Parse(parse_data(line, 10)); // reading station 2 arrival time hour
                    ts2_atm[routes] = int.Parse(parse_data(line, 11)); // reading station 2 arrival time minutes
                    ts2_dth[routes] = int.Parse(parse_data(line, 12)); // reading station 2 departure time hour
                    ts2_dtm[routes] = int.Parse(parse_data(line, 13)); // reading station 2 departure time minutes

                    ts3_ath[routes] = int.Parse(parse_data(line, 14)); // reading station 3 arrival time hour
                    ts3_atm[routes] = int.Parse(parse_data(line, 15)); // reading station 3 arrival time minutes
                    ts3_dth[routes] = int.Parse(parse_data(line, 16)); // reading station 3 departure time hour
                    ts3_dtm[routes] = int.Parse(parse_data(line, 17)); // reading station 3 departure time minutes

                    ts4_ath[routes] = int.Parse(parse_data(line, 18)); // reading station 4 arrival time hour
                    ts4_atm[routes] = int.Parse(parse_data(line, 19)); // reading station 4 arrival time minutes
                    ts4_dth[routes] = int.Parse(parse_data(line, 20)); // reading station 4 departure time hour
                    ts4_dtm[routes] = int.Parse(parse_data(line, 21)); // reading station 4 departure time minutes

                    tticket[routes] = int.Parse(parse_data(line, 22)); // raeding trains tickets prices
                    tcargo[routes] = int.Parse(parse_data(line, 23));  // reading cargo routes of trains
                    routes++;
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
                    stations[st] = line;
                    st++; // increment in stations count
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
                    t_name[t] = parse_data(line, 1);          // ticket train name
                    from[t] = parse_data(line, 2);            // departure station
                    to[t] = parse_data(line, 3);              // arrival station
                    quantity[t] = int.Parse(parse_data(line, 4));  // quantity of tickets
                    ticket_no[t] = int.Parse(parse_data(line, 5)); // ticket number
                    price[t] = int.Parse(parse_data(line, 6));     // price of tickets
                    day[t] = float.Parse(parse_data(line, 7));       // day of ticket
                    month[t] = float.Parse(parse_data(line, 8));     // month of ticket
                    year[t] = float.Parse(parse_data(line, 9));      // year of ticket
                    date[t] = float.Parse(parse_data(line, 10));     // calculated date for applying conditions
                    t++;                                      // increment in tickets count
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
                    cargo_train[book] = parse_data(line, 1);      // cargo booked train name
                    book_from[book] = parse_data(line, 2);        // departure station
                    book_to[book] = parse_data(line, 3);          // arrival station
                    weight[book] = int.Parse(parse_data(line, 4));     // weight of cargo
                    c_price[book] = int.Parse(parse_data(line, 5));    // cargo booking price
                    booking_no[book] = int.Parse(parse_data(line, 6)); // booking number
                    book_day[book] = float.Parse(parse_data(line, 7));   // booking day
                    book_month[book] = float.Parse(parse_data(line, 8)); // booking month
                    book_year[book] = float.Parse(parse_data(line, 9));  // booking year
                    book_date[book] = float.Parse(parse_data(line, 10)); // calculated date for applying conditions

                    book++; // increment in cargo booked
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
        static void store_data()
        {
            string path1 = "Train_Routes_Data.txt";

            // writing data of train routes into file
            StreamWriter file = new StreamWriter(path1);
            //file.open("Train_Routes_Data.txt", ios::out); // opening file for writing
            for (int idx = 0; idx < routes; idx++)        // changing index of arrays
            {
                file.Write(train[idx] + "," + ts1[idx] + "," + ts2[idx] + "," + ts3[idx] + "," + ts4[idx] + ",");
                file.Write(ts1_ath[idx] + "," + ts1_atm[idx] + "," + ts1_dth[idx] + "," + ts1_dtm[idx] + ",");
                file.Write(ts2_ath[idx] + "," + ts2_atm[idx] + "," + ts2_dth[idx] + "," + ts2_dtm[idx] + ",");
                file.Write(ts3_ath[idx] + "," + ts3_atm[idx] + "," + ts3_dth[idx] + "," + ts3_dtm[idx] + ",");
                file.Write(ts4_ath[idx] + "," + ts4_atm[idx] + "," + ts4_dth[idx] + "," + ts4_dtm[idx] + ",");
                file.Write(tticket[idx] + "," + tcargo[idx] + ",");

                if (idx < routes - 1)
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
            for (int idx = 0; idx < st; idx++)
            {
                st_file.Write(stations[idx]);
                if (idx < st - 1)
                {
                    st_file.WriteLine();
                }
            }
            st_file.Close();

            string path4 = "tickets_data.txt";
            // writing tickets data into file
            StreamWriter newFile = new StreamWriter(path4);
            // newFile.open("tickets_data.txt", ios::out); // opening file
            for (int idx = 0; idx < t; idx++)
            {
                newFile.Write(t_name[idx] + "," + from[idx] + "," + to[idx] + ",");
                newFile.Write(quantity[idx] + "," + ticket_no[idx] + "," + price[idx] + ",");
                newFile.Write(day[idx] + "," + month[idx] + "," + year[idx] + "," + date[idx] + ",");
                if (idx < t - 1)
                {
                    newFile.WriteLine();
                }
            }
            newFile.Close(); // closing file

            string path5 = "booked_cargo_data.txt";
            // writing cargo booking data into file
            StreamWriter cargo_File = new StreamWriter(path5);
            // cargo_File.open("booked_cargo_data.txt", ios::out); // opening file
            for (int idx = 0; idx < book; idx++)
            {
                cargo_File.Write(cargo_train[idx] + "," + book_from[idx] + "," + book_to[idx] + ",");
                cargo_File.Write(weight[idx] + "," + c_price[idx] + "," + booking_no[idx] + ",");
                cargo_File.Write(book_day[idx] + "," + book_month[idx] + "," + book_year[idx] + "," + book_date[idx] + ",");

                if (idx < book - 1)
                {
                    cargo_File.WriteLine();
                }
            }
            cargo_File.Close(); // closing file
        }

    }
}
