using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_V2.BL
{
    internal class BookingType
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
    }
}
