using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace managementHotelierT1.Model
{
    class User
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        /// <summary>
        ///  0: all users
        ///  1: employee
        ///  /// 5: admin
        /// </summary>
        public int accountType { get; set; }

        public User() { }
        public User(string name, string mail, string phone, string username, string pass, int type)
        {
            this.username = username;
            this.password = pass;
            this.accountType = type;
            this.name = name;
            this.email = mail;
            this.phone = phone;
        }
        public User(string name, string mail, string phone, string username, string pass)
        {
            this.username = username;
            this.password = pass;
            this.name = name;
            this.email = mail;
            this.phone = phone;
        }
        /// <summary>
        /// flag==0 => add operation
        /// flag==1 => update operation
        /// </summary>
        /// <param name="u"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static String validUser(User u, int flag)
        {
            String err = "";
            bool OK = true;
            if (u.name.Equals("") && flag==0)
            {
                err += "INVALID name\n";
                OK = false;
            }
            if (u.email.Equals("") && flag == 0)
            {
                err += "INVALID email\n";
                OK = false;
            }
            if ((u.phone.Equals("") || u.phone.Length != 10) && flag == 0)
            {
                err += "INVALID phone\n";
                OK = false;
            }
            /*else
            {
                if(u.phone.Length != 10 && flag == 1)
                {
                    err += "INVALID phone";
                    OK = false;
                }
            }*/
            if (u.username.Equals("") && flag == 0)
            {
                err += "INVALID username\n";
                OK = false;
            }
            if (u.password.Equals("") && flag == 0)
            {
                err += "INVALID password\n";
                OK = false;
            }
            if (OK == true)
            {
                return "OK";
            }
            else
                return err;
        }


    }
}
