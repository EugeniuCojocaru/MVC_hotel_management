using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using managementHotelierT1.Model;
using managementHotelierT1.Controller;


namespace managementHotelierT1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppController ac = new AppController();
            Application.Run(ac.getUsersGUI());
           /* UserOP uOP = new UserOP("Users.xml");
            Users u = new Users();

            User us = new User("admin", "admin", "admin", "admin", "admin", -1);
            u.userList.Add(us);
            uOP.saveUsers(u);*/

            


        }
    }

}
