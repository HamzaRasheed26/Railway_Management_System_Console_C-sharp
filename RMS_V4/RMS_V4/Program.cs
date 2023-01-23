using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RMS_V4.BL;
using RMS_V4.DL;
using RMS_V4.UI;

namespace RMS_V4
{
    internal class Program
    {
        //-----------------------------  main function------------------------------------------
        static void Main(string[] args)
        {
            string path1 = "Train_Routes_Data.txt";
            string path2 = "tickets_data.txt";
            string path3 = "booked_cargo_data.txt";
            string path4 = "stations.txt";
            string credentialFilePath = "Credentials.txt";

            MUserDL.readData(credentialFilePath);
            RouteDL.LoadDataFromFile(path1);
            TrainTicketDL.loadDataFromFile(path2);
            TrainCargoDL.loadDataFromFile(path3);
            StationDL.loadDataFromFile(path4);

            //______________________ Data structure _______________
            string password = "";
            

            // -------------------- Actual Program Runs From Here

            load_data( ref password);
            string role;

            while (true) // loop for main menu
            {
                // calling login function
                

                char op = MUserUI.LoginPage();

                if (op == '1')
                {
                    // sign in
                    MUser u  = MUserUI.signin();
                    MUser user = MUserDL.findUser(u);
                    if (user != null)
                    {
                        role = user.getRole();
                    }
                    else
                    {
                        role = "nill";
                    }

                    // __________________________ Admin Portion ________________________
                    if (role == "admin")
                    {
                        char option;

                        while (true) // loop for admin option
                        {
                            Console.Clear();
                            RMSUI.head();
                            // calling admin menu
                            option = RMSUI.Admin_Menu();
                            Console.Clear();
                            int sub_op;

                            if (option == '1')
                            {
                                // admin menu option 1 starts

                                // calling function for printing list of trains
                                int index = RouteUI.list_of_trains("Admin", "View Train Route", RouteDL.getRouteList());
                                // calling function for further train detail
                                Route route = RouteDL.GetSingleRouteByIndex(index);
                                Console.Clear();
                                RMSUI.head();
                                RouteUI.view_train_route_detail("Admin", "View Train Route", route);

                                // admin menu option 1 ends
                            }
                            else if (option == '2')
                            {
                                // admin menu option 2 starts

                                // function for adding train
                                Console.Clear();
                                RMSUI.head();
                                Route route = RouteUI.add_train_route();
                                
                                RouteDL.addIntoList(route);
                                // admin menu option 2 ends
                            }
                            else if (option == '3')
                            {
                                // admin menu option 3 starts

                                sub_op = RouteUI.edit_route(); // menu of edit route

                                if (sub_op == '1') // for option 1
                                {
                                    Console.Clear();
                                    int index;


                                    // calling lists of routeCount
                                    index = RouteUI.list_of_trains("Admin", "Delete Route", RouteDL.getRouteList());
                                    Route route = RouteDL.GetSingleRouteByIndex(index);
                                    if (RouteUI.delete_route(route.TrainName)) // deleting route
                                    {
                                        RouteDL.removeFromList(index);
                                    }
                                }
                                else if (sub_op == '2') // for option 2
                                {
                                    sub_op = RouteUI.modify_route(); // modifying route

                                    if (sub_op == '1')
                                    {
                                        int index = RouteUI.list_of_trains("Admin", "Route Route", RouteDL.getRouteList());
                                        Route route = RouteDL.GetSingleRouteByIndex(index);
                                        string newName = RouteUI.change_train_name(route.TrainName); // changing train name
                                        route.TrainName = newName;
                                    }
                                    else if (sub_op == '2')
                                    {
                                        Console.Clear();
                                        int idx;
                                        idx = RouteUI.list_of_trains("Admin", "Moify Route", RouteDL.getRouteList());
                                        Route route = RouteDL.GetSingleRouteByIndex(idx);
                                        List<Station> stationList = RouteUI.change_train_stations(route); // changing train station
                                        route.Stations = stationList;
                                    }
                                }

                                // admin menu option 3 ends
                            }
                            else if (option == '4')
                            {
                                // admin menu option 3 starts

                                int index = RouteUI.list_of_trains("Admin", "View Train Route", RouteDL.getRouteList());
                                Console.Clear();
                                RMSUI.head();

                                Route r = RouteDL.GetSingleRouteByIndex(index);
                                RouteUI.set_ticket_price(r);

                                // admin menu option 3 ends
                            }
                            else if (option == '5')
                            {
                                // admin menu option 4 starts

                                int index = RouteUI.list_of_trains("Admin", "View Train Route", RouteDL.getRouteList());
                                Console.Clear();
                                RMSUI.head();

                                Route route = RouteDL.GetSingleRouteByIndex(index);
                                RouteUI.set_freight_rate(route);

                                // admin menu option 4 ends
                            }
                            else if (option == '6')
                            {
                                // admin menu option 5 starts
                                RMSUI.head();
                                int index = StationDL.station_schedule_menu("Admin");
                                Console.Clear();
                                RMSUI.head();
                                index--;
                                string st = StationDL.GetSingleSingleByIndex(index);
                                StationDL.train_station_check("Admin", RouteDL.getRouteList(), st);

                                // admin menu option 5 ends
                            }
                            else if (option == '7')
                            {
                                // admin menu option 6 starts

                                RMSUI.add_notice();

                                // admin menu option 6 ends
                            }
                            else if (option == '8')
                            {
                                // admin menu option 7 starts

                                RMSUI.view_employers_data();

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
                    else if (role == "user")
                    {
                        string option;
                        while (true)
                        {
                            Console.Clear();
                            RMSUI.head();
                            option = RMSUI.user_menu();
                            Console.Clear();


                            if (option == "1")
                            {
                                // user menu option 1 starts

                                int index = RouteUI.list_of_trains("User", "View Train Route", RouteDL.getRouteList());
                                Route route = RouteDL.GetSingleRouteByIndex(index);
                                Console.Clear();
                                RMSUI.head();
                                RouteUI.view_train_route_detail("User", "View Train Route", route);

                                // user menu option 1 ends
                            }
                            else if (option == "2")
                            {
                                // user menu option 2 starts

                                RMSUI.head();
                                int index = StationDL.station_schedule_menu("User");
                                Console.Clear();
                                RMSUI.head();
                                index--;
                                string st = StationDL.GetSingleSingleByIndex(index);
                                StationDL.train_station_check("User", RouteDL.getRouteList(), st);

                                //sub_op = station_schedule_menu("User", stations);
                                //train_station_check("User", sub_op, RouteDL.getRouteList(), stations);

                                // user menu option 2 ends
                            }
                            else if (option == "3")
                            {
                                // user menu option 3 starts

                                RouteUI.view_tickets_price(RouteDL.getRouteList());

                                // user menu option 3 ends
                            }
                            else if (option == "4")
                            {
                                // user menu option 4 starts


                                int index = RouteUI.list_of_trains("User", "Buy Ticket", RouteDL.getRouteList());

                                Route route = RouteDL.GetSingleRouteByIndex(index);
                                TrainTicket ticket = TrainTicketUI.buy_ticket(route);

                                if (ticket != null)
                                {
                                    ticket.Booking_no = TrainTicketDL.ListCount() + 1;
                                    TrainTicketDL.addIntoList(ticket);
                                }

                                // user menu option 4 ends
                            }
                            else if (option == "5")
                            {
                                // user menu option 5 starts
                                List<TrainTicket> sortedTicketList;
                                sortedTicketList = TrainTicketDL.sortTicketList(); // calling function for sorting
                                TrainTicketUI.my_tickets(sortedTicketList);

                                // user menu option 5 ends
                            }
                            else if (option == "6")
                            {
                                // user menu option 6 starts

                                RouteUI.view_freight_rate(RouteDL.getRouteList());

                                // user menu option 6 ends
                            }
                            else if (option == "7")
                            {
                                // user menu option 7 starts

                                int index = RouteUI.list_of_trains("User", "Book Cargo", RouteDL.getRouteList());

                                Route route = RouteDL.GetSingleRouteByIndex(index);
                                TrainCargo cargo = TrainCargoUI.book_cargo(route);

                                if (cargo != null)
                                {
                                    cargo.Booking_no = TrainCargoDL.ListCount() + 1;
                                    cargo.calculateDate();
                                    TrainCargoDL.addIntoList(cargo);
                                }
                                // user menu option 7 ends
                            }
                            else if (option == "8")
                            {
                                // user menu option 8 starts
                                List<TrainCargo> sortedCargoList;
                                sortedCargoList = TrainCargoDL.sortCargoList(); // calling function for sortin
                                TrainCargoUI.my_booked_cargo(sortedCargoList);

                                // user menu option 8 ends
                            }
                            else if (option == "9")
                            {
                                // user menu option 9 starts

                                RMSUI.view_notice();

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
                    
                    // if invalid input
                    else
                    {
                        Console.WriteLine("Invalid Input!");
                    }
                }
                else if (op == '2')
                {
                    // sign up
                    MUser user = MUserUI.SignUp();
                    if (!MUserDL.isExist(user))
                    {
                        MUserDL.AddUserIntoList(user);

                        if (user.getRole() == "Customer")
                        {
                            string username = user.getUsername();
                            Customer newCustomer = new Customer(username);
                            CustomerDL.addIntoList(newCustomer);
                            //CustomerDL.storeData(customerFilePath);
                        }

                    }

                    MUserDL.storeData(credentialFilePath);
                }
                // _____________________________ Logout statement ___________________________
                else if (op == '3')
                {
                    RouteDL.storeDataIntoFlie(path1);
                    TrainTicketDL.storeDataIntoFile(path2);
                    TrainCargoDL.storeDataIntoFile(path3);
                    StationDL.storeDataIntoFile(path4);
                    store_data(password);

                    break;
                }
            }

            RMSUI.developer();
        }

        // ________________________________ Function Definitions _____________________________________________________________________

        // Railway management system head function
       


        // login page function
        static string login_page(string password)
        {
            while (true) // loop run until user enter wrong value
            {
                Console.Clear();
                RMSUI.head();

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

        // function for loading data from file to arrays and variables
        static void load_data( ref string password)
        {          
            string path2 = "user_password.txt";
            string line;

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
        }

        // function for storing data into file
        static void store_data( string password)
        {
            
            string path2 = "user_password.txt";
            // writing password into file
            StreamWriter pwd = new StreamWriter(path2);
            //pwd.open("user_password.txt", ios::out);
            pwd.Write(password);
            pwd.Close();   
        }
    }
}
