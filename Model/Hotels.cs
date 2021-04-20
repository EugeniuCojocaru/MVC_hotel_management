using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace managementHotelierT1.Model
{
    class Hotels
    {
        public List<Hotel> hotelList { get; set; }

        public Hotels()
        {
            hotelList = new List<Hotel>();
        }
        public Hotels(List<Hotel> hotels)
        {
            this.hotelList = hotels;
        }
        public string getLocationOfRoom(string id)
        {
            string loc = "";
            foreach (Hotel h in hotelList)
            {
                if (h.id == int.Parse(id[0].ToString()))
                {
                    loc = h.location;
                    break;
                }
            }

            return loc;
        }
        /// <summary>
        /// gives the id of a hotel
        /// </summary>
        /// <param name="hotel">name of the hotel</param>
        /// <returns></returns>
        public int getIDofHotel(string hotel)
        {
            int id = -1;
            foreach(Hotel h in hotelList)
            {
                if (h.name.Equals(hotel))
                {
                    id = h.id;
                    break;
                }
            }
            return id;
        }
        /// <summary>
        /// returns the Hotel item with a specific id
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns>the hotel if it exists, else null</returns>
        public Hotel getHotelByID(int i)
        {
            bool ok = false;
            Hotel hot = new Hotel();
            foreach (Hotel h in hotelList)
            {
                if (h.id==i)
                {
                    ok = true;
                    hot = h;
                    break;
                }
            }
            if (ok)
                return hot;
            else
                return null;
        }
        public void myString()
        {
            foreach (Hotel h in this.hotelList)
            {
                h.myString();
            }
        }
    }

}
