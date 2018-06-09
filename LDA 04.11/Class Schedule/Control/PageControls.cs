using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Class_Schedule.Database;
using Class_Schedule.Control;
using System.Windows.Controls;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Class_Schedule.Views
{
    class PageControls : UserControl
    {
        public void idozito(TextBlock infoOut)
        {
            System.Windows.Threading.DispatcherTimer t = new System.Windows.Threading.DispatcherTimer();
            t.Tick += idozitettTartalom;
            t.Interval = new TimeSpan(0, 0, 8);
            t.Start();
            void idozitettTartalom(object sender, EventArgs e)
            {
                try { infoOut.Text = ""; } catch (Exception) { }
            }
        }
        public void exitApplication()
        {
            Environment.Exit(0);
        }
    }
}
