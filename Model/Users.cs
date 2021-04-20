using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace managementHotelierT1.Model
{
    class Users
    {
        public List<User> userList { get; set; }

        public Users()
        {
            userList = new List<User>();
        }
        public Users(List<User> users)
        {
            this.userList = users;
        }

        public bool validUsername(string username)
        {
            bool ok = true;
            foreach(User u in userList)
            {
                if(u.username.Equals(username))
                {
                    ok = false;
                    break;
                }
            }
            return ok;
        }
        public int validLoginInfo(string username, string pass)
        {
            int type = -2;
            foreach (User u in userList)
            {
                if (u.username.Equals(username) && u.password.Equals(pass))
                {
                    type = u.accountType;
                    break;
                }
            }

            return type;
        }
        public void updateUser(User uu)
        {
            foreach (User u in userList)
            {
                if (u.username.Equals(uu.username))
                {
                    if (!uu.name.Equals(""))
                    {
                        u.name = uu.name;
                    }
                    if (!uu.email.Equals(""))
                    {
                        u.email = uu.email;
                    }
                    if (!uu.phone.Equals(""))
                    {
                        u.phone = uu.phone;
                    }
                    if (!uu.password.Equals(""))
                    {
                        u.password = uu.password;
                    }
                    u.accountType = uu.accountType;

                }
            }
        }
    }
}
