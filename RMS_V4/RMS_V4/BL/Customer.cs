using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_V4.BL
{
    internal class Customer
    {
        private string name;

        private List<TrainCargo> cargoBooked;
        private List<TrainTicket> tickets;

        public Customer(string name, List<TrainCargo> cargoBooked, List<TrainTicket> tickets)
        {
            this.name = name;
            this.cargoBooked = cargoBooked;
            this.tickets = tickets;
        }

        public Customer(string name)
        {
            this.name = name;
            cargoBooked = new List<TrainCargo>() ;
            tickets = new List<TrainTicket>();
        }

        public string Name { get => name; set => name = value; }
        public List<TrainCargo> CargoBooked { get => cargoBooked; set => cargoBooked = value; }
        public List<TrainTicket> Tickets { get => tickets; set => tickets = value; }
    }
}
