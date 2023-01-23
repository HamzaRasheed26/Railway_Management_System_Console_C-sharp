using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS_V4.BL;
using RMS_V4.DL;

namespace RMS_V4.UI
{
    internal class TrainTicketUI
    {
        // function for buying ticket of train
        public static TrainTicket buy_ticket(Route route)
        {
            string trainName, from, to;
            float price;
            int day, month, year, quantity;

            trainName = route.TrainName;

            Console.Clear();
            RMSUI.head();

            Console.WriteLine(" User >> Buy Tickets ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Train Name :" + trainName);

            // this line print the stations name that are available on this train route
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine("Stations available :");
            int idx = 1;
            foreach (Station st in route.Stations)
            {
                Console.WriteLine("\t" + idx + ". " + st.StationName);
                idx++;
            }

            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Select the station from above...");
            Console.WriteLine(" ");

            while (true) // this loop run until user enter correct value
            {
                Console.Write(" From Station : ");
                from = Console.ReadLine();
               
                // check station name entered by user is valid or not
                
                if (route.isStationExist(from))
                {
                    break;
                }
                else // if station name does not match
                {
                    Console.WriteLine(" Invalid Station Name !");
                    Console.WriteLine(" Again Input ");
                }
            }
            while (true) // this loop run until user enter correct value
            {
                Console.Write(" To Station :");
                to = Console.ReadLine();
                
                // check station name entered by user is valid or not
                if (route.isStationExist(to))
                {
                    break;
                }
                else // if station name does not match
                {
                    Console.WriteLine(" Invalid Station Name !");
                    Console.WriteLine(" Again Input ");
                }
            }

            while (true) // validation for date
            {
                Console.Write(" Enter date ( dd mm yyyy) :");
                day = int.Parse(Console.ReadLine());
                month = int.Parse(Console.ReadLine());
                year = int.Parse(Console.ReadLine());

                
                if (TrainTicketDL.isDateValid(day, month, year))
                {
                    break;
                }
                Console.WriteLine("\n Invalid Date ! ");
                Console.WriteLine(" Again enter date please.");
            }

            Console.WriteLine(" Ticket price is :" + route.TicketPrice);

            while (true) // validation for quantity
            {
                Console.Write(" Enter quantity of tickets :");
                quantity = int.Parse(Console.ReadLine());

                if (quantity > 12 || quantity <= 0) // quantity cannot be greater than 12
                {
                    Console.WriteLine(" Error You cannot buy more than 12 quantity ! ");
                }
                else
                {
                    break;
                }
            }

            price = route.TicketPrice * quantity;

            Console.WriteLine("Total price for " + quantity + " tickets :" + price);
            // confirming for buying ticket
            Console.Write("You want to buy Ticket (1 for yes, 0 for not) :");
            char op;
            op = char.Parse(Console.ReadLine());

            TrainTicket buy = new TrainTicket(trainName, from, to, quantity, price, 0, day, month, year);
            
            if (buying_ticket_message(op, buy))
            {
                return buy;
            }
            else
            {
                return null;
            }
        }

        // function for printing on screen that ticket buyed
        public static bool buying_ticket_message(char flag, TrainTicket buy)
        {
            bool check;
            if (flag == '1') // message of buying ticket
            {
                Console.Clear();
                RMSUI.head();
                Console.WriteLine(" User >> Buy Tickets ");
                Console.WriteLine("_____________________________________________________________");
                Console.WriteLine();
                Console.WriteLine(" You buy Ticket Succesfully ***");
                Console.WriteLine();


                buy.print();

                Console.WriteLine(" ****Thanks for buying Ticket****");

                buy.calculateDate();


                check = true;
            }
            else // if not buyed than 
            {
                Console.WriteLine();
                Console.WriteLine(" Ticket not Buyed !");
                check = false;
            }

            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
            return check;
        }

        // function for viewing my tickets
        public static void my_tickets(List<TrainTicket> ticketList)
        {
            RMSUI.head();
            Console.WriteLine(" User >> My Tickets ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();

            

            if(!print_tickets(ticketList)) // calling function for displaying tickets
            {
                Console.WriteLine("No Ticket is buyed");
            }
            Console.WriteLine();
            Console.Write("Press any key for continue....");
            Console.ReadKey();
            Console.WriteLine();
        }

        


        // function for displaying tickets on screen
        public static bool print_tickets(List<TrainTicket> ticketList)
        {
            int flag = 0;
            foreach(TrainTicket ticket in ticketList) // loop run for buyed ticket in list
            {
                // if ticket is buyed
                if (ticket.Date != 0F) // if date is not equal to zero
                {
                    ticket.print();
                    flag++;
                }
            }

            // if no ticket is buyed
            if (flag == 0)
            {
                return false;
            }
            return true;
        }
    }
}
