using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace managementHotelierT1.Model
{
    class Rooms
    {
        public List<Room> roomList { get; set; }

        public Rooms()
        {
            this.roomList = new List<Room>();
        }
        public Rooms(List<Room> users)
        {
            this.roomList = users;
        }
        public bool validRoom(Room rr)
        {
            bool ok = true;
            foreach(Room r in roomList)
            {
                if (r.id.Equals(rr.id)){
                    ok = false;
                    break;
                }
            }
            return ok;
        }
        public bool availiableRoom(string rid)
        {
            bool ok = true;
            foreach(Room r in roomList)
            {
                if (r.id.Equals(rid))
                {
                    if (r.booked == true)
                    {
                        ok = false;
                    }                        
                    else
                    {
                        if (r.booked == false)
                            r.booked = true;
                    }
                    break;
                }

            }
            return ok;

        }
       
        public void myString()
        {
            foreach (Room r in this.roomList)
            {

                Console.WriteLine(">> ID: "+ r.id);
                Console.WriteLine("Booked: "+ r.booked);
                Console.WriteLine("Price: "+ r.price.ToString());
                Console.WriteLine("Position: "+ r.position);
                Console.WriteLine("Facilities: ");
                Facilities f =new Facilities(r.facilities);
                f.myString();
            }
        }

        public int getNumberRoomsBooked()
        {
            int n = 0;
            foreach (Room r in roomList)
            {
                if (r.booked==true)
                {
                    n++;                    
                }
            }
            return n;
        }

    }
}
