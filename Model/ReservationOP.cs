using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace managementHotelierT1.Model
{
    class ReservationOP
    {
        public string file { get; set; }

        public ReservationOP() { 
            file= "Reservations.xml";
        }

        public ReservationOP(string file)
        {
            this.file = file;
        }

        public bool saveReservations(Reservations users)
        {
            try
            {
                XmlWriter xmlWriter = XmlWriter.Create(this.file);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("reservations");
                xmlWriter.WriteString("\n");
                foreach (Reservation r  in users.reservationList)
                {
                    xmlWriter.WriteStartElement("reservation");
                    xmlWriter.WriteAttributeString("client", r.client);
                    xmlWriter.WriteAttributeString("room", r.room);
                   // xmlWriter.WriteAttributeString("days", r.days.ToString());                    
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
        public bool deleteReservation(Reservation user)
        {
            Reservations uDelete = readReservations();
            bool ok = false;
            foreach (Reservation u in uDelete.reservationList)
            {
                if (u.client == user.client && u.room==user.room)// && u.days==user.days)
                {
                    uDelete.reservationList.Remove(u);
                    ok = true;
                    break;
                }
            }

            if (ok == true)
                _ = saveReservations(uDelete);

            return ok;
        }
       
        public Reservations readReservations()
        {
            Reservations u = new Reservations();
            XmlReader reader = XmlReader.Create(this.file);

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "reservation"))
                {
                    Reservation readUser = new Reservation(
                        reader.GetAttribute("client"),
                        reader.GetAttribute("room")//,                       
                        //int.Parse(reader.GetAttribute("days"))
                        );
                    u.reservationList.Add(readUser);
                }
            }
            reader.Close();

            return u;
        }
    }
}
