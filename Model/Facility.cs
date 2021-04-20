using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace managementHotelierT1.Model
{
    
    class Facility
    {
        public string description { get; set; }      

        public Facility()
        {
            this.description = "facility";
        }

        public Facility( string name)
        {
            this.description = name;
        }


    }
}
