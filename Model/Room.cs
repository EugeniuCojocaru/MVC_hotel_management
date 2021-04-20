using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace managementHotelierT1.Model
{
    class Room
    {
        //cate camere sunt inchiriate/cate sunt libere
        public string id { get; set; }
        public bool booked { get; set; }
        public double price { get; set; }
        public string position { get; set; }
        public List<Facility> facilities { get; set; }     

        public Room()
        {
            this.facilities = new List<Facility>();
        }

        public Room(string id, bool booked, double price, string position, List<Facility> facilities)
        {
            this.id = id;
            this.booked = booked;
            this.price = price;
            this.position = position;
            this.facilities = facilities;
           
        }

        public void setFacilities(string input)
        {
            string[] subs = input.Split(',');

            foreach (string sub in subs)
            {
                if(!sub.Equals(""))
                    this.facilities.Add(new Facility(sub));
            }
        }
        
        public List<string> getInfo()
        {
            List<string> l = new List<string>();
            l.Add(id);
            l.Add(booked.ToString());
            l.Add(price.ToString());
            l.Add(position);
            foreach(Facility f in facilities)
            {
                l.Add(f.description);
            }
            return l;
        }

        public bool haveFacility(string facility)
        {
            foreach(Facility f in facilities)
            {
                if (f.description.Equals(facility))
                    return true;
            }
            return false;
        }


    }
}
