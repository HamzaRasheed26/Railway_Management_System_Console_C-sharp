using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS_V4.BL;
using System.IO;

namespace RMS_V4.DL
{
    internal class TrainTicketDL
    {
        private static List<TrainTicket> ticketList = new List<TrainTicket>();

        public static void addIntoList(TrainTicket t)
        {
            ticketList.Add(t);
        }

        public static List<TrainTicket> getTicketList()
        {
            return ticketList;
        }

        public static TrainTicket getSingleObject(int index)
        {
            if(index >= 0 && index < ticketList.Count )
            {
                return ticketList[index];
            }
            return null;
        }

        public static int ListCount()
        {
            return ticketList.Count;
        }

        public static void loadDataFromFile(string path)
        {
            string line;

            // reading tickets data from files
            if (File.Exists(path))
            {
                StreamReader newFile = new StreamReader(path);
                while ((line = newFile.ReadLine()) != null)
                {
                    
                    string[] splittedRecord = line.Split(',');
                    string traiName = splittedRecord[0];          // ticket train name
                    string from = splittedRecord[1];            // departure station
                    string to = splittedRecord[2];              // arrival station
                    int quantity = int.Parse(splittedRecord[3]);  // quantity of tickets
                    int ticket_no = int.Parse(splittedRecord[4]); // ticket number
                    float price = int.Parse(splittedRecord[5]);     // price of tickets
                    int day = int.Parse(splittedRecord[6]);       // day of ticket
                    int month = int.Parse(splittedRecord[7]);     // month of ticket
                    int year = int.Parse(splittedRecord[8]);      // year of ticket
                    float date = float.Parse(splittedRecord[9]);     // calculated date for applying conditions
                     // temporary for reding from files
                    TrainTicket temp = new TrainTicket(traiName, from, to, quantity, price, ticket_no, month, day, year, date);
                    addIntoList(temp); // adding in the list of buyed tickets
                }
                newFile.Close(); // closing file
            }
        }


        public static void storeDataIntoFile(string path)
        {
            // writing tickets data into file
            StreamWriter newFile = new StreamWriter(path);
            int idx = 0;
            foreach (TrainTicket t in ticketList)
            {
                newFile.Write(t.TrainName + "," + t.From + "," + t.To + ",");
                newFile.Write(t.Quantity + "," + t.Booking_no + "," + t.Price + ",");
                newFile.Write(t.Day + "," + t.Month + "," + t.Year + "," + t.Date + ",");
                
                if (idx < ticketList.Count - 1)
                {
                    newFile.WriteLine();
                }
                idx++;
            }
            newFile.Close(); // closing file
        }
        // this function sort the tickets according to the date which comes first
        /*public static void sort_my_tickets()
        {
            float min;
            int index = 0;
            for (int idx = 0; idx < ticketList.Count; idx++) // it takes value from array and compare with inner loop value
            {
                min = 112122221; // imaginary minimum vlue
                for (int x = idx; x < ticketList.Count; x++)
                {
                    if (min > ticketList[x].date) // if the selected array value is minimum
                    {
                        min = ticketList[x].date;
                        index = x; // storing the index at which we find minimum
                    }
                }

                swaping_ticket(idx, index); // calling function for swaping
            }
        }*/

        /* // function for swaping the values of parallel arrays
         public static void swaping_ticket(int value, int idx)
         {
             TrainTicket temp1 = ticketList[idx];
             ticketList[idx] = ticketList[value];
             ticketList[value] = temp1;
         }*/

        public static List<TrainTicket> sortTicketList()
        {
            if (ticketList != null)
            {
                List<TrainTicket> sortedTicketList = ticketList.OrderBy(o => o.Date).ToList();
                return sortedTicketList;
            }

            return null;
        }

        public static bool isDateValid(float d, float m, float y)
        {
            // check on year
            if (y == 2022)
            {
                // check on month
                if (m == 1 || m == 3 || m == 5 || m == 7 || m == 8 || m == 10 || m == 12)
                {
                    // check on day range from 1 to 31
                    if (d >= 1 && d <= 31)
                    {
                        return true;
                    }
                }
                // check on month
                else if (m == 4 || m == 6 || m == 9 || m == 11)
                {
                    // check on  day range from 1 to 30
                    if (d >= 1 && d <= 30)
                    {
                        return true;
                    }
                }
                // check on month of febuary
                else if (m == 2)
                {
                    // check on day range from 1 to 28
                    if (d >= 1 && d <= 28)
                    {
                        return true;
                    }
                }
            }
            return false;

        }
    }
}
