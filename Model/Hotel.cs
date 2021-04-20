using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace managementHotelierT1.Model
{
    class Hotel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public List<Room> rooms { get; set; }
        public List<Facility> facilities { get; set; }

        public Hotel() 
        {
            rooms = new List<Room>();
            facilities = new List<Facility>();
        }
        public Hotel(int id, string name, string location, List<Room> rooms, List<Facility> facilities)
        {
            this.id = id;
            this.name = name;           
            this.location = location;
            this.rooms = rooms;
            this.facilities = facilities;
        }
        public void setFacilities(string input)
        {
            string[] subs = input.Split(',');

            foreach (string sub in subs)
            {
                if (!sub.Equals(""))
                    this.facilities.Add(new Facility(sub));
            }
        }
        public void setRooms(string input)
        {
            string[] subs = input.Split(',');

            foreach (string sub in subs)
            {
                if (!sub.Equals(""))
                {
                    Room r = new Room();
                    r.id = sub;
                    this.rooms.Add(r);
                }
                    
            }
        }

        public void deleteRoom(string id)
        {
            
        }
       
        public void myString()
        {
            Console.WriteLine(">> ID: "+this.id+"\nName: " + this.name+ "\nLocation: "+ this.location);
            Console.WriteLine("Facilities: ");
            foreach (Facility f in this.facilities)
            {
                Console.WriteLine("- " + f.description);
            }
            Console.WriteLine("Rooms: ");
            foreach (Room r in this.rooms)
            {
                Console.WriteLine("- " + r.id);
            }
        }


    }
     

}
