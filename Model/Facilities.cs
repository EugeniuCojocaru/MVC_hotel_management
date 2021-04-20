using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace managementHotelierT1.Model
{
    class Facilities
    {
        public List<Facility> facilityList { get; set; }

        public Facilities()
        {
            this.facilityList = new List<Facility>();
        }
        public Facilities(List<Facility> facilities)
        {
            this.facilityList = facilities;
        }
        public Facilities(List<string> facilities)
        {
            facilityList = new List<Facility>();
            foreach (string s in facilities)
            {
                facilityList.Add(new Facility(s));
            }            
        }
        public void myString()
        {
            foreach(Facility f in this.facilityList)
            {
                Console.WriteLine(f.description);
            }
        }
    }
}
