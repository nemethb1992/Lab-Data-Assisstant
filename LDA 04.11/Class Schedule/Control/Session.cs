using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Class_Schedule.Database;
using System.Linq;
using System.Text;

namespace Class_Schedule.Control
{
    public class Session
    {

        dbEntities db = new dbEntities();
        private static List<string> UserDatas;
        private static string database;
        public string databaseUrl { get { return database; } set { database = value; } }
        public List<string> UserData {get{ return UserDatas; } set { UserDatas = value; } }

        public void pageLoadByAuthority(Button button1, Button button2, TabItem tab1, TabItem tab2, string admin, string level)
        {
            if (admin != "1")
            {
                button1.Visibility = Visibility.Hidden;
            }
            if (level != "2" && level != "3")
            {
                tab1.Visibility = Visibility.Hidden;
                button2.Visibility = Visibility.Hidden;
            }

        }
        public void admin_execution_popper(Button button, string admin, string level)
        {
            if (admin == "1" && level == "3")
            {
                button.Visibility = Visibility.Visible;
            }
        }
    }

}
