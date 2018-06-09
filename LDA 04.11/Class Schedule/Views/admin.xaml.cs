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

namespace Class_Schedule.Views
{
    /// <summary>
    /// Interaction logic for admin.xaml
    /// </summary>
    public partial class admin : UserControl
    {
        excelMethods ex = new excelMethods();
        dbEntities db = new dbEntities();
        Session session = new Session();
        PageControls pc = new PageControls();
        ListViewLoaders listLoader = new ListViewLoaders();
        private Grid mainGrid;
        private mainpage mainpage;
        DateTime date = DateTime.UtcNow;
        public admin(Grid mainGrid)
        {
            InitializeComponent();
            this.mainGrid = mainGrid;
            session.admin_execution_popper(admin_execution_btn, session.UserData[7], session.UserData[4]);
            listLoader.userListLoader(lv_admin_userlist);
            listLoader.szallitokListLoader(listView_szallitok);
            listLoader.adminMeasureLoader(db.admin_listviewLoader(tbx_cikk_tab3.Text, tbx_charge_tab3.Text, tbx_kw_tab3.Text, tbx_beerk_tab3.Text), lv_admin);
            activityLister();
           
            try
            {
                legendChanger_Admin(db.langWordList(db.languageAsker()));
            }
            catch (Exception)
            {
            }
        }

        void legendChanger_Admin(List<string> list)
        {
            try
            {
                tab1.Header = list[91];
                tab2.Header = list[89];
                tab3.Header = list[90];
                tab4.Header = list[98];
                admin_szint_1.Content = list[101];
                admin_szint_2.Content = list[102];
                check_admin_nyitott.Content = list[4];
                check_admin_zart.Content = list[5];
                leg_felh.Content = list[7];
                leg_realname.Content = list[11];
                leg_email.Content = list[13];
                admin_valid_check.Content = list[49];
                admin_adminjog_check.Content = list[50];
                leg_jog.Content = list[51];
                admin_delete_btn.Content = list[52];
                admin_save_btn.Content = list[48];
                adminBackBtn.Content = list[14];
                leg_helyreallitas.Content = list[59];
                excel.Content = list[53];
                leg_kimentes.Content = list[60];
                saveToExcelBtn.Content = list[54];
                leg_ujhely.Content = list[61];
                btn_newDbUrl.Content = list[55];
                leg_szallitofel.Content = list[58];
                btn_szallitoSave.Content = list[56];
                btn_szallitoDelete.Content = list[57];
                leg_szojegyzek.Content = list[78];
                leg_keres1.Content = list[99];
                leg_keres2.Content = list[99];
                btn_szojegyzfriss.Content = list[79];
                btn_save_tab3.Content = list[2];
                btn_delete_tab3.Content = list[92];
                admin_cikkszam_t3.Content = list[15];
                admin_charge_t3.Content = list[16];
                admin_beerk_t3.Content = list[17];
                admin_cikkszam2_t3.Content = list[15];
                admin_charge2_t3.Content = list[16];
                admin_beerk2_t3.Content = list[17];
                admin_uj_t3.Content = list[93];
                admin_regi_t3.Content = list[94];

                lv_admin_col1.Header = list[15];
                lv_admin_col2.Header = list[16];
                lv_admin_col3.Header = list[39];
                lv_admin_col4.Header = list[17];
                listView_szallitok_col1.Header = list[81];
                lv_admin_userlist_col1.Header = list[7];
                lv_admin_userlist_col2.Header = list[11];
                lv_admin_userlist_col3.Header = list[13];
                lv_admin_userlist_col4.Header = list[50];
                lv_admin_userlist_col5.Header = list[82];
                lv_admin_userlist_col6.Header = list[49];
                lv_admin_userlist_col7.Header = list[103];
                activity_listView_col1.Header = list[7];
                activity_listView_col2.Header = list[96];
                activity_listView_col3.Header = list[100];
                activity_listView_col4.Header = list[15];
                activity_listView_col5.Header = list[16];
                activity_listView_col6.Header = list[17];
                activity_listView_col7.Header = list[97];
            }
            catch (Exception)
            {
            }
        }
     
