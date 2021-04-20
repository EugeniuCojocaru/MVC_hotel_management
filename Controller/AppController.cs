using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using managementHotelierT1.View;
using managementHotelierT1.Model;
using Newtonsoft.Json;
using System.IO;

namespace managementHotelierT1.Controller
{
    public class AppController
    {
        private UsersGUI GUI;
        private int connectedUser = 0;
        private string connectedUsername;
        private static int jsonDoc = 0;
        private static int csvDoc = 0;


        public AppController()
        {
            GUI = new UsersGUI();
            permissions(0,false);
            eventManager();
        }

        public UsersGUI getUsersGUI()
        {
            return GUI;
        }

        private void eventManager()
        {   // room show
            GUI.getMenuItemHotels().Click += new EventHandler(menuItemHotels_Click);
            GUI.getListHotels().SelectedIndexChanged += new EventHandler(listHotels_SelectedIndexChange);
            GUI.getListRooms().SelectedIndexChanged += new EventHandler(listRooms_SelectedIndexChange);
            // room filter
            GUI.getMenuItemRoomFilter().Click += new EventHandler(menuItemRoomFilter_Click);
            GUI.getFilterButton().Click += new EventHandler(buttonFilter_Click);
            GUI.getFilterListFilteredRoom().SelectedIndexChanged += new EventHandler(listFilteredRoom_SelectedIndexChange);
            // room add update
            GUI.getMenuItemRoomAddUpdate().Click += new EventHandler(menuItemRoomAddUpdate_Click);
            GUI.getAddUpdateButton().Click += new EventHandler(buttonAddUpdate_Click);
            // delete room
            GUI.getMenuItemRoomDelete().Click += new EventHandler(menuItemRoomDelete_Click);
            GUI.getDeleteButton().Click += new EventHandler(buttonDelete_Click);
            // book room
            GUI.getMenuItemRoomBook().Click += new EventHandler(menuItemRoomBook_Click);
            GUI.getBookButton().Click += new EventHandler(buttonBook_Click);
            // users
            GUI.getMenuItemUsers().Click += new EventHandler(menuItemUsers_Click);
            GUI.getUserListBox().SelectedIndexChanged += new EventHandler(listUsers_SelectedIndexChange);
            GUI.getUserButtonCreate().Click += new EventHandler(buttonUserAdd_Click);
            GUI.getUserButtonUpdate().Click += new EventHandler(buttonUserUpdate_Click);
            GUI.getUserButtonDelete().Click += new EventHandler(buttonUserDelete_Click);
            // register
            GUI.getMenuItemRegister().Click += new EventHandler(menuItemRegister_Click);
            GUI.getRegisterButton().Click += new EventHandler(buttonRegister_Click);
            // login
            GUI.getMenuItemLogin().Click += new EventHandler(menuItemLogin_Click);
            GUI.getLoginButton().Click += new EventHandler(buttonLogin_Click);
            // logout
            GUI.getMenuItemLogout().Click += new EventHandler(menuItemLogout_Click);
            // overview
            GUI.getMenuItemOverview().Click += new EventHandler(menuItemOverview_Click);
            // charts
            GUI.getMenuItemGraphics().Click += new EventHandler(menuItemCharts_Click);
            //reports
            GUI.getMenuItemJSON().Click += new EventHandler(menuItemJSON_Click);
            GUI.getMenuItemCSV().Click += new EventHandler(menuItemCSV_Click);
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ SHOW ALL ROOMS
        private void menuItemHotels_Click(object sender, EventArgs e)
        {   //select tab page
            GUI.getTabControl().SelectedTab = GUI.getTabPageShowRooms();
            // reset
            GUI.getPanelRoomInfo().Visible = false;
            GUI.getButtonRoomDelete().Visible = false;
            GUI.getListRooms().Items.Clear();
            GUI.getBookTextBoxClient().Visible = false;
            GUI.getBookButton().Visible = false;
            GUI.getBookLabel().Visible = false;
            //functionality
            HotelOP hOP = new HotelOP();
            Hotels hotels = hOP.readHotels();
            GUI.getListHotels().Items.Clear();
            GUI.getListHotels().BeginUpdate();
            foreach (Hotel h in hotels.hotelList)
            {
                GUI.getListHotels().Items.Add(h.name);
            }
            GUI.getListHotels().EndUpdate();
        }
        private void listHotels_SelectedIndexChange(object sender, EventArgs e)
        {   //functionality
            if (GUI.getListHotels().SelectedItem != null)
            {
                HotelOP hOP = new HotelOP();
                Hotels hotels = hOP.readHotels();
                GUI.getListRooms().Items.Clear();
                GUI.getListRooms().BeginUpdate();

                foreach (Hotel h in hotels.hotelList)
                {
                    if (h.name.Equals(GUI.getListHotels().SelectedItem.ToString()))
                    {
                        foreach (Room r in h.rooms)
                        {
                            GUI.getListRooms().Items.Add(r.id);
                        }
                    }
                }
                GUI.getListRooms().EndUpdate();
            }
        }
        private void listRooms_SelectedIndexChange(object sender, EventArgs e)
        {
            if (GUI.getListRooms().SelectedItem != null)
            {   //reset
                GUI.getPanelRoomInfo().Visible = true;
                //functionality
                List<string> output = new List<string>();
                RoomOP rOP = new RoomOP();
                Rooms rooms = rOP.readRooms();

                foreach (Room r in rooms.roomList)
                {
                    if (r.id.Equals(GUI.getListRooms().SelectedItem.ToString()))
                    {
                        output = r.getInfo();
                    }
                }

                GUI.getRoomNameLabel().Text = "> Room: " + output[0][1];
                if (output[1].Equals("False"))
                    GUI.getRoomBookedLabel().Text = "> Available: True";
                else
                    GUI.getRoomBookedLabel().Text = "> Available: False";
                GUI.getRoomPriceLabel().Text = "> Price/night: " + output[2] + "$";
                GUI.getRoomPositionLabel().Text = "> Orientation: " + output[3];
                GUI.getRoomFacilitiesLabel().Text = "> Facilities:";

                for (int i = 4; i < output.Count; i++)
                {
                    GUI.getRoomFacilitiesLabel().Text += "\n~ " + output[i];
                }
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ FILTER ROOMS
        private void menuItemRoomFilter_Click(object sender, EventArgs e)
        {   //select tab page
            GUI.getTabControl().SelectedTab = GUI.getTabPageRoomFilter();
            //reset
            GUI.getFilterLabelName().Text = "";
            GUI.getFilterLabelBooked().Text = "";
            GUI.getFilterLabelPrice().Text = "";
            GUI.getFilterLabelPosition().Text = "";
            GUI.getFilterLabelFacilities().Text = "";
            GUI.getFilterCheckBoxN().Checked = false;
            GUI.getFilterCheckBoxS().Checked = false;
            GUI.getFilterCheckBoxE().Checked = false;
            GUI.getFilterCheckBoxW().Checked = false;
            GUI.getFilterCheckBoxBooked().Checked = false;
            GUI.getFilterListFilteredRoom().Items.Clear();
            //functionality
            FacilityOP fOP = new FacilityOP();
            Facilities facilities = fOP.readFacilities();
            GUI.getFilterListFacility().Items.Clear();
            GUI.getFilterListFacility().BeginUpdate();
            foreach (Facility f in facilities.facilityList)
            {
                GUI.getFilterListFacility().Items.Add(f.description);
            }
            GUI.getFilterListFacility().EndUpdate();


            HotelOP hOP = new HotelOP();
            Hotels hotels = hOP.readHotels();
            GUI.getFilterListLocation().Items.Clear();
            GUI.getFilterListLocation().BeginUpdate();
            foreach (Hotel h in hotels.hotelList)
            {
                if (GUI.getFilterListLocation().Items.IndexOf(h.location) == -1)
                {
                    GUI.getFilterListLocation().Items.Add(h.location);
                }
            }
            GUI.getFilterListLocation().EndUpdate();

        }
        private void buttonFilter_Click(object sender, EventArgs ev)
        {   //reset
            GUI.getFilterListFilteredRoom().Items.Clear();
            //functionality            
            double price1 = Double.Parse(GUI.getFilterTextBoxPriceStart().Text);
            double price2 = Double.Parse(GUI.getFilterTextBoxPriceStop().Text);
            bool booked = GUI.getFilterCheckBoxBooked().Checked;
            bool n = GUI.getFilterCheckBoxN().Checked;
            bool s = GUI.getFilterCheckBoxS().Checked;
            bool e = GUI.getFilterCheckBoxE().Checked;
            bool w = GUI.getFilterCheckBoxW().Checked;


            RoomOP rOP = new RoomOP();
            Rooms rooms = rOP.readRooms();

            foreach (Room r in rooms.roomList)
            {
                bool ok = true;
                if (r.price < price1 || r.price > price2) ok = false;
                if (n || s || e || w)
                {
                    bool ok2 = false;
                    if (n && r.position.Equals("N")) ok2 = true;
                    if (s && r.position.Equals("S")) ok2 = true;
                    if (e && r.position.Equals("E")) ok2 = true;
                    if (w && r.position.Equals("W")) ok2 = true;
                    if (ok2 == false)
                        ok = false;
                }

                if (!booked && r.booked) ok = false;

                if (GUI.getFilterListFacility().SelectedItems.Count != 0)
                {
                    for (int i = 0; i < GUI.getFilterListFacility().Items.Count; i++)
                    {
                        if (GUI.getFilterListFacility().GetItemChecked(i))
                            if (!r.haveFacility(GUI.getFilterListFacility().Items[i].ToString()))
                            {
                                ok = false;
                                break;
                            }
                    }

                }

                if (GUI.getFilterListLocation().SelectedItems.Count != 0)
                {
                    HotelOP hOP = new HotelOP();
                    Hotels hotels = hOP.readHotels();
                    string loc = hotels.getLocationOfRoom(r.id);

                    if (loc.Equals(""))
                        ok = false;
                    else
                    {
                        bool ok2 = false;
                        for (int i = 0; i < GUI.getFilterListLocation().Items.Count; i++)
                        {
                            if (GUI.getFilterListLocation().GetItemChecked(i))
                                if (loc.Equals(GUI.getFilterListLocation().Items[i].ToString()))
                                {
                                    ok2 = true;
                                    break;
                                }

                        }
                        if (ok2 == false) ok = false;
                    }
                }

                if (ok == true)
                {
                    GUI.getFilterListFilteredRoom().Items.Add(r.id);
                }
            }
        }
        private void listFilteredRoom_SelectedIndexChange(object sender, EventArgs e)
        {
            if (GUI.getFilterListFilteredRoom().SelectedItem != null)
            {
                //functionality
                List<string> output = new List<string>();
                RoomOP rOP = new RoomOP();
                Rooms rooms = rOP.readRooms();

                foreach (Room r in rooms.roomList)
                {
                    if (r.id.Equals(GUI.getFilterListFilteredRoom().SelectedItem.ToString()))
                    {
                        output = r.getInfo();
                    }
                }
                GUI.getFilterLabelName().Text = "> Room: " + output[0][1];
                if (output[1].Equals("False"))
                    GUI.getFilterLabelBooked().Text = "> Available: True";
                else
                    GUI.getFilterLabelBooked().Text = "> Available: False";
                GUI.getFilterLabelPrice().Text = "> Price/night: " + output[2] + "$";
                GUI.getFilterLabelPosition().Text = "> Orientation: " + output[3];
                GUI.getFilterLabelFacilities().Text = "> Facilities:";

                for (int i = 4; i < output.Count; i++)
                {
                    GUI.getFilterLabelFacilities().Text += "\n~ " + output[i];
                }

            }

        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ADD/UPDATE ROOMS
        private void menuItemRoomAddUpdate_Click(object sender, EventArgs e)
        {   //reset
            GUI.getTabControl().SelectedTab = GUI.getTabPageRoomAddUpdate();
            GUI.getAddUpdateListFacilities().Items.Clear();
            GUI.getAddUpdateCheckBoxHotel().Items.Clear();
            GUI.getAddUpdateCheckBoxHotel().Text = "";
            GUI.getAddUpdateCheckBoxPosition().Items.Clear();
            GUI.getAddUpdateCheckBoxPosition().Text = "";
            GUI.getAddUpdateTextBoxPrice().Text = "";
            GUI.getAddUpdateTextBoxNumber().Text = "";

            //functionality
            FacilityOP fOP = new FacilityOP();
            Facilities facilities = fOP.readFacilities();
            foreach (Facility f in facilities.facilityList)
            {
                GUI.getAddUpdateListFacilities().Items.Add(f.description);
            }
            HotelOP hOP = new HotelOP(); //continua de adaugat itm in combobox 
            Hotels hotels = hOP.readHotels();
            foreach (Hotel h in hotels.hotelList)
            {
                GUI.getAddUpdateCheckBoxHotel().Items.Add(h.name);
            }
            GUI.getAddUpdateCheckBoxPosition().Items.Add("N");
            GUI.getAddUpdateCheckBoxPosition().Items.Add("S");
            GUI.getAddUpdateCheckBoxPosition().Items.Add("E");
            GUI.getAddUpdateCheckBoxPosition().Items.Add("W");
        }
        private void buttonAddUpdate_Click(object sender, EventArgs e)
        {   //functionality
            List<string> newRoomFacilities = new List<string>();
            for (int i = 0; i < GUI.getAddUpdateListFacilities().Items.Count; i++)
            {
                if (GUI.getAddUpdateListFacilities().GetItemChecked(i))
                {
                    newRoomFacilities.Add(GUI.getAddUpdateListFacilities().Items[i].ToString());
                }
            }
            Facilities f = new Facilities(newRoomFacilities);

            string newRoomPosition = GUI.getAddUpdateCheckBoxPosition().SelectedItem.ToString();
            string newRoomHotel = GUI.getAddUpdateCheckBoxHotel().SelectedItem.ToString();
            double newRoomPrice = double.Parse(GUI.getAddUpdateTextBoxPrice().Text.Trim());
            int newRoomNumber = int.Parse(GUI.getAddUpdateTextBoxNumber().Text.Trim());

            HotelOP hOP = new HotelOP();
            Hotels hotels = hOP.readHotels();
            int hotelID = hotels.getIDofHotel(newRoomHotel);

            Room newRoom = new Room(
                hotelID.ToString() + newRoomNumber.ToString(),
                false,
                newRoomPrice,
                newRoomPosition,
                f.facilityList);

            RoomOP rOP = new RoomOP();
            Rooms rooms = rOP.readRooms();

            if (rooms.validRoom(newRoom))
            {
                rooms.roomList.Add(newRoom);
                rOP.saveRooms(rooms);
                Hotel oldHotel = hotels.getHotelByID(hotelID);
                Hotel newHotel = hotels.getHotelByID(hotelID);
                //newHotel = oldHotel;
                newHotel.rooms.Add(newRoom);
                bool ok = hOP.updateHotel(oldHotel, newHotel);
                if (ok)
                    MessageBox.Show("ADD Success");
                else
                    MessageBox.Show("ADD Fail");
            }
            else
            {
                foreach (Room r in rooms.roomList)
                {
                    if (r.id.Equals(newRoom.id))
                    {
                        r.position = newRoom.position;
                        r.price = newRoom.price;
                        r.facilities = newRoom.facilities;
                    }
                }
                bool ok = rOP.saveRooms(rooms);
                if (ok)
                    MessageBox.Show("UPDATE Success");
                else
                    MessageBox.Show("UPDATE Fail");
            }
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ DELETE ROOMS
        private void menuItemRoomDelete_Click(object sender, EventArgs e)
        {   //select tab page
            GUI.getTabControl().SelectedTab = GUI.getTabPageShowRooms();
            // reset
            GUI.getPanelRoomInfo().Visible = false;
            GUI.getButtonRoomDelete().Visible = true;
            GUI.getListRooms().Items.Clear();
            GUI.getBookTextBoxClient().Visible = false;
            GUI.getBookButton().Visible = false;
            GUI.getBookLabel().Visible = false;
            //functionality
            HotelOP hOP = new HotelOP();
            Hotels hotels = hOP.readHotels();
            GUI.getListHotels().Items.Clear();
            GUI.getListHotels().BeginUpdate();
            foreach (Hotel h in hotels.hotelList)
            {
                GUI.getListHotels().Items.Add(h.name);
            }
            GUI.getListHotels().EndUpdate();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            HotelOP hOP = new HotelOP();
            Hotels h = hOP.readHotels();

            int hotelID = h.getIDofHotel(GUI.getListHotels().SelectedItem.ToString());
            Hotel hotel = h.getHotelByID(hotelID);
            foreach (Room r in hotel.rooms)
            {
                if (r.id.Equals(GUI.getListRooms().SelectedItem.ToString()))
                {
                    RoomOP rOP = new RoomOP();
                    rOP.deleteRoom(r);
                    hotel.rooms.Remove(r);
                    break;
                }
            }
            if (hOP.updateHotel(h.getHotelByID(hotelID), hotel))
            {
                MessageBox.Show("Delete success");
                //functionality
                if (GUI.getListHotels().SelectedItem != null)
                {
                    HotelOP hOP2 = new HotelOP();
                    Hotels hotels = hOP2.readHotels();
                    GUI.getListRooms().Items.Clear();
                    GUI.getListRooms().BeginUpdate();

                    foreach (Hotel h2 in hotels.hotelList)
                    {
                        if (h2.name.Equals(GUI.getListHotels().SelectedItem.ToString()))
                        {
                            foreach (Room r in h2.rooms)
                            {
                                GUI.getListRooms().Items.Add(r.id);
                            }
                        }
                    }
                    GUI.getListRooms().EndUpdate();
                }
            }
            else
                MessageBox.Show("Delete fail");

        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ BOOK ROOM
        private void menuItemRoomBook_Click(object sender, EventArgs e)
        {   //select tab page
            GUI.getTabControl().SelectedTab = GUI.getTabPageShowRooms();
            // reset
            GUI.getPanelRoomInfo().Visible = false;
            GUI.getButtonRoomDelete().Visible = false;
            GUI.getListRooms().Items.Clear();
            GUI.getBookTextBoxClient().Text = "";
            GUI.getBookTextBoxClient().Visible = true;
            GUI.getBookButton().Visible = true;
            GUI.getBookLabel().Visible = true;
            //functionality
            HotelOP hOP = new HotelOP();
            Hotels hotels = hOP.readHotels();
            GUI.getListHotels().Items.Clear();
            GUI.getListHotels().BeginUpdate();
            foreach (Hotel h in hotels.hotelList)
            {
                GUI.getListHotels().Items.Add(h.name);
            }
            GUI.getListHotels().EndUpdate();
        }
        private void buttonBook_Click(object sender, EventArgs e)
        {
            if (GUI.getBookTextBoxClient().Text != null)
            {
                RoomOP rOP = new RoomOP();
                Rooms r = rOP.readRooms();

                if (r.availiableRoom(GUI.getListRooms().SelectedItem.ToString()))
                {
                    ReservationOP reOP = new ReservationOP();
                    Reservations rs = reOP.readReservations();
                    Reservation re = new Reservation(
                        GUI.getBookTextBoxClient().Text,
                        GUI.getListRooms().SelectedItem.ToString()
                        );
                    rs.reservationList.Add(re);
                    reOP.saveReservations(rs);
                    rOP.saveRooms(r);
                    GUI.getBookTextBoxClient().Text = "";
                    MessageBox.Show("Book success");

                }
                else
                    MessageBox.Show("Book fail");

            }
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ USER
        private void refreshListUsers()
        {
            GUI.getUserListBox().Items.Clear();
            UserOP uOP = new UserOP();
            Users us = uOP.readUsers();
            foreach (User u in us.userList)
            {
                if (u.accountType < connectedUser || u.username.Equals(connectedUsername))
                {
                    GUI.getUserListBox().Items.Add(u.username);
                }
            }
        }
        private void menuItemUsers_Click(object sender, EventArgs e)
        {   //select tab page
            GUI.getTabControl().SelectedTab = GUI.getTabPageUsers();
            // reset
            GUI.getUserListBox().Items.Clear();
            GUI.getUserComboBoxAccountType().Items.Clear();
            GUI.getUserTextBoxName().Text = "";
            GUI.getUserTextBoxEmail().Text = "";
            GUI.getUserTextBoxPhone().Text = "";
            GUI.getUserTextBoxUsername().Text = "";
            GUI.getUserTextBoxPassword().Text = "";
            GUI.getUserComboBoxAccountType().Text = "";
            //functionality
            UserOP uOP = new UserOP();
            Users us = uOP.readUsers();

            foreach (User u in us.userList)
            {
                if (u.accountType < connectedUser || u.username.Equals(connectedUsername))
                {
                    GUI.getUserListBox().Items.Add(u.username);
                }
            }
            switch (connectedUser)
            {
                case 1:
                    GUI.getUserComboBoxAccountType().Items.Add("user");
                    // GUI.getUserComboBoxAccountType().Items.Add("employee");
                    break;
                case 5:
                    GUI.getUserComboBoxAccountType().Items.Add("user");
                    GUI.getUserComboBoxAccountType().Items.Add("employee");
                    // GUI.getUserComboBoxAccountType().Items.Add("admin");
                    break;
                case 6:
                    GUI.getUserComboBoxAccountType().Items.Add("user");
                    GUI.getUserComboBoxAccountType().Items.Add("employee");
                    GUI.getUserComboBoxAccountType().Items.Add("admin");
                    break;
                default: MessageBox.Show("You fucked up!"); break;
            }

        }
        private void listUsers_SelectedIndexChange(object sender, EventArgs e)
        {
            if (GUI.getUserListBox().SelectedItem != null)
            {
                UserOP uOP = new UserOP();
                Users us = uOP.readUsers();

                foreach (User u in us.userList)
                {
                    if (u.accountType <= connectedUser)
                    {
                        //MessageBox.Show("u here!");
                        if (u.username.Equals(GUI.getUserListBox().SelectedItem.ToString()))
                        {
                            GUI.getUserTextBoxName().Text = u.name;
                            GUI.getUserTextBoxEmail().Text = u.email;
                            GUI.getUserTextBoxPhone().Text = u.phone;
                            GUI.getUserTextBoxUsername().Text = u.username;
                            GUI.getUserTextBoxPassword().Text = u.password;
                            switch (u.accountType)
                            {
                                case 0: GUI.getUserComboBoxAccountType().Text = "user"; break;
                                case 1: GUI.getUserComboBoxAccountType().Text = "employee"; break;
                                case 5: GUI.getUserComboBoxAccountType().Text = "admin"; break;
                                case 6: GUI.getUserComboBoxAccountType().Text = "god"; break;
                                default: MessageBox.Show("You fucked up!"); break;
                            }
                            break;
                        }
                    }
                }
            }
        }
        private void buttonUserAdd_Click(object sender, EventArgs e)
        {
            if (GUI.getUserListBox().SelectedItem != null)
            {
                User u = new User(
                GUI.getUserTextBoxName().Text,
                GUI.getUserTextBoxEmail().Text,
                GUI.getUserTextBoxPhone().Text,
                GUI.getUserTextBoxUsername().Text,
                GUI.getUserTextBoxPassword().Text
                );
                string err = User.validUser(u, 0);
                if (!err.Equals("OK"))
                {
                    MessageBox.Show(err);
                }
                else
                {
                    UserOP uOP = new UserOP();
                    Users us = uOP.readUsers();
                    string username = GUI.getUserTextBoxUsername().Text;
                    bool valid = us.validUsername(username);
                    if (valid)
                    {
                        int account = 0;
                        switch (GUI.getUserComboBoxAccountType().Text)
                        {
                            case "user": account = 0; break;
                            case "employee": account = 1; break;
                            case "admin": account = 5; break;
                            case "god": account = 6; break;
                            default: MessageBox.Show("You fucked up!"); break;
                        }
                        u.accountType = account;
                        us.userList.Add(u);
                        uOP.saveUsers(us);
                        refreshListUsers();
                        MessageBox.Show("VALID account creation");
                    }
                    else
                    {
                        MessageBox.Show("INVALID: already used username");
                    }
                }
            }
        }
        private void buttonUserUpdate_Click(object sender, EventArgs e)
        {
            if (GUI.getUserListBox().SelectedItem != null)
            {
                User u = new User(
                    GUI.getUserTextBoxName().Text,
                    GUI.getUserTextBoxEmail().Text,
                    GUI.getUserTextBoxPhone().Text,
                    GUI.getUserTextBoxUsername().Text,
                    GUI.getUserTextBoxPassword().Text
                    );
                string err = User.validUser(u, 1);
                if (!err.Equals("OK"))
                {
                    MessageBox.Show(err);
                }
                else
                {
                    UserOP uOP = new UserOP();
                    Users us = uOP.readUsers();
                    string username = GUI.getUserTextBoxUsername().Text;
                    if (username.Equals(GUI.getUserListBox().SelectedItem.ToString()))
                    {
                        int account = 0;
                        switch (GUI.getUserComboBoxAccountType().Text)
                        {
                            case "user": account = 0; break;
                            case "employee": account = 1; break;
                            case "admin": account = 5; break;
                            case "god": account = 6; break;
                            default: MessageBox.Show("You fucked up!"); break;
                        }
                        if (account < connectedUser)
                        {
                            u.accountType = account;

                            us.updateUser(u);
                            uOP.saveUsers(us);
                            refreshListUsers();
                            MessageBox.Show("VALID account update");
                        }
                        else
                            MessageBox.Show("ERROR: no permission to create accounts with a greater accunt type than yours");
                    }
                    else
                    {
                        MessageBox.Show("INVALID: you can't change the username");
                    }

                }
            }
        }
        private void buttonUserDelete_Click(object sender, EventArgs e)
        {
            if (GUI.getUserListBox().SelectedItem != null)
            {
                if (GUI.getUserTextBoxUsername().Text.Equals("GOD"))
                {
                    MessageBox.Show("I'M NOT INTO SUICIDE");
                }
                else
                {
                    if (GUI.getUserTextBoxUsername().Text.Equals(connectedUsername))
                    {
                        MessageBox.Show("You can't delete you own account, coconut brain...");
                    }
                    else
                    {
                        UserOP uOP = new UserOP();
                        Users us = uOP.readUsers();
                        bool ok = false;

                        foreach (User i in us.userList)
                        {
                            if (i.username.Equals(GUI.getUserTextBoxUsername().Text))
                            {
                                ok = true;
                                us.userList.Remove(i);
                                break;

                            }
                        }
                        if (ok == false)
                        {
                            MessageBox.Show("INVALID account delete");
                        }
                        else
                        {
                            MessageBox.Show("VALID account delete");
                            uOP.saveUsers(us);
                            refreshListUsers();
                        }
                    }
                }

            }
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ REGISTER
        private void menuItemRegister_Click(object sender, EventArgs e)
        {   //select tab page
            GUI.getTabControl().SelectedTab = GUI.getTabPageRegister();
            // reset
            GUI.getRegName().Text = "";
            GUI.getRegEmail().Text = "";
            GUI.getRegPhone().Text = "";
            GUI.getRegUsername().Text = "";
            GUI.getRegPassword().Text = "";
            GUI.getRegRePass().Text = "";

        }
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            User u = new User(
               GUI.getRegName().Text,
               GUI.getRegEmail().Text,
               GUI.getRegPhone().Text,
               GUI.getRegUsername().Text,
               GUI.getRegPassword().Text
               );
            string err = User.validUser(u, 0);
            if (!err.Equals("OK"))
            {
                MessageBox.Show(err);
            }
            else
            {
                if (!u.password.Equals(GUI.getRegRePass().Text))
                {
                    MessageBox.Show("ERROR: password don't match");
                }
                else
                {
                    UserOP uOP = new UserOP();
                    Users us = uOP.readUsers();
                    string username = GUI.getRegUsername().Text;
                    bool valid = us.validUsername(username);
                    if (valid)
                    {                        
                        u.accountType = 0;
                        us.userList.Add(u);
                        uOP.saveUsers(us);
                        //refreshListUsers();
                        GUI.getTabControl().SelectedTab = GUI.getTabPageLogin();
                        MessageBox.Show("VALID account creation");
                    }
                    else
                    {
                        MessageBox.Show("ERROR: already used username");
                    }
                }
            }
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ LOGIN
        private void menuItemLogin_Click(object sender, EventArgs e)
        {   //select tab page
            GUI.getTabControl().SelectedTab = GUI.getTabPageLogin();
            // reset         
            GUI.getInUsername().Text = "";
            GUI.getInPassword().Text = "";
        }
        private void permissions(int accountType, bool login)
        {
            if (accountType == 0)
            {
                GUI.getMenuItemRoomBook().Visible = false;
                GUI.getMenuItemRoomAddUpdate().Visible = false;
                GUI.getMenuItemRoomDelete().Visible = false;
                GUI.getMenuItemUsers().Visible = false;
                GUI.getMenuItemReports().Visible = false;
                if (login)
                {
                    GUI.getMenuItemRegister().Visible = false;
                    GUI.getMenuItemLogin().Visible = false;
                    GUI.getMenuItemLogout().Visible = true;
                    GUI.getTabControl().SelectedTab = GUI.getTabPageOverview();  
                    
                }
                else
                {
                    GUI.getMenuItemRegister().Visible = true;
                    GUI.getMenuItemLogin().Visible = true;
                    GUI.getMenuItemLogout().Visible = false;
                    GUI.Text = "Welcome!";
                    GUI.getTabControl().SelectedTab = GUI.getTabPageOverview();

                }
            }
            else
            {
                GUI.getMenuItemRegister().Visible = false;
                GUI.getMenuItemLogin().Visible = false;
                GUI.getMenuItemLogout().Visible = true;
                GUI.getMenuItemReports().Visible = true;
                GUI.getMenuItemRoomBook().Visible = true;
                GUI.getMenuItemRoomAddUpdate().Visible = true;
                GUI.getMenuItemRoomDelete().Visible = true;
                GUI.getMenuItemUsers().Visible = true;
                GUI.getTabControl().SelectedTab = GUI.getTabPageOverview();
            }           
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            UserOP uOP = new UserOP();
            Users users = uOP.readUsers();

            int accountType = users.validLoginInfo(GUI.getInUsername().Text, GUI.getInPassword().Text);
            switch (accountType)
            {
                case -2:
                    MessageBox.Show("INVALID login info");
                    break;               
                case 0://USER
                    GUI.Text = "WELCOME, " + GUI.getInUsername().Text + "!";
                    connectedUser = 0;
                    connectedUsername = GUI.getInUsername().Text;
                    permissions(connectedUser,true);
                    break;
                case 1://EMPLOYEE
                    GUI.Text = "WELCOME, " + GUI.getInUsername().Text + "!";
                    connectedUser = 1;
                    connectedUsername = GUI.getInUsername().Text;
                    permissions(connectedUser, true);
                    break;
                case 5://ADMIN
                    GUI.Text = "WELCOME, " + GUI.getInUsername().Text + "!";
                    connectedUser = 5;
                    connectedUsername = GUI.getInUsername().Text;
                    permissions(connectedUser, true);
                    break;
                case 6://GOD
                    GUI.Text = "GLAD TO SEE YOU AGAIN " + GUI.getInUsername().Text + "-SAMA!";
                    connectedUser = 6;
                    connectedUsername = GUI.getInUsername().Text;
                    permissions(connectedUser, true);
                    break;

                default:
                    MessageBox.Show("YOU FUCKED UP");
                    break;
            }
            
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ LOGOUT
        private void menuItemLogout_Click(object sender, EventArgs e)
        {
            permissions(0, false);
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ OVERVIEW
        private void menuItemOverview_Click(object sender, EventArgs e)
        {
            GUI.getTabControl().SelectedTab = GUI.getTabPageOverview();
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ CHARTS
        private void menuItemCharts_Click(object sender, EventArgs e)
        {   // tab select
            GUI.getTabControl().SelectedTab = GUI.getTabPageCharts();
            // functionality
            RoomOP rOP = new RoomOP();
            Rooms rs = rOP.readRooms();
            int booked = rs.getNumberRoomsBooked();
            int nbooked = rs.roomList.Count - booked;
            GUI.getChart1().Series["Booked"].Points.AddXY("Booked",booked);
            GUI.getChart1().Series["Booked"].Points.AddXY("Not Booked",nbooked);
            GUI.getChart2().Series["Booked"].Points.AddXY("Booked", booked);
            GUI.getChart2().Series["Booked"].Points.AddXY("Not Booked", nbooked);

            foreach(Room r in rs.roomList)
            {
                GUI.getChart4().Series["Facilities"].Points.AddXY(r.id, r.facilities.Count);
            }
            foreach (Room r in rs.roomList)
            {
                GUI.getChart3().Series["Price"].Points.AddXY(r.id, r.price);
            }
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ REPORTS
        private void menuItemJSON_Click(object sender, EventArgs e)
        {
            ReservationOP rOP = new ReservationOP();
            Reservations rs = rOP.readReservations();

            string JSONresult = JsonConvert.SerializeObject(rs);
            string path = "jsonReport" +(jsonDoc++) + ".json";
            if (File.Exists(path))
            {
                File.Delete(path);
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(JSONresult.ToString());
                    tw.Close();
                }
            }
            else
            {
                if (!File.Exists(path))
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine(JSONresult.ToString());
                        tw.Close();
                    }
                }
            }
            MessageBox.Show("SUCCESS json");
        }
        private void menuItemCSV_Click(object sender, EventArgs e)
        {
            ReservationOP rOP = new ReservationOP();
            Reservations rs = rOP.readReservations();

            var csv = new StringBuilder();
            csv.AppendLine("client,room,days");
            foreach (Reservation r in rs.reservationList)
            {
                var first = r.client;
                var second = r.room;               

                var newLine = string.Format("{0},{1}", first, second);
                csv.AppendLine(newLine);
            }


            File.WriteAllText("csvReport" + (csvDoc++) + ".csv", csv.ToString());
            MessageBox.Show("SUCCESS csv");
        }
    }
}
