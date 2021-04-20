using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace managementHotelierT1.Model
{
    class Reservations
    {
        public List<Reservation> reservationList { get; set; }

        public Reservations()
        {
            reservationList = new List<Reservation>();
        }
        public Reservations(List<Reservation> users)
        {
            this.reservationList = users;
        }
    }
}
