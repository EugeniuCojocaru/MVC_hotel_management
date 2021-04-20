using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace managementHotelierT1.Model
{
    class RoomOP
    {
        public string file { get; set; }
        public RoomOP() { 
            file= "Rooms.xml";
        }

        public RoomOP(string file)
        {
            this.file = file;
        }

        public bool saveRooms(Rooms rooms)
        {
            try
            {
                XmlWriter xmlWriter = XmlWriter.Create(this.file);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("rooms");
                xmlWriter.WriteString("\n");
                foreach(Room r in rooms.roomList)
                {
                    xmlWriter.WriteStartElement("room");
                    xmlWriter.WriteAttributeString("id", r.id);
                    xmlWriter.WriteAttributeString("booked", r.booked.ToString());
                    xmlWriter.WriteAttributeString("price", r.price.ToString());
                    xmlWriter.WriteAttributeString("position", r.position);
                    string s="";
                    foreach (Facility f in r.facilities)
                    {
                        s += f.description + ",";
                    }
                    xmlWriter.WriteAttributeString("facilities", s);
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
        public bool deleteRoom(Room room)
        {
            Rooms rdelete = readRooms();
            bool ok = false;
            foreach (Room r in rdelete.roomList)
            {
                if (r.id == room.id)
                {
                    rdelete.roomList.Remove(r);
                    ok = true;
                    break;
                }
            }

            if (ok == true)
                _ = saveRooms(rdelete);
           
            return ok;
        }
        public bool updateRoom(Room oldRoom, Room newRoom)
        {
            Rooms rUpdate = readRooms();
            bool ok = false;
            foreach (Room r in rUpdate.roomList)
            {
                if (r.id == oldRoom.id)
                {
                    r.id = newRoom.id;
                    r.booked = newRoom.booked;
                    r.price = newRoom.price;
                    r.position = newRoom.position;
                    r.facilities = newRoom.facilities;
                    ok = true;
                    break;
                }
            }

            if (ok == true)
                _ = saveRooms(rUpdate);
            return ok;
        }
        public Rooms readRooms()
        {
            Rooms r = new Rooms();
            XmlReader reader = XmlReader.Create(this.file);

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "room"))
                {
                    Room rr = new Room();
                    rr.id = reader.GetAttribute("id");
                    //Console.WriteLine(">"+rr.id);
                    if (reader.GetAttribute("booked") == "True")
                        rr.booked = true;
                    else
                        rr.booked = false;
                    rr.price = double.Parse(reader.GetAttribute("price"));
                    rr.position = reader.GetAttribute("position");
                    rr.setFacilities(reader.GetAttribute("facilities"));                
                    r.roomList.Add(rr);
                }
            }
            reader.Close();
            return r;
        }
    }
}
