using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_V4.BL
{
    internal class Booking
    {
        protected string trainName; // for storing the name of train in which cargo is booked
        protected string from;  // for storing the name of departure station
        protected string to;    // for storing the name of arrival station
        protected float price;       // for cargo deleviring price user have to pay
        protected int booking_no;    // for the number of booking
        // for date of booking
        protected float day;
        protected float month;
        protected float year;
        protected float date;

        public Booking(string trainName, string from, string to, float price, int booking_no, float day, float month, float year)
        {
            this.trainName = trainName;
            this.from = from;
            this.to = to;
            this.price = price;
            this.booking_no = booking_no;
            this.day = day;
            this.month = month;
            this.year = year;
            date = 0;
        }

        public Booking(string trainName, string from, string to, float price, int booking_no, float day, float month, float year, float date)
        {
            this.trainName = trainName;
            this.from = from;
            this.to = to;
            this.price = price;
            this.booking_no = booking_no;
            this.day = day;
            this.month = month;
            this.year = year;
            this.date = date;
        }

        public string TrainName { get => trainName; set => trainName = value; }
        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public float Price { get => price; set => price = value; }
        public int Booking_no { get => booking_no; set => booking_no = value; }
        public float Day { get => day; set => day = value; }
        public float Month { get => month; set => month = value; }
        public float Year { get => year; set => year = value; }
        public float Date { get => date; set => date = value; }

        public float calculateDate()
        {
            date = day + (month * 30.417F);
            return date;
        }
    }
}
