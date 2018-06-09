using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Class_Schedule.Database;
using Class_Schedule.Control;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace Class_Schedule.Views
{
    /// <summary>
    /// Interaction logic for welcomepage.xaml
    /// </summary>
    public partial class welcomepage : UserControl
    {
        //MainWindow mainWindow = new MainWindow();
        excelMethods ex = new excelMethods();
        dbEntities db = new dbEntities();
        Session session = new Session();
        PageControls pc = new PageControls();
        //MainWindow mw = new MainWindow();
        private Grid mainGrid;
        private mainpage mainpage;
        private registrationpage registrationpage;

        public welcomepage(Grid mainGrid)
        {
            InitializeComponent();
            this.mainGrid = mainGrid;
            db.lastDbOpenUrl();
            db.dbConncetionValider();
            connectionValider();

            try
            {
                legendChanger_Welcome(db.langWordList(db.languageAsker()));
            }
            catch (Exception)
            {
                MessageBox.Show("Adja meg a szójegyzék helyét. (xls)");
                ex.langListRead();
                legendChanger_Welcome(db.langWordList(db.languageAsker()));
            }
        }

        void enterApp()
        {
            string user = user_inp.Text;
            string pass = pass_inp.Password;
            bool valider = false;
            valider = db.dbEnterValider(user, pass);
            List<string> sessionData = db.sessionDataSource(user, pass);
            DateTime date = DateTime.Now;
            if (valider == true && user != "Felhasználónév" && user != null && pass != "Jelszó" && pass != null)
            {
                session.UserData = sessionData;
                db.update_User_Last_LogDate(sessionData[1], date.ToString());
                mainGrid.Children.Clear();
                mainGrid.Children.Add(mainpage = new mainpage(mainGrid));
            }
            else
            {
                bool conn = db.dbConncetionValider();
                if (conn == true)
                {
                    info_out.Text = db.langWordList(db.languageAsker())[76];
                    pc.idozito(info_out);
                }
                else
                {
                    connectionValider();
                }
            }
        }
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            enterApp();
        }
        void legendChanger_Welcome(List<string> list)
        {
            try
            {
                user_inp.Text = list[7];
                pass_label.Text = list[8];
                loginBtn.Content = list[9];
                registraionBtn.Content = list[10];
            }
            catch (Exception)
            {
            }
        }
        public void connectionValider()
        {
            bool valid = db.dbConncetionValider();
            if (valid == true)
            {
                info_out.Text = "";

            }
            else
            {
                info_out.Text = db.langWordList(db.languageAsker())[77];
                pc.idozito(info_out);
            }
        }

        private void user_inp_GotFocus(object sender, RoutedEventArgs e)
        {

            info_out.Text = null;
            if (user_inp.Text == db.langWordList(db.languageAsker())[7])
                user_inp.Text = null;
        }

        private void user_inp_LostFocus(object sender, RoutedEventArgs e)
        {
            if(user_inp.Text == "")
            user_inp.Text = db.langWordList(db.languageAsker())[7] ;
        }

        private void pass_inp_GotFocus(object sender, RoutedEventArgs e)
        {
            info_out.Text = null;
            if (pass_label.Text == db.langWordList(db.languageAsker())[8])
                pass_label.Text = null;
        }

        private void pass_inp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pass_inp.Password == "")
                pass_label.Text = db.langWordList(db.languageAsker())[8];
        }

        private void registraionBtn_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(registrationpage = new registrationpage(mainGrid));
        }

        private void welcome_x_Click(object sender, RoutedEventArgs e)
        {
            pc.exitApplication();
        }

        private void welcome_minimize_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.WindowState = WindowState.Minimized;
        }

        private void btn_hu_Click(object sender, RoutedEventArgs e)
        {
            db.langWrite(1);
            legendChanger_Welcome(db.langWordList(db.languageAsker()));
        }

        private void btn_de_Click(object sender, RoutedEventArgs e)
        {
            db.langWrite(2);
            legendChanger_Welcome(db.langWordList(db.languageAsker()));
        }

        private void btn_en_Click(object sender, RoutedEventArgs e)
        {
            db.langWrite(3);
            legendChanger_Welcome(db.langWordList(db.languageAsker()));
        }

        private void pass_inp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            enterApp();
        }

        private void user_inp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            enterApp();
        }
    }
}