        //    private void admin_exit_Click(object sender, RoutedEventArgs e)
        //{
        //    Environment.Exit(0);
        //}

        private void adminBackBtn_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(mainpage = new mainpage(mainGrid));
        }

        private void listView_4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pc.idozito(admin_info_out);
            ListViewLoaders.UserItems data = (sender as ListView).SelectedItem as ListViewLoaders.UserItems;
            try
            {
                user_admin_inp.Text = data.col1;
                realname_admin_inp.Text = data.col2;
                email_admin_inp.Text = data.col3;
                if (data.col4 == "igen")
                {
                    admin_adminjog_check.IsChecked = true;
                }
                else
                {
                    admin_adminjog_check.IsChecked = false;
                }
                if (data.col6 == "igen")
                {
                    admin_valid_check.IsChecked = true;
                }
                else
                {
                    admin_valid_check.IsChecked = false;
                }
                if (data.col5 == "1")
                {
                    admin_szint_1.IsChecked = true;
                    admin_szint_2.IsChecked = false;
                    admin_szint_3.IsChecked = false;
                }
                if (data.col5 == "2")
                {
                    admin_szint_1.IsChecked = false;
                    admin_szint_2.IsChecked = true;
                    admin_szint_3.IsChecked = false;
                }
                if (data.col5 == "3")
                {
                    admin_szint_1.IsChecked = false;
                    admin_szint_2.IsChecked = false;
                    admin_szint_3.IsChecked = true;
                }
                if(data.col5 == "0")
                {
                    admin_szint_1.IsChecked = false;
                    admin_szint_2.IsChecked = false;
                    admin_szint_3.IsChecked = false;
                }
            }
            catch
            {
                user_admin_inp.Text = "";
                realname_admin_inp.Text = "";
                email_admin_inp.Text = "";
                admin_adminjog_check.IsChecked = false;
                admin_valid_check.IsChecked = false;
                admin_szint_1.IsChecked = false;
                admin_szint_2.IsChecked = false;
                admin_szint_3.IsChecked = false;
            }
        }

        private void admin_delete_btn_Click(object sender, RoutedEventArgs e)
        {
            db.deleteUser(user_admin_inp.Text);
            listLoader.userListLoader(lv_admin_userlist);
        }

        private void admin_save_btn_Click(object sender, RoutedEventArgs e)
        {
            List<object> userDatas = new List<object>();
            int valid = 0;
            int adminjog = 0;
            int auth = 0;
            if (admin_valid_check.IsChecked == true) { valid = 1; }
            if (admin_adminjog_check.IsChecked == true) { adminjog = 1; }
            if (admin_szint_1.IsChecked == true) { auth = 1; }
            if (admin_szint_2.IsChecked == true) { auth = 2; }
            if (admin_szint_3.IsChecked == true) { auth = 3; }

            userDatas.Add(user_admin_inp.Text);
            userDatas.Add(realname_admin_inp.Text);
            userDatas.Add(auth);
            userDatas.Add(email_admin_inp.Text);
            userDatas.Add(valid);
            userDatas.Add(adminjog);

            db.userCahnge(userDatas);
            listLoader.userListLoader(lv_admin_userlist);
            admin_info_out.Text = db.langWordList(db.languageAsker())[73];
            pc.idozito(admin_info_out);
        }

        private void admin_szint_1_Checked(object sender, RoutedEventArgs e)
        {
            admin_szint_2.IsChecked = false;
            admin_szint_3.IsChecked = false;
        }

        private void admin_szint_2_Checked(object sender, RoutedEventArgs e)
        {
            admin_szint_1.IsChecked = false;
            admin_szint_3.IsChecked = false;
        }

        private void admin_szint_3_Checked(object sender, RoutedEventArgs e)
        {
            admin_szint_1.IsChecked = false;
            admin_szint_2.IsChecked = false;
        }

        private void excel_Click(object sender, RoutedEventArgs e)
        {
            admin_info_out.Text = db.langWordList(db.languageAsker())[72];
            ex.excelTablaBeolvaso();
            admin_info_out.Text = "";
        }

        private void saveToExcelBtn_Click(object sender, RoutedEventArgs e)
        {

            admin_info_out.Text = db.langWordList(db.languageAsker())[72];
            ex.excelAdatbazisKimento();
            admin_info_out.Text = "";
        }

        private void btn_szallitoSave_Click(object sender, RoutedEventArgs e)
        {
            if (admin_szallitok_inp.Text != db.langWordList(db.languageAsker())[80])
            {
                bool valid = db.szallitoValiderDb(admin_szallitok_inp.Text);
                if (valid == true)
                {
                    listLoader.szallitokListLoader(listView_szallitok);
                    admin_info_out.Text = db.langWordList(db.languageAsker())[71];
                    pc.idozito(admin_info_out);
                    admin_szallitok_inp.Text = db.langWordList(db.languageAsker())[81];
                }
                else
                {
                    admin_info_out.Text = db.langWordList(db.languageAsker())[70];
                    pc.idozito(admin_info_out);
                }
            }
        }
        private void listView_szallitok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewLoaders.SzallitoItems data = (sender as ListView).SelectedItem as ListViewLoaders.SzallitoItems;
            try
            {
                admin_szallitok_inp.Text = data.col1;
            }
            catch
            {
                admin_szallitok_inp.Text = "";
            }
        }

        private void btn_szallitoDelete_Click(object sender, RoutedEventArgs e)
        {
            bool valid = db.szallitoDeleteDb(admin_szallitok_inp.Text);
            if (valid == true)
            {
                admin_info_out.Text = db.langWordList(db.languageAsker())[68];
                pc.idozito(admin_info_out);
                listLoader.szallitokListLoader(listView_szallitok);
            }
            else
            {
                admin_info_out.Text = db.langWordList(db.languageAsker())[69];
                pc.idozito(admin_info_out);
            }
        }

        private void btn_newDbUrl_Click(object sender, RoutedEventArgs e)
        {
            db.dbUrlChanger();
            listLoader.szallitokListLoader(listView_szallitok);
            listLoader.userListLoader(lv_admin_userlist);
        }
        private void admin_minimize_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.WindowState = WindowState.Minimized;
        }

        private void btn_szojegyzfriss_Click(object sender, RoutedEventArgs e)
        {
            ex.langListRead();
        }

        private void lv_admin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewLoaders.AdminMeasureItems data = (sender as ListView).SelectedItem as ListViewLoaders.AdminMeasureItems;
            try
            {
                tbx_cikkView_tab3.Text = data.col1;
                tbx_chargeView_tab3.Text = data.col2;
                tbx_beerkView_tab3.Text = data.col4;
            }
            catch
            {
                tbx_cikkView_tab3.Text = "";
                tbx_chargeView_tab3.Text = "";
                tbx_beerkView_tab3.Text = "";
            }
        }

        private void tbx_cikk_tab3_TextChanged(object sender, TextChangedEventArgs e)
        {
            listLoader.adminMeasureLoader(db.admin_listviewLoader(tbx_cikk_tab3.Text, tbx_charge_tab3.Text, tbx_kw_tab3.Text, tbx_beerk_tab3.Text), lv_admin);
        }

        private void tbx_charge_tab3_TextChanged(object sender, TextChangedEventArgs e)
        {
            listLoader.adminMeasureLoader(db.admin_listviewLoader(tbx_cikk_tab3.Text, tbx_charge_tab3.Text, tbx_kw_tab3.Text, tbx_beerk_tab3.Text), lv_admin);
        }

        private void tbx_kw_tab3_TextChanged(object sender, TextChangedEventArgs e)
        {
            listLoader.adminMeasureLoader(db.admin_listviewLoader(tbx_cikk_tab3.Text, tbx_charge_tab3.Text, tbx_kw_tab3.Text, tbx_beerk_tab3.Text), lv_admin);
        }

        private void tbx_beerk_tab3_TextChanged(object sender, TextChangedEventArgs e)
        {
            listLoader.adminMeasureLoader(db.admin_listviewLoader(tbx_cikk_tab3.Text, tbx_charge_tab3.Text, tbx_kw_tab3.Text, tbx_beerk_tab3.Text), lv_admin);
        }

        private void btn_save_tab3_Click(object sender, RoutedEventArgs e)
        {
            string cikk1, charge1, beerk1, cikk2, charge2, beerk2;
            if (tbx_cikkModify_tab3.Text == "")
            { cikk1 = tbx_cikkView_tab3.Text; }
            else
            { cikk1 = tbx_cikkModify_tab3.Text; }

            if (tbx_chargeModify_tab3.Text == "")
            { charge1 = tbx_chargeView_tab3.Text; }
            else
            { charge1 = tbx_chargeModify_tab3.Text; }

            if (tbx_beerkModify_tab3.Text == "")
            { beerk1 = tbx_beerkView_tab3.Text; }
            else
            { beerk1 = tbx_beerkModify_tab3.Text; }
            cikk2 = tbx_cikkView_tab3.Text;
            charge2 = tbx_chargeView_tab3.Text;
            beerk2 = tbx_beerkView_tab3.Text;

            db.admin_meresModify(cikk1, charge1, beerk1, cikk2, charge2, beerk2);
            listLoader.adminMeasureLoader(db.admin_listviewLoader(tbx_cikk_tab3.Text, tbx_charge_tab3.Text, tbx_kw_tab3.Text, tbx_beerk_tab3.Text), lv_admin);
        }
        private void activityLister()
        {
            string allapot = "";
            if (check_admin_zart.IsChecked == true && check_admin_nyitott.IsChecked == true)
            {
                allapot = "";
            }
            else if (check_admin_zart.IsChecked == true)
            {
                allapot = "1";
            }
            else if (check_admin_nyitott.IsChecked == true)
            {
                allapot = "0";
            }

            listLoader.Activity_ListLoader(db.UserActivityLister(tbx_admin_tab4_user.Text, tbx_admin_tab4_activity.Text, allapot, tbx_admin_tab4_cikk.Text, tbx_admin_tab4_charge.Text, tbx_admin_tab4_beerk.Text, tbx_admin_tab4_activitydate.Text), activity_listView);
        }
        private void btn_delete_tab3_Click(object sender, RoutedEventArgs e)
        {
            db.admin_meresDelete(tbx_cikkView_tab3.Text, tbx_chargeView_tab3.Text, tbx_beerkView_tab3.Text);
            listLoader.adminMeasureLoader(db.admin_listviewLoader(tbx_cikk_tab3.Text, tbx_charge_tab3.Text, tbx_kw_tab3.Text, tbx_beerk_tab3.Text), lv_admin);
        }

        private void tbx_admin_tab4_user_TextChanged(object sender, TextChangedEventArgs e)
        {
            activityLister();
        }

        private void tbx_admin_tab4_activity_TextChanged(object sender, TextChangedEventArgs e)
        {
            activityLister();
        }

        private void tbx_admin_tab4_cikk_TextChanged(object sender, TextChangedEventArgs e)
        {
            activityLister();
        }

        private void tbx_admin_tab4_charge_TextChanged(object sender, TextChangedEventArgs e)
        {
            activityLister();
        }

        private void tbx_admin_tab4_beerk_TextChanged(object sender, TextChangedEventArgs e)
        {
            activityLister();
        }

        private void tbx_admin_tab4_activitydate_TextChanged(object sender, TextChangedEventArgs e)
        {
            activityLister();
        }

        private void check_admin_nyitott_Checked(object sender, RoutedEventArgs e)
        {
            activityLister();
        }

        private void check_admin_zart_Checked(object sender, RoutedEventArgs e)
        {
            activityLister();
        }

        private void check_admin_zart_Unchecked(object sender, RoutedEventArgs e)
        {
            activityLister();
        }
        private void check_admin_nyitott_Unchecked(object sender, RoutedEventArgs e)
        {
            activityLister();
        }

        private void admin_execution_btn_Click(object sender, RoutedEventArgs e)
        {
            db.dballapotchanger();
        }
    }
}
