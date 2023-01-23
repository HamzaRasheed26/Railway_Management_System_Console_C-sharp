using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_V4.UI
{
    internal class RMSUI
    {
        public static void head()
        {
            Console.WriteLine("*************************************************************");
            Console.WriteLine("*                  RAILWAY MANAGEMENT SYSTEM                *");
            Console.WriteLine("*************************************************************");
            Console.WriteLine();
        }

        // Admin menu page function
        public static char Admin_Menu()
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

        // this function is hardcode its only print data of employers
        // we cannot edit this data
        public static void view_employers_data()
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
        public static string user_menu()
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

        // functoin for developer name
        public static void developer()
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
        private static string notice = "No_notice";

        // Function for posting notices for user
        public static void add_notice()
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

        // function for viewing notice
        public static void view_notice()
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
    }
}
