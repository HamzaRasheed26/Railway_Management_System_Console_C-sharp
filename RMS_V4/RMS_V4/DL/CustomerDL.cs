using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RMS_V4.BL;

namespace RMS_V4.DL
{
    internal class CustomerDL
    {
        private static List<Customer> customerList = new List<Customer>();

        public static Customer presentCustomer;

        public static void initializeCustomer(string name)
        {
            presentCustomer = new Customer(name);
        }

        public static List<Customer> getList()
        {
            return customerList;
        }

        public static void addIntoList(Customer c)
        {
            customerList.Add(c);
        }

        public static Customer getByIndex(int index)
        {
            if (index >= 0 && index < customerList.Count)
            {
                return customerList[index];
            }
            return null;
        }
    }
}
