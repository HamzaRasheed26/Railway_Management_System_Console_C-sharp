using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RMS_V4.BL;

namespace RMS_V4.DL
{
    internal class MUserDL
    {
        public static List<MUser> UsersList = new List<MUser>();

        public static bool readData(string path)
        {

            string name, password, role;

            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                string line;


                while ((line = file.ReadLine()) != null)
                {
                    string[] splittedRecord = line.Split(',');

                    name = splittedRecord[0];
                    password = splittedRecord[1];
                    role = splittedRecord[2];

                    MUser user = new MUser(name, password, role);
                    AddUserIntoList(user);
                }
                file.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void storeData(string path)
        {

            StreamWriter file = new StreamWriter(path);
            int i = 0;
            foreach (MUser user in UsersList)
            {
                i++;
                file.Write(user.getUsername() + "," + user.getPassword() + "," + user.getRole());
                if (i < UsersList.Count)
                {
                    file.WriteLine();
                }
            }

            file.Flush();
            file.Close();
        }

        public static void AddUserIntoList(MUser User)
        {
            UsersList.Add(User);
        }

        public static bool isExist(MUser u)
        {
            foreach (MUser user in UsersList)
            {
                if (user.getUsername() == u.getUsername() && user.getPassword() == u.getPassword())
                {
                    return true;
                }
            }
            return false;
        }

        public static MUser findUser(MUser user)
        {
            foreach (MUser u in MUserDL.UsersList)
            {
                if (user.getUsername() == u.getUsername() && user.getPassword() == u.getPassword())
                {
                    if (user.getRole() == "Customer")
                    {
                        CustomerDL.initializeCustomer(u.getUsername());
                    }

                    return u;
                }
            }
            return null;
        }
    }
}
