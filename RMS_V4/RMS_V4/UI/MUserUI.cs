using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS_V4.BL;
using RMS_V4.DL;

namespace RMS_V4.UI
{
    internal class MUserUI
    {
        public static MUser SignUp()
        {
            string password, username, role;

            Console.Clear();

            Console.WriteLine("Enter your Name : ");
            username = Console.ReadLine();
            Console.WriteLine("Enter Pasword : ");
            password = Console.ReadLine();

            role = "user";

            MUser temp = new MUser(username, password, role);

            return temp;
        }

        public static MUser signin()
        {
            string password, username;

            Console.Clear();
            RMSUI.head();
            Console.Write("Enter UserName : ");
            username = Console.ReadLine();
            Console.Write("Enter Password : ");
            password = Console.ReadLine();

            MUser user = new MUser(username, password);

            return user;
        }

        public static char LoginPage()
        {
            Console.Clear();
            RMSUI.head();
            char option;
            Console.WriteLine(" Login Page >>");
            Console.WriteLine(" 1. Sign In");
            Console.WriteLine(" 2. Sign Up");
            Console.WriteLine(" 3. Exit");
            Console.WriteLine("Your Option : ");
            option = char.Parse(Console.ReadLine());
            return option;
        }
    }
}
