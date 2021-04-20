using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace managementHotelierT1.View
{
    public partial class UsersGUI : Form, IUsersGUI
    {    
        public UsersGUI()
        {
            InitializeComponent();   
        }        
        
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ TAB CONTROLER
        public TabControl getTabControl()
        {
            return tabControl1;
        }
        public TabPage getTabPageShowRooms()
        {
            return pageShowRoom;
        }
        public TabPage getTabPageRoomFilter()
        {
            return pageRoomFilter;
        }
        public TabPage getTabPageRoomAddUpdate()
        {
            return pageRoomAddUpdate;
        }
        public TabPage getTabPageUsers()
        {
            return tabPage2;
        }
        public TabPage getTabPageRegister()
        {
            return registerPage;
        }
        public TabPage getTabPageLogin()
        {
            return loginPage;
        }
        public TabPage getTabPageOverview()
        {
            return tabPage1;
        }
        public TabPage getTabPageCharts()
        {
            return tabPage8;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ MENU STRIP
        public ToolStripMenuItem getMenuItemHotels()
        {
            return hotelsMenuItem;
        }
        public ToolStripMenuItem getMenuItemRoomFilter()
        {
            return menuItemRoomFilter;
        }
        public ToolStripMenuItem getMenuItemRoomAddUpdate()
        {
            return menuItemRoomAddUpdate;
        }
        public ToolStripMenuItem getMenuItemRoomDelete()
        {
            return menuItemRoomDelete;
        }
        public ToolStripMenuItem getMenuItemRoomBook()
        {
            return bookMenu;
        }
        public ToolStripMenuItem getMenuItemUsers()
        {
            return usersMenu;
        }
        public ToolStripMenuItem getMenuItemRegister()
        {
            return registerMenu;
        }
        public ToolStripMenuItem getMenuItemLogin()
        {
            return logInMenu;
        }
        public ToolStripMenuItem getMenuItemLogout()
        {
            return logOutMenu;
        }
        public ToolStripMenuItem getMenuItemOverview()
        {
            return firstPangeToolStripMenuItem;
        }
        public ToolStripMenuItem getMenuItemGraphics()
        {
            return graphicsMenuItem;
        }
        public ToolStripMenuItem getMenuItemJSON()
        {
            return jsonMenuItem;
        }
        public ToolStripMenuItem getMenuItemCSV()
        {
            return csvMenuItem;
        }
        public ToolStripMenuItem getMenuItemReports()
        {
            return reportsMenu;
        }


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ROOMS BY HOTEL
        public ListBox getListHotels()
        {
            return listHotels;
        }
        public ListBox getListRooms()
        {
            return listRooms;
        }
        public Label getRoomNameLabel()
        {
            return roomNameLabel;
        }
        public Label getRoomBookedLabel()
        {
            return roomBookedLabel;
        }
        public Label getRoomPriceLabel()
        {
            return roomPriceLabel;
        }
        public Label getRoomPositionLabel()
        {
            return roomPositionLabel;
        }
        public Label getRoomFacilitiesLabel()
        {
            return roomFacilitiesLabel;
        }
        public Panel getPanelRoomInfo()
        {
            return panelRoomInfo;
        }
        public Button getButtonRoomDelete()
        {
            return deleteRoomButton;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ FILTER ROOMS
        public CheckedListBox getFilterListFacility()
        {
            return listBoxFacility;
        }
        public CheckedListBox getFilterListLocation()
        {
            return listLocation;
        }
        public ListBox getFilterListFilteredRoom()
        {
            return listFilteredRoom;
        }
        public TextBox getFilterTextBoxPriceStart()
        {
            return priceStart;
        }
        public TextBox getFilterTextBoxPriceStop()
        {
            return priceStop;
        }
        public CheckBox getFilterCheckBoxN()
        {
            return N;
        }
        public CheckBox getFilterCheckBoxS()
        {
            return S;
        }
        public CheckBox getFilterCheckBoxE()
        {
            return E;
        }
        public CheckBox getFilterCheckBoxW()
        {
            return W;
        }
        public CheckBox getFilterCheckBoxBooked()
        {
            return booked;
        }
        public Button getFilterButton()
        {
            return filterButton;
        }
        public Label getFilterLabelName()
        {
            return filterName;
        }
        public Label getFilterLabelBooked()
        {
            return filterBooked;
        }
        public Label getFilterLabelPrice()
        {
            return filterPrice;
        }
        public Label getFilterLabelPosition()
        {
            return filterPosition;
        }
        public Label getFilterLabelFacilities()
        {
            return filterFacilities;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ADD ROOM
        public CheckedListBox getAddUpdateListFacilities()
        {
            return listBoxAddUpdateFacilities;
        }
        public ComboBox getAddUpdateCheckBoxHotel()
        {
            return comboBoxAddUpdateHotel;
        }
        public ComboBox getAddUpdateCheckBoxPosition()
        {
            return comboBoxAddUpdatePosition;
        }
        public TextBox getAddUpdateTextBoxPrice()
        {
            return addRoomPriceBox;
        }
        public TextBox getAddUpdateTextBoxNumber()
        {
            return addRoomNumberBox;
        }
        public Button getAddUpdateButton()
        {
            return addRoomButton;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ DELETE ROOM
        public Button getDeleteButton()
        {
            return deleteRoomButton;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ BOOK ROOM
        public Button getBookButton()
        {
            return bookButton;
        }
        public TextBox getBookTextBoxClient()
        {
            return bookNameTextBox;
        }
        public Label getBookLabel() {
            return label27; 
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ USER
        public TextBox getUserTextBoxName()
        {
            return userName;
        }
        public TextBox getUserTextBoxEmail()
        {
            return userEmail;
        }
        public TextBox getUserTextBoxPhone()
        {
              return userPhone;
        }
        public TextBox getUserTextBoxUsername()
        {
            return userUsername;
        }
        public TextBox getUserTextBoxPassword()
        {
            return userPass;
        }
        public ComboBox getUserComboBoxAccountType()
        {
            return userAccountType;
        }
        public ListBox getUserListBox()
        {
            return listUsers;
        }
        public Button getUserButtonCreate()
        {
            return userCreate;
        }
        public Button getUserButtonUpdate()
        {
            return userUpdate;
        }
        public Button getUserButtonDelete()
        {
            return userDelete;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ REGISTER
        public TextBox getRegName()
        {
            return regName;
        }
        public TextBox getRegEmail()
        {
            return regEmail;
        }
        public TextBox getRegPhone()
        {
            return regPhone;
        }
        public TextBox getRegUsername()
        {
            return regUsername;
        }
        public TextBox getRegPassword()
        {
            return regPassword;
        }
        public TextBox getRegRePass()
        {
            return regRePass;
        }
        public Button getRegisterButton()
        {
            return registerButton;
        }        
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ LOGIN
        public TextBox getInPassword()
        {
            return inPassword;
        }
        public TextBox getInUsername()
        {
            return inUsername;
        }     
        public Button getLoginButton()
        {
            return inLogin;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ CHARTS
        public Chart getChart1()
        {
            return chart1;
        }
        public Chart getChart2()
        {
            return chart2;
        }
        public Chart getChart3()
        {
            return chart3;
        }
        public Chart getChart4()
        {
            return chart4;
        }
    }
}
