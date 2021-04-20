using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace managementHotelierT1.Model
{
    class FacilityOP
    {
        public string file { get; set; } 
        
        public FacilityOP()
        {
            file = "Facility.xml";
        }

        public FacilityOP(string file)
        {
            this.file = file;
        }

        public bool saveFacilities(Facilities facilities)
        {
            try
            {
                XmlWriter xmlWriter = XmlWriter.Create(this.file);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("facilities");
                xmlWriter.WriteString("\n");
                foreach (Facility f in facilities.facilityList)
                {
                    xmlWriter.WriteStartElement("facility");
                    xmlWriter.WriteAttributeString("description", f.description);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteString("\n");
                }

                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool deleteFacility(Facility facility)
        {
            Facilities fdelete = readFacilities();
            bool ok = false;
            foreach (Facility f in fdelete.facilityList)
            {
                if (f.description == facility.description)
                {
                    fdelete.facilityList.Remove(f);
                    ok = true;
                    break;
                }
            }
            if(ok==true)
             _ = saveFacilities(fdelete);
            return ok;
        }
        public bool updateFacility(Facility oldFacility, Facility newFacility)
        {
            Facilities fUpdate = readFacilities();
            bool ok = false;
            foreach (Facility f in fUpdate.facilityList)
            {
                if (f.description == oldFacility.description)
                {
                    f.description = newFacility.description;
                    ok = true;
                    break;
                }
            }

            if (ok == true)
                _ = saveFacilities(fUpdate);            
            return ok;
        }
        public Facilities readFacilities()
        {
            Facilities f = new Facilities();
            XmlReader reader = XmlReader.Create(this.file);

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "facility"))
                {
                    //Console.WriteLine(reader.GetAttribute("description"));
                    f.facilityList.Add(new Facility(reader.GetAttribute("description")));
                }
            }
            reader.Close();
            return f;
        }
    }
}
