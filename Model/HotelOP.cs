using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace managementHotelierT1.Model
{
    class HotelOP
    {
        public string file { get; set; }

        public HotelOP(){
            file = "Hotels.xml";
        }

        public HotelOP(string file)
        {
            this.file = file;
        }

        public bool saveHotels(Hotels hotels)
        {
            try
            {
                XmlWriter xmlWriter = XmlWriter.Create(this.file);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("hotels");
                xmlWriter.WriteString("\n");
                foreach (Hotel h in hotels.hotelList)
                {
                    xmlWriter.WriteStartElement("hotel");
                    xmlWriter.WriteAttributeString("id",h.id.ToString());
                    xmlWriter.WriteAttributeString("name",h.name);
                    xmlWriter.WriteAttributeString("location",h.location);
                    string sf = "";
                    foreach (Facility f in h.facilities)
                    {
                        sf += f.description + ",";
                    }
                    string sr = "";
                    foreach (Room r in h.rooms)
                    {
                        sr += r.id + ",";
                    }
                    xmlWriter.WriteAttributeString("facilities", sf);
                    xmlWriter.WriteAttributeString("rooms", sr);
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
        public bool deleteHotel(Hotel hotel)
        {
            Hotels hdelete = readHotels();
            bool ok = false;
            foreach (Hotel h in hdelete.hotelList)
            {
                if (h.id == hotel.id)
                {
                    hdelete.hotelList.Remove(h);
                    ok = true;
                    break;
                }
            }

            if (ok == true)
                _ = saveHotels(hdelete);

            return ok;
        }
        public bool updateHotel(Hotel oldHotel, Hotel newHotel)
        {
            Hotels hUpdate = readHotels();
            bool ok = false;
            foreach (Hotel h in hUpdate.hotelList)
            {
                if (h.id == oldHotel.id)
                {
                    h.id = newHotel.id;
                    h.name = newHotel.name;
                    h.location = newHotel.location;
                    h.rooms = newHotel.rooms;
                    h.facilities = newHotel.facilities;
                    ok = true;
                    break;
                }
            }

            if (ok == true)
                _ = saveHotels(hUpdate);

            return ok;            
        }
        public Hotels readHotels()
        {
            Hotels h = new Hotels();
            XmlReader reader = XmlReader.Create(this.file);

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "hotel"))
                {
                    Hotel readHotel = new Hotel();
                    readHotel.id = int.Parse(reader.GetAttribute("id"));                    
                    readHotel.name = reader.GetAttribute("name");
                    readHotel.location = reader.GetAttribute("location");
                    readHotel.setRooms(reader.GetAttribute("rooms"));
                    readHotel.setFacilities(reader.GetAttribute("facilities"));
                    h.hotelList.Add(readHotel);
                
                }
            }
            reader.Close();
            return h;
        }
    }
}
