using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_V4.BL
{
    internal class TrainCargo : Booking
    {   
        private float weight;       // for weight of cargo  

        public TrainCargo(string trainName, string from, string to, float weight, float price, int booking_no, float day, float month, float year) : base(trainName, from, to, price, booking_no, day, month, year)
        {
            this.weight = weight;
        }

        public TrainCargo(string trainName, string from, string to, float weight, float price, int booking_no, float day, float month, float year, float date) : base(trainName, from, to, price, booking_no, day, month, year, date)
        {
            this.weight = weight;   
        }

        public float Weight { get => weight; set => weight = value; }

        public void print()
        {
            Console.WriteLine("  *** Booking no. " + booking_no + " ***");
            Console.WriteLine("   Train  : " + trainName);
            Console.WriteLine("   From   : " + from);
            Console.WriteLine("   To     : " + to);
            Console.WriteLine("   Date   : " + day + "-" + month + "-" + year);
            Console.WriteLine("   Weight : " + weight);
            Console.WriteLine("   Price  : " + price);
            Console.WriteLine("\n");
        }

    }
}
