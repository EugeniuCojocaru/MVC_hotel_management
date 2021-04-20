using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace managementHotelierT1.Model
{
    class UserOP
    {
        public string file { get; set; }       

        public UserOP() {
        file= "Users.xml";
        }

        public UserOP(string file)
        {
            this.file = file;
        }

        public bool saveUsers(Users users)
        {
            try
            {
                XmlWriter xmlWriter = XmlWriter.Create(this.file);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("users");
                xmlWriter.WriteString("\n");
                foreach (User u in users.userList)
                {
                    xmlWriter.WriteStartElement("user");
                    xmlWriter.WriteAttributeString("name",u.name );
                    xmlWriter.WriteAttributeString("email", u.email);
                    xmlWriter.WriteAttributeString("phone", u.phone);
                    xmlWriter.WriteAttributeString("username", u.username);
                    xmlWriter.WriteAttributeString("password", u.password);
                    xmlWriter.WriteAttributeString("accountType", u.accountType.ToString());                           
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
        public bool deleteUser(User user)
        {
            Users uDelete = readUsers();
            bool ok = false;
            foreach (User u in uDelete.userList)
            {
                if (u.username == user.username)
                {
                    uDelete.userList.Remove(u);
                    ok = true;
                    break;
                }
            }

            if (ok == true)
                _ = saveUsers(uDelete);

            return ok;
        }
        public bool updateUser(User oldUser, User newUser)
        {
            Users uDelete = readUsers();
            bool ok = false;
            foreach (User u in uDelete.userList)
            {
                if (u.username == oldUser.username)
                {

                    u.name = newUser.name;
                    u.email = newUser.email;
                    u.phone = newUser.phone;
                    u.username = newUser.username;
                    u.password = newUser.password;
                    ok = true;
                    break;
                }
            }

            if (ok == true)
                _ = saveUsers(uDelete);

            return ok;
        }
        public Users readUsers()
        {
           Users u = new Users();
            XmlReader reader = XmlReader.Create(this.file);

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "user"))
                {
                    User readUser = new User(
                        reader.GetAttribute("name"),
                        reader.GetAttribute("email"),
                        reader.GetAttribute("phone"),
                        reader.GetAttribute("username"),
                        reader.GetAttribute("password"),
                        int.Parse(reader.GetAttribute("accountType"))                        
                        );
                    u.userList.Add(readUser);
                }
            }
            reader.Close();
           
            return u;
        }
    }
}
