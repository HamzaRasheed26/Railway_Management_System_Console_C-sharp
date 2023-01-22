using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_V3.BL
{
    internal class TrainCargo
    {
        // arrays that used for booking cargo
        public string cargo_train; // for storing the name of train in which cargo is booked
        public string book_from;  // for storing the name of departure station
        public string book_to;    // for storing the name of arrival station
        public int weight;       // for weight of cargo
        public int c_price;       // for cargo deleviring price user have to pay
        public int booking_no;    // for the number of booking

        // arrays for date of booking
        public float book_day, book_month, book_year;
        public float book_date;

        public void calculateDate()
        {
            book_date = book_day + (book_month * 30.417F);
        }

        public void print()
        {
            Console.WriteLine("  *** Booking no. " + booking_no + " ***");
            Console.WriteLine("   Train  : " + cargo_train);
            Console.WriteLine("   From   : " + book_from);
            Console.WriteLine("   To     : " + book_to);
            Console.WriteLine("   Date   : " + book_day + "-" + book_month + "-" + book_year);
            Console.WriteLine("   Weight : " + weight);
            Console.WriteLine("   Price  : " + c_price);
            Console.WriteLine("\n");
        }

    }
}
