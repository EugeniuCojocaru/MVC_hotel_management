using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace managementHotelierT1.Model
{
    class Reservation
    {
        public string client { get; set; }
        public string room { get; set; } 

        public Reservation(string c, string r)//, int d)
        {
            client = c;
            room = r;
            
        }
    }
}
