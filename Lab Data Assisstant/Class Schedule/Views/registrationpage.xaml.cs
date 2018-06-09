using System;
using System.Windows;
using System.Windows.Controls;
using Class_Schedule.Database;
using Class_Schedule.Control;
using System.Collections.Generic;

namespace Class_Schedule.Views
{
    /// <summary>
    /// Interaction logic for registrationpage.xaml
    /// </summary>
    public partial class registrationpage : UserControl
    {
        dbEntities db = new dbEntities();
        PageControls pc = new PageControls();
        Session session = new Session();
        private Grid mainGrid;
        private welcomepage welcomepage;
        DateTime date = DateTime.Now;
        public registrationpage(Grid mainGrid)
        { 
            InitializeComponent();
            this.mainGrid = mainGrid;
            legendChanger_Registration(db.langWordList(db.languageAsker()));
        }

    private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void mainpageBackBtn_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(welcomepage = new welcomepage(mainGrid));
        }
        private void user_inp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (user_inp.Text == "")
                user_inp.Text = db.langWordList(db.languageAsker())[7];
        }
        private void realname_inp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (realname_inp.Text == "")
                realname_inp.Text = db.langWordList(db.languageAsker())[11];
        }
        private void email_inp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (email_inp.Text == "")
                email_inp.Text = db.langWordList(db.languageAsker())[13];
        }
        private void user_inp_GotFocus(object sender, RoutedEventArgs e)
        {
            //info_out.Text = null;
            if (user_inp.Text == db.langWordList(db.languageAsker())[7])
                user_inp.Text = null;
        }
        private void realname_inp_GotFocus(object sender, RoutedEventArgs e)
        {
            //info_out.Text = null;
            if (realname_inp.Text == db.langWordList(db.languageAsker())[11])
                realname_inp.Text = null;
        }
        private void email_inp_GotFocus(object sender, RoutedEventArgs e)
        {
            //info_out.Text = null;
            if (email_inp.Text == db.langWordList(db.languageAsker())[13])
                email_inp.Text = null;
        }
        private void pass_inp_GotFocus(object sender, RoutedEventArgs e)
        {
            //info_out.Text = null;
            if (pass_label.Text == db.langWordList(db.languageAsker())[8])
                pass_label.Text = null;
        }
        private void pass_inp2_GotFocus(object sender, RoutedEventArgs e)
        {
            //info_out.Text = null;
            if (pass_label2.Text == db.langWordList(db.languageAsker())[12])
                pass_label2.Text = null;
        }

        private void pass_inp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pass_inp.Password == "")
                pass_label.Text = db.langWordList(db.languageAsker())[8];
        }
        private void pass_inp2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pass_inp2.Password == "")
                pass_label2.Text = db.langWordList(db.languageAsker())[12];
        }

        private void registBtn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = false;
            string user = user_inp.Text;
            string pass = pass_inp.Password;
            string pass2 = pass_inp2.Password;
            string realname = realname_inp.Text;
            string email = email_inp.Text;
            if (user != db.langWordList(db.languageAsker())[7] && user != "" && pass != null && pass2 != null && realname != db.langWordList(db.languageAsker())[11] && realname != "" && email != db.langWordList(db.languageAsker())[13] && email != "")
            {
                if (pass == pass2)
                {
                    valid = db.dbUserRegistration(user, pass, pass2, realname, email);
                    if (valid == false)
                    {
                        registration_info_out.Text = db.langWordList(db.languageAsker())[74];
                        pc.idozito(registration_info_out);
                    }
                }
                else
                {
                    registration_info_out.Text = db.langWordList(db.languageAsker())[75];
                    pc.idozito(registration_info_out);
                }
            }
            else
            {
                valid = false;
            }
            if (valid == true)
            {
                user_inp.Text = db.langWordList(db.languageAsker())[7];
                pass_label.Text = db.langWordList(db.languageAsker())[8];
                pass_label2.Text = db.langWordList(db.languageAsker())[12];
                pass_inp.Password = null;
                pass_inp2.Password = null;
                realname_inp.Text = db.langWordList(db.languageAsker())[11];
                email_inp.Text = db.langWordList(db.languageAsker())[13];
                registration_info_out.Text = db.langWordList(db.languageAsker())[80];
                pc.idozito(registration_info_out);
            }
        }

        private void registration_x_Click(object sender, RoutedEventArgs e)
        {
            pc.exitApplication();
        }
        private void registration_minimize_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.WindowState = WindowState.Minimized;
        }
        private void legendChanger_Registration(List<string> lista)
        {
            realname_inp.Text = lista[11];
            user_inp.Text = lista[7];
            pass_label.Text = lista[8];
            pass_label2.Text = lista[12];
            email_inp.Text = lista[13];
            registBtn.Content = lista[10];
            mainpageBackBtn.Content = lista[14];
        }
    }
    
}
