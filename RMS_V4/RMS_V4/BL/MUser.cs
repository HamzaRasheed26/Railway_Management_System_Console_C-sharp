using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS_V4.BL
{
    internal class MUser
    {
        private string username;
        private string password;
        private string role;

        public MUser(string username, string password)
        {
            this.username = username;
            this.password = password;
            role = "nill";
        }

        public MUser(string username, string password, string role)
        {
            this.username = username;
            this.password = password;
            this.role = role;
        }

        public string getUsername()
        {
            return username;
        }
        public string getPassword()
        {
            return password;
        }
        public string getRole()
        {
            return role;
        }

        public void setUsername(string username)
        {
            this.username = username;
        }
        public void setPassword(string password)
        {
            this.password = password;
        }
        public void setRole(string role)
        {
            this.role = role;
        }

       
    }
}
