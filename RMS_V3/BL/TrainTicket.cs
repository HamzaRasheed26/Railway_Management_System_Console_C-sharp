using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_V3.BL
{
    internal class TrainTicket
    {
        //  that used for buying tickets
        public string t_name; // for train name
        public string from;   // for departure station
        public string to;     // for arrival station
        public int quantity;  // for quantity of tickets
        public int ticket_no; // for the number of ticket
        public int price;    // for price of tickets user have to pay

        //  for time date of ticket
        public float day, month, year;
        public float date;

        // Member Functions
        public void calculateDate()
        {
            date = day + (month * 30.417F);
        }

        public void print()
        {
            Console.WriteLine("  *** Ticket no. " + ticket_no + " ***");
            Console.WriteLine("   Train    : " + t_name);
            Console.WriteLine("   From     : " + from);
            Console.WriteLine("   To       : " + to);
            Console.WriteLine("   Date     : " + day + "-" + month + "-" + year);
            Console.WriteLine("   Quantity : " + quantity);
            Console.WriteLine("   Price    : " + price);
            Console.WriteLine("\n");
            Console.WriteLine();
        }


    }
}
