using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_V2.BL
{
    internal class TicketType
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
    }
}
