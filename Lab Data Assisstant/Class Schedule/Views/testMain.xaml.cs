using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Text.RegularExpressions;
using System.Threading;




namespace Class_Schedule.Views
{
    /// <summary>
    /// Interaction logic for mainpage.xaml
    /// </summary>
    public partial class testMain : UserControl
    {
        //MainWindow mainWindow = new MainWindow();
        dbEntities db = new dbEntities();
        ListViewLoaders listLoader = new ListViewLoaders();
        Session session = new Session();
        PageControls pc = new PageControls();
        private Grid mainGrid;
        private welcomepage welcomepage;
        private admin admin;
        DateTime date = DateTime.Now;

        public testMain(Grid grid)
        {
            InitializeComponent();
            //session.pageLoadByAuthority(mainpageToAdmin, save_btn2, tab3, tab2, session.UserData[7], session.UserData[4]);
            legendChanger_Mainpage(db.langWordList(db.languageAsker()));
            searched_cikk_listView_loader();
            onlyViewModeInputClose();
            szallitokListazo();
            welcomeSign();
        }

        void welcomeSign()
        {
            main_info_out.Text = db.langWordList(db.languageAsker())[62];
            pc.idozito(main_info_out);
        }
        void onlyViewModeInputClose()
        {
            if (session.UserData[4] == "1")
            {
                main_info_out.Margin = new Thickness(325, 83, 438, 646);
                in1_cikkszam.IsReadOnly = true;
                in1_charge.IsReadOnly = true;
                in1_beerkdatum.IsReadOnly = true;
                in1_szallito.IsReadOnly = true;
                in1_anyag_tipus.IsReadOnly = true;
                in1_anyagnev.IsReadOnly = true;
                in1_pc.IsReadOnly = true;
                in1_utomunk.IsReadOnly = true;
                in1_folyokep_c.IsReadOnly = true;
                in1_utokalapacs_j.IsReadOnly = true;
                in1_folyokep_suly.IsReadOnly = true;
                in1_suruseg.IsReadOnly = true;
                in1_szin.IsReadOnly = true;
                in1_szakszig_min.IsReadOnly = true;
                in1_szakszig_max.IsReadOnly = true;
                in1_utesal_min.IsReadOnly = true;
                in1_utesal_max.IsReadOnly = true;
                in1_foly_g_min.IsReadOnly = true;
                in1_foly_g_max.IsReadOnly = true;
                in1_foly_cm_min.IsReadOnly = true;
                in1_foly_cm_max.IsReadOnly = true;
                in1_toltotart_min.IsReadOnly = true;
                in1_toltotart_max.IsReadOnly = true;
                in1_uto_date.IsReadOnly = true;
                in1_kw.IsReadOnly = true;
                in1_allapot.IsReadOnly = true;
                in1_viztartalom.IsReadOnly = true;
                in1_szakszig.IsReadOnly = true;
                in1_szakszig_gy.IsReadOnly = true;
                in1_utesallosag.IsReadOnly = true;
                in1_utesallosag_gy.IsReadOnly = true;
                in1_folykep_g_1.IsReadOnly = true;
                in1_folykep_g_2.IsReadOnly = true;
                in1_folykep_g_gy.IsReadOnly = true;
                in1_folykep_cm_1.IsReadOnly = true;
                in1_folykep_cm_2.IsReadOnly = true;
                in1_folykep_cm_gy.IsReadOnly = true;
                in1_toltoanyag.IsReadOnly = true;
                in1_toltoanyag_gy.IsReadOnly = true;
                in1_megjegyzes.IsReadOnly = true;
            }
        }
        void legendChanger_Mainpage(List<string> list)
        {
            try
            {
                if (session.UserData[4] == "1") { tab2.Header = list[1]; }
                else { tab2.Header = list[2]; }
                tab1.Header = list[0];
                tab3.Header = list[3];
                mainpageBackBtn.Content = list[6];

                t1_cikk.Content = list[15];
                t1_charge.Content = list[16];
                t1_szallito.Content = list[18];
                t1_anyagnev.Content = list[19];
                t1_beerk.Content = list[17];
                nyitott_cbox.Content = list[4];
                lezart_cbox.Content = list[5];

                t2_cikk.Content = list[15];
                t3_cikk.Content = list[15];
                t2_charge.Content = list[16];
                t3_charge.Content = list[16];
                t2_beerk.Content = list[17];
                t3_beerk.Content = list[17];
                save_btn2.Content = list[48];
                save_btn3.Content = list[48];
                t2_szallito.Content = list[18];
                t3_szallito.Content = list[18];
                t2_anyagtipus.Content = list[20];
                t3_anyagtipus.Content = list[20];
                t2_anyagnev.Content = list[19];
                t3_anyagnev.Content = list[19];
                t2_pc.Content = list[21];
                t3_pc.Content = list[21];
                t2_utomunk.Content = list[22];
                t3_utomunk.Content = list[22];
                t2_folyokephom.Content = list[23];
                t3_folyokephom.Content = list[23];
                t2_utokal.Content = list[24];
                t3_utokal.Content = list[24];
                t2_folykepsuly.Content = list[25];
                t3_folykepsuly.Content = list[25];
                t2_suruseg.Content = list[26];
                t3_suruseg.Content = list[26];
                t2_szin.Content = list[27];
                t3_szin.Content = list[27];
                t2_szakszigmin.Content = list[28];
                t3_szakszigmin.Content = list[28];
                t2_szakszigmax.Content = list[29];
                t3_szakszigmax.Content = list[29];
                t2_utesmin.Content = list[30];
                t3_utesmin.Content = list[30];
                t2_utesmax.Content = list[31];
                t3_utesmax.Content = list[31];
                t2_folykGmin.Content = list[32];
                t3_folykGmin.Content = list[32];
                t2_folykGmax.Content = list[33];
                t3_folykGmax.Content = list[33];
                t2_folykCMmin.Content = list[34];
                t3_folykCMmin.Content = list[34];
                t2_folykCMmax.Content = list[35];
                t3_folykCMmax.Content = list[35];
                t2_toltomin.Content = list[36];
                t3_toltomin.Content = list[36];
                t2_toltomax.Content = list[37];
                t3_toltomax.Content = list[37];
                t2_utolsodatum.Content = list[38];
                t3_utolsodatum.Content = list[38];
                t2_kw.Content = list[39];
                t3_kw.Content = list[39];
                t2_allapot.Content = list[40];
                t3_allapot.Content = list[40];
                t2_viztart.Content = list[41];
                t3_viztart.Content = list[41];
                t2_szakszig.Content = list[42];
                t3_szakszig.Content = list[42];
                t2_szakszigGY.Content = list[84];
                t3_szakszigGY.Content = list[84];
                t2_utes.Content = list[43];
                t3_utes.Content = list[43];
                t2_utesGY.Content = list[85];
                t3_utesGY.Content = list[85];
                t2_folykG.Content = list[44];
                t3_folykG.Content = list[44];
                t2_folykGGY.Content = list[86];
                t3_folykGGY.Content = list[86];
                t2_folykCM.Content = list[45];
                t3_folykCM.Content = list[45];
                t2_folykCMGY.Content = list[87];
                t3_folykCMGY.Content = list[87];
                t2_tolto.Content = list[46];
                t3_tolto.Content = list[46];
                t2_toltoGY.Content = list[88];
                t3_toltoGY.Content = list[88];
                t2_megjegyz.Content = list[47];
                t3_megjegyz.Content = list[47];
                t3_szallitok.Content = list[18];
                t3_comboLabel.Content = list[83];
            }
            catch (Exception)
            {
            }
        }
        private void szallitokListazo()
        {

            List<string> lista = new List<string>();
            lista = db.szallitoListaStr();
            comboBox3.Items.Clear();
            for (int i = 0; i < lista.Count; i++)
            {
                comboBox3.Items.Add(lista[i]);
            }
        }
        private void searched_cikk_listView_loader()
        {
            string allapot = "nyitott";
            if (nyitott_cbox.IsChecked == true)
            {
                allapot = "nyitott";
            }
            else
            {
                allapot = "zárt";
            }
            listLoader.listViewLoader(db.dbListViewLoader(allapot, input_cikkszam.Text, input_charge.Text, input_beerk_datum.Text, input_anyagnev.Text, input_szallito.Text), listView_4);
        }

        bool inputAllFilled_Bool(string type)
        {
            bool valid = false;
            if (type == "change")
            {
                if (in1_viztartalom.Text == "") { in1_viztartalom.Text = "-"; }
                if (in1_szakszig.Text == "") { in1_szakszig.Text = "-"; }
                if (in1_szakszig_gy.Text == "") { in1_szakszig_gy.Text = "-"; }
                if (in1_utesallosag.Text == "") { in1_utesallosag.Text = "-"; }
                if (in1_utesallosag_gy.Text == "") { in1_utesallosag_gy.Text = "-"; }
                if (in1_folykep_g_1.Text == "") { in1_folykep_g_1.Text = "0"; }
                if (in1_folykep_g_2.Text == "") { in1_folykep_g_2.Text = in1_folykep_g_1.Text; }
                if (in1_folykep_g_gy.Text == "") { in1_folykep_g_gy.Text = "-"; }
                if (in1_folykep_cm_1.Text == "") { in1_folykep_cm_1.Text = "0"; }
                if (in1_folykep_cm_2.Text == "") { in1_folykep_cm_2.Text = in1_folykep_cm_1.Text; }
                if (in1_folykep_cm_gy.Text == "") { in1_folykep_cm_gy.Text = "-"; }
                if (in1_toltoanyag.Text == "") { in1_toltoanyag.Text = "-"; }
                if (in1_toltoanyag_gy.Text == "") { in1_toltoanyag_gy.Text = "-"; }
                if (in1_megjegyzes.Text == "") { in1_megjegyzes.Text = "-"; }


                if (in1_cikkszam.Text != "" &&
                    in1_charge.Text != "" &&
                    in1_beerkdatum.Text != "" &&
                    in1_szallito.Text != "" &&
                    in1_anyag_tipus.Text != "" &&
                    in1_anyagnev.Text != "" &&
                    in1_pc.Text != "" &&
                    in1_utomunk.Text != "" &&
                    in1_folyokep_c.Text != "" &&
                    in1_utokalapacs_j.Text != "" &&
                    in1_folyokep_suly.Text != "" &&
                    in1_suruseg.Text != "" &&
                    in1_szin.Text != "" &&
                    in1_szakszig_min.Text != "" &&
                    in1_szakszig_max.Text != "" &&
                    in1_utesal_min.Text != "" &&
                    in1_utesal_max.Text != "" &&
                    in1_foly_g_min.Text != "" &&
                    in1_foly_g_max.Text != "" &&
                    in1_foly_cm_min.Text != "" &&
                    in1_foly_cm_max.Text != "" &&
                    in1_toltotart_min.Text != "" &&
                    in1_toltotart_max.Text != "" &&
                    in1_uto_date.Text != "" &&
                    in1_kw.Text != "" &&
                    in1_allapot.Text != "" &&
                    in1_viztartalom.Text != "" &&
                    in1_szakszig.Text != "" &&
                    in1_szakszig_gy.Text != "" &&
                    in1_utesallosag.Text != "" &&
                    in1_utesallosag_gy.Text != "" &&
                    in1_folykep_g_1.Text != "" &&
                    in1_folykep_g_2.Text != "" &&
                    in1_folykep_g_gy.Text != "" &&
                    in1_folykep_cm_1.Text != "" &&
                    in1_folykep_cm_2.Text != "" &&
                    in1_folykep_cm_gy.Text != "" &&
                    in1_toltoanyag.Text != "" &&
                    in1_toltoanyag_gy.Text != "" &&
                    in1_megjegyzes.Text != "")
                {
                    if ((in1_allapot.Text.ToLower() == "zárt" || in1_allapot.Text.ToLower() == "nyitott") && (in1_utomunk.Text.ToLower() == "igen" || in1_utomunk.Text.ToLower() == "nem"))
                    {
                        valid = true;
                    }
                }
            }
            if (type == "new")
            {
                if (in2_viztartalom.Text == "") { in2_viztartalom.Text = "-"; }
                if (in2_szakszig.Text == "") { in2_szakszig.Text = "-"; }
                if (in2_szakszig_gy.Text == "") { in2_szakszig_gy.Text = "-"; }
                if (in2_utesallosag.Text == "") { in2_utesallosag.Text = "-"; }
                if (in2_utesallosag_gy.Text == "") { in2_utesallosag_gy.Text = "-"; }
                if (in2_folykep_g_1.Text == "") { in2_folykep_g_1.Text = "0"; }
                if (in2_folykep_g_2.Text == "") { in2_folykep_g_2.Text = in2_folykep_g_1.Text; }
                if (in2_folykep_g_gy.Text == "") { in2_folykep_g_gy.Text = "-"; }
                if (in2_folykep_cm_1.Text == "") { in2_folykep_cm_1.Text = "0"; }
                if (in2_folykep_cm_2.Text == "") { in2_folykep_cm_2.Text = in2_folykep_cm_1.Text; }
                if (in2_folykep_cm_gy.Text == "") { in2_folykep_cm_gy.Text = "-"; }
                if (in2_toltoanyag.Text == "") { in2_toltoanyag.Text = "-"; }
                if (in2_toltoanyag_gy.Text == "") { in2_toltoanyag_gy.Text = "-"; }
                if (in2_megjegyzes.Text == "") { in2_megjegyzes.Text = "-"; }

                if (in2_cikkszam.Text != "" &&
                    in2_charge.Text != "" &&
                    in2_beerkdatum.Text != "" &&
                    in2_szallito.Text != "" &&
                    in2_anyag_tipus.Text != "" &&
                    in2_anyagnev.Text != "" &&
                    in2_pc.Text != "" &&
                    in2_utomunk.Text != "" &&
                    in2_folyokep_c.Text != "" &&
                    in2_utokalapacs_j.Text != "" &&
                    in2_folyokep_suly.Text != "" &&
                    in2_suruseg.Text != "" &&
                    in2_szin.Text != "" &&
                    in2_szakszig_min.Text != "" &&
                    in2_szakszig_max.Text != "" &&
                    in2_utesal_min.Text != "" &&
                    in2_utesal_max.Text != "" &&
                    in2_foly_g_min.Text != "" &&
                    in2_foly_g_max.Text != "" &&
                    in2_foly_cm_min.Text != "" &&
                    in2_foly_cm_max.Text != "" &&
                    in2_toltotart_min.Text != "" &&
                    in2_toltotart_max.Text != "" &&
                    in2_uto_date.Text != "" &&
                    in2_kw.Text != "" &&
                    in2_allapot.Text != "" &&
                    in2_viztartalom.Text != "" &&
                    in2_szakszig.Text != "" &&
                    in2_szakszig_gy.Text != "" &&
                    in2_utesallosag.Text != "" &&
                    in2_utesallosag_gy.Text != "" &&
                    in2_folykep_g_1.Text != "" &&
                    in2_folykep_g_2.Text != "" &&
                    in2_folykep_g_gy.Text != "" &&
                    in2_folykep_cm_1.Text != "" &&
                    in2_folykep_cm_2.Text != "" &&
                    in2_folykep_cm_gy.Text != "" &&
                    in2_toltoanyag.Text != "" &&
                    in2_toltoanyag_gy.Text != "" &&
                    in2_megjegyzes.Text != "")
                {
                    if ((in2_allapot.Text.ToLower() == "zárt" || in2_allapot.Text.ToLower() == "nyitott") && (in2_utomunk.Text.ToLower() == "igen" || in2_utomunk.Text.ToLower() == "nem"))
                    {
                        valid = true;
                    }
                }
            }


            return valid;
        }
        void inputFillUp()
        {
            string article = in1_cikkszam.Text;
            string charge = in1_charge.Text;
            string beerkdatum = in1_beerkdatum.Text;
            List<string> li = new List<string>();


            li = db.dbSearchRow(article, charge, beerkdatum);
            if (li.Count != 0)
            {
                in1_szallito.Text = li[0];
                in1_anyag_tipus.Text = li[1];
                in1_anyagnev.Text = li[2];
                in1_pc.Text = li[3];
                in1_utomunk.Text = li[4];
                in1_folyokep_c.Text = li[5];
                in1_utokalapacs_j.Text = li[6];
                in1_folyokep_suly.Text = li[7];
                in1_suruseg.Text = li[8];
                in1_szin.Text = li[9];

                in1_szakszig_min.Text = li[10];
                in1_szakszig_max.Text = li[11];
                in1_utesal_min.Text = li[12];
                in1_utesal_max.Text = li[13];
                in1_foly_g_min.Text = li[14];
                in1_foly_g_max.Text = li[15];
                in1_foly_cm_min.Text = li[16];
                in1_foly_cm_max.Text = li[17];
                in1_toltotart_min.Text = li[18];
                in1_toltotart_max.Text = li[19];

                in1_uto_date.Text = li[20];
                in1_kw.Text = li[21];
                in1_allapot.Text = li[22];
                in1_viztartalom.Text = li[23];
                in1_szakszig.Text = li[24];
                in1_szakszig_gy.Text = li[25];
                in1_utesallosag.Text = li[26];
                in1_utesallosag_gy.Text = li[27];
                in1_folykep_g_1.Text = li[28];
                in1_folykep_g_2.Text = "";

                in1_folykep_g_gy.Text = li[29];
                in1_folykep_cm_1.Text = li[30];
                in1_folykep_cm_2.Text = "";
                in1_folykep_cm_gy.Text = li[31];
                in1_toltoanyag.Text = li[32];
                in1_toltoanyag_gy.Text = li[33];
                in1_megjegyzes.Text = li[34];

                inputEvaulationColorer(in1_szakszig, null, li[10], li[11], li[24], "null");
                inputEvaulationColorer(in1_utesallosag, null, li[12], li[13], li[26], "null");
                inputEvaulationColorer(in1_folykep_g_1, null, li[14], li[15], li[28], "null");
                inputEvaulationColorer(in1_folykep_cm_1, null, li[16], li[17], li[30], "null");
                inputEvaulationColorer(in1_toltoanyag, null, li[18], li[19], li[32], "null");


                //inputEvaulationColorer(in1_szakszig, in1_szakszig_min.Text, in1_szakszig_max.Text, in1_szakszig.Text);
                //inputEvaulationColorer(in1_utesallosag, in1_utesal_min.Text, in1_utesal_max.Text, in1_utesallosag.Text);
                //inputEvaulationColorer(in1_folykep_g_1, in1_foly_g_min.Text, in1_foly_g_max.Text, in1_folykep_g_1.Text);
                //inputEvaulationColorer(in1_folykep_cm_1, in1_foly_cm_min.Text, in1_foly_cm_max.Text, in1_folykep_cm_1.Text);
                //inputEvaulationColorer(in1_toltoanyag,  in1_toltotart_min.Text,  in1_toltotart_max.Text, in1_toltoanyag.Text);
            }
        }
        void inputEvaulationColorer(TextBox input, TextBox input2, string minStr, string maxStr, string valueStr, string valueStr2)
        {
            if (valueStr2 == "null" || valueStr2 == "" || valueStr2 == "-")
            {
                valueStr2 = valueStr;
            }
            var bc = new BrushConverter();
            string color = "#FFFFFF";
            try
            {
                Double.TryParse(minStr.Replace(".", ","), out double min);
                Double.TryParse(maxStr.Replace(".", ","), out double max);
                Double.TryParse(valueStr.Replace(".", ","), out double value);
                Double.TryParse(valueStr2.Replace(".", ","), out double value2);

                double finalValue = (value + value2) / 2;
                if (finalValue > min && finalValue < max)
                {
                    color = "#ccffcc";
                }
                else if (finalValue == min || finalValue == max)
                {
                    color = "#fffabb";
                }
                else if (finalValue < min || finalValue > max)
                {
                    color = "#ffcccc";
                }
            }
            catch (Exception)
            {
                color = "#FFFFFF";
            }
            input.Background = (Brush)bc.ConvertFrom(color);
            if (input2 != null)
            {
                input2.Background = (Brush)bc.ConvertFrom(color);
            }
        }
        void inputCleaner(string tab)
        {
            if (tab == "new")
            {
                in2_beerkdatum.Text = null;
                in2_charge.Text = null;
                in2_szallito.Text = null;
                in2_anyag_tipus.Text = null;
                in2_anyagnev.Text = null;
                in2_pc.Text = null;
                in2_utomunk.Text = null;
                in2_folyokep_c.Text = null;
                in2_utokalapacs_j.Text = null;
                in2_folyokep_suly.Text = null;
                in2_suruseg.Text = null;
                in2_szin.Text = null;
                in2_szakszig_min.Text = null;
                in2_szakszig_max.Text = null;
                in2_utesal_min.Text = null;
                in2_utesal_max.Text = null;
                in2_foly_g_min.Text = null;
                in2_foly_g_max.Text = null;
                in2_foly_cm_min.Text = null;
                in2_foly_cm_max.Text = null;
                in2_toltotart_min.Text = null;
                in2_toltotart_max.Text = null;
                in2_uto_date.Text = null;
                in2_kw.Text = null;
                in2_allapot.Text = null;
                in2_viztartalom.Text = null;
                in2_szakszig.Text = null;
                in2_szakszig_gy.Text = null;
                in2_utesallosag.Text = null;
                in2_utesallosag_gy.Text = null;
                in2_folykep_g_1.Text = null;
                in2_folykep_g_2.Text = null;
                in2_folykep_g_gy.Text = null;
                in2_folykep_cm_1.Text = null;
                in2_folykep_cm_2.Text = null;
                in2_folykep_cm_gy.Text = null;
                in2_toltoanyag.Text = null;
                in2_toltoanyag_gy.Text = null;
                in2_megjegyzes.Text = null;
            }
            if (tab == "change")
            {
                in1_beerkdatum.Text = null;
                in1_charge.Text = null;
                in1_szallito.Text = null;
                in1_anyag_tipus.Text = null;
                in1_anyagnev.Text = null;
                in1_pc.Text = null;
                in1_utomunk.Text = null;
                in1_folyokep_c.Text = null;
                in1_utokalapacs_j.Text = null;
                in1_folyokep_suly.Text = null;
                in1_suruseg.Text = null;
                in1_szin.Text = null;
                in1_szakszig_min.Text = null;
                in1_szakszig_max.Text = null;
                in1_utesal_min.Text = null;
                in1_utesal_max.Text = null;
                in1_foly_g_min.Text = null;
                in1_foly_g_max.Text = null;
                in1_foly_cm_min.Text = null;
                in1_foly_cm_max.Text = null;
                in1_toltotart_min.Text = null;
                in1_toltotart_max.Text = null;
                in1_uto_date.Text = null;
                in1_kw.Text = null;
                in1_allapot.Text = null;
                in1_viztartalom.Text = null;
                in1_szakszig.Text = null;
                in1_szakszig_gy.Text = null;
                in1_utesallosag.Text = null;
                in1_utesallosag_gy.Text = null;
                in1_folykep_g_1.Text = null;
                in1_folykep_g_2.Text = null;
                in1_folykep_g_gy.Text = null;
                in1_folykep_cm_1.Text = null;
                in1_folykep_cm_2.Text = null;
                in1_folykep_cm_gy.Text = null;
                in1_toltoanyag.Text = null;
                in1_toltoanyag_gy.Text = null;
                in1_megjegyzes.Text = null;
            }
        }
        void cikkInputFillUp()
        {
            string article = in2_cikkszam.Text;
            List<string> li = new List<string>();

            li = db.dbSearchCikk(article);
            if (li.Count != 0)
            {
                in2_szallito.Text = li[1];
                in2_anyag_tipus.Text = li[3];
                in2_anyagnev.Text = li[2];
                in2_pc.Text = li[4];
                in2_utomunk.Text = li[5];
                in2_folyokep_c.Text = li[6];
                in2_utokalapacs_j.Text = li[7];
                in2_folyokep_suly.Text = li[8];
                in2_suruseg.Text = li[9];
                in2_szin.Text = li[10];
                in2_szakszig_min.Text = li[11];
                in2_szakszig_max.Text = li[12];
                in2_utesal_min.Text = li[13];
                in2_utesal_max.Text = li[14];
                in2_foly_g_min.Text = li[15];
                in2_foly_g_max.Text = li[16];
                in2_foly_cm_min.Text = li[17];
                in2_foly_cm_max.Text = li[18];
                in2_toltotart_min.Text = li[19];
                in2_toltotart_max.Text = li[20];

                in2_szallito.IsEnabled = false;
                in2_anyag_tipus.IsEnabled = false;
                in2_anyagnev.IsEnabled = false;
                in2_pc.IsEnabled = false;
                in2_utomunk.IsEnabled = false;
                in2_folyokep_c.IsEnabled = false;
                in2_utokalapacs_j.IsEnabled = false;
                in2_folyokep_suly.IsEnabled = false;
                in2_suruseg.IsEnabled = false;
                in2_szin.IsEnabled = false;
                in2_szakszig_min.IsEnabled = false;
                in2_szakszig_max.IsEnabled = false;
                in2_utesal_min.IsEnabled = false;
                in2_utesal_max.IsEnabled = false;
                in2_foly_g_min.IsEnabled = false;
                in2_foly_g_max.IsEnabled = false;
                in2_foly_cm_min.IsEnabled = false;
                in2_foly_cm_max.IsEnabled = false;
                in2_toltotart_min.IsEnabled = false;
                in2_toltotart_max.IsEnabled = false;
            }
            else
            {
                in2_szallito.Text = null;
                in2_anyag_tipus.Text = null;
                in2_anyagnev.Text = null;
                in2_pc.Text = null;
                in2_utomunk.Text = null;
                in2_folyokep_c.Text = null;
                in2_utokalapacs_j.Text = null;
                in2_folyokep_suly.Text = null;
                in2_suruseg.Text = null;
                in2_szin.Text = null;
                in2_szakszig_min.Text = null;
                in2_szakszig_max.Text = null;
                in2_utesal_min.Text = null;
                in2_utesal_max.Text = null;
                in2_foly_g_min.Text = null;
                in2_foly_g_max.Text = null;
                in2_foly_cm_min.Text = null;
                in2_foly_cm_max.Text = null;
                in2_toltotart_min.Text = null;
                in2_toltotart_max.Text = null;

                in2_szallito.IsEnabled = true;
                in2_anyag_tipus.IsEnabled = true;
                in2_anyagnev.IsEnabled = true;
                in2_pc.IsEnabled = true;
                in2_utomunk.IsEnabled = true;
                in2_folyokep_c.IsEnabled = true;
                in2_utokalapacs_j.IsEnabled = true;
                in2_folyokep_suly.IsEnabled = true;
                in2_suruseg.IsEnabled = true;
                in2_szin.IsEnabled = true;
                in2_szakszig_min.IsEnabled = true;
                in2_szakszig_max.IsEnabled = true;
                in2_utesal_min.IsEnabled = true;
                in2_utesal_max.IsEnabled = true;
                in2_foly_g_min.IsEnabled = true;
                in2_foly_g_max.IsEnabled = true;
                in2_foly_cm_min.IsEnabled = true;
                in2_foly_cm_max.IsEnabled = true;
                in2_toltotart_min.IsEnabled = true;
                in2_toltotart_max.IsEnabled = true;

            }


        }
        void ujCikkSave()
        {
            if (inputAllFilled_Bool("new") == true)
            {
                bool valid = false;
                double fg1, fg2, fcm1, fcm2;
                string fg = null, fcm = null, respond = "";
                try
                {

                    fg1 = Convert.ToDouble(in2_folykep_g_1.Text.Replace(".", ","));
                    fg2 = Convert.ToDouble(in2_folykep_g_2.Text.Replace(".", ","));
                    fcm1 = Convert.ToDouble(in2_folykep_cm_1.Text.Replace(".", ","));
                    fcm2 = Convert.ToDouble(in2_folykep_cm_2.Text.Replace(".", ","));
                    fg = Convert.ToString((fg1 + fg2) / 2);
                    fcm = Convert.ToString((fcm1 + fcm2) / 2);
                    valid = true;
                }
                catch (Exception)
                {
                    valid = false;
                }
                if (valid == true)
                {
                    List<string> lista = new List<string>();
                    lista.Add(in2_cikkszam.Text);
                    lista.Add(in2_szallito.Text);
                    lista.Add(in2_anyagnev.Text);
                    lista.Add(in2_anyag_tipus.Text);
                    lista.Add(in2_pc.Text);
                    lista.Add(in2_utomunk.Text);
                    lista.Add(in2_folyokep_c.Text);
                    lista.Add(in2_utokalapacs_j.Text);
                    lista.Add(in2_folyokep_suly.Text);
                    lista.Add(in2_suruseg.Text);
                    lista.Add(in2_szin.Text);
                    lista.Add(in2_szakszig_min.Text);
                    lista.Add(in2_szakszig_max.Text);
                    lista.Add(in2_utesal_min.Text);
                    lista.Add(in2_utesal_max.Text);
                    lista.Add(in2_foly_g_min.Text);
                    lista.Add(in2_foly_g_max.Text);
                    lista.Add(in2_foly_cm_min.Text);
                    lista.Add(in2_foly_cm_max.Text);
                    lista.Add(in2_toltotart_min.Text);
                    lista.Add(in2_toltotart_max.Text);
                    lista.Add(in2_charge.Text);
                    lista.Add(in2_beerkdatum.Text);
                    lista.Add(in2_uto_date.Text);
                    lista.Add(in2_kw.Text);
                    lista.Add(in2_allapot.Text);
                    lista.Add(in2_viztartalom.Text);
                    lista.Add(in2_szakszig.Text);
                    lista.Add(in2_szakszig_gy.Text);
                    lista.Add(in2_utesallosag.Text);
                    lista.Add(in2_utesallosag_gy.Text);
                    lista.Add(fg);
                    lista.Add(in2_folykep_g_gy.Text);
                    lista.Add(fcm);
                    lista.Add(in2_folykep_cm_gy.Text);
                    lista.Add(in2_toltoanyag.Text);
                    lista.Add(in2_toltoanyag_gy.Text);
                    lista.Add(in2_megjegyzes.Text);

                    respond = db.newRowWrite(lista, in2_cikkszam.Text, in2_charge.Text, in2_beerkdatum.Text);
                    db.szallitoValiderDb(in2_szallito.Text);

                    main_info_out.Text = respond;
                    pc.idozito(main_info_out);
                }
                else
                {
                    main_info_out.Text = db.langWordList(db.languageAsker())[67];
                    pc.idozito(main_info_out);
                }
            }
            else
            {
                main_info_out.Text = db.langWordList(db.languageAsker())[66];
                pc.idozito(main_info_out);
            }

        }
        void changeCikkSave()
        {
            bool valid = false;
            double fg1, fg2, fcm1, fcm2, fg, fcm;
            string fgStr = null, fcmStr = null;
            try
            {

                fg1 = Convert.ToDouble(in1_folykep_g_1.Text.Replace(".", ","));
                fg2 = Convert.ToDouble(in1_folykep_g_2.Text.Replace(".", ","));
                fcm1 = Convert.ToDouble(in1_folykep_cm_1.Text.Replace(".", ","));
                fcm2 = Convert.ToDouble(in1_folykep_cm_2.Text.Replace(".", ","));
                fg = (fg1 + fg2) / 2;
                fcm = (fcm1 + fcm2) / 2;
                fgStr = fg.ToString();
                fcmStr = fcm.ToString();
                valid = true;
            }
            catch (Exception)
            {
                valid = false;
            }
            if (valid == true)
            {
                List<string> lista = new List<string>();
                lista.Add(in1_cikkszam.Text);
                lista.Add(in1_szallito.Text);
                lista.Add(in1_anyagnev.Text);
                lista.Add(in1_anyag_tipus.Text);
                lista.Add(in1_pc.Text);
                lista.Add(in1_utomunk.Text);
                lista.Add(in1_folyokep_c.Text);
                lista.Add(in1_utokalapacs_j.Text);
                lista.Add(in1_folyokep_suly.Text);
                lista.Add(in1_suruseg.Text);
                lista.Add(in1_szin.Text);
                lista.Add(in1_szakszig_min.Text);
                lista.Add(in1_szakszig_max.Text);
                lista.Add(in1_utesal_min.Text);
                lista.Add(in1_utesal_max.Text);
                lista.Add(in1_foly_g_min.Text);
                lista.Add(in1_foly_g_max.Text);
                lista.Add(in1_foly_cm_min.Text);
                lista.Add(in1_foly_cm_max.Text);
                lista.Add(in1_toltotart_min.Text);
                lista.Add(in1_toltotart_max.Text);
                lista.Add(in1_charge.Text);
                lista.Add(in1_beerkdatum.Text);
                lista.Add(in1_uto_date.Text);
                lista.Add(in1_kw.Text);
                lista.Add(in1_allapot.Text);
                lista.Add(in1_viztartalom.Text);
                lista.Add(in1_szakszig.Text);
                lista.Add(in1_szakszig_gy.Text);
                lista.Add(in1_utesallosag.Text);
                lista.Add(in1_utesallosag_gy.Text);
                lista.Add(fgStr);
                lista.Add(in1_folykep_g_gy.Text);
                lista.Add(fcmStr);
                lista.Add(in1_folykep_cm_gy.Text);
                lista.Add(in1_toltoanyag.Text);
                lista.Add(in1_toltoanyag_gy.Text);
                lista.Add(in1_megjegyzes.Text);

                db.changeRowWrite(lista, in1_cikkszam.Text, in1_charge.Text, in1_beerkdatum.Text);
                db.szallitoValiderDb(in1_szallito.Text);
                main_info_out.Text = db.langWordList(db.languageAsker())[64];
            }
            else
            {
                main_info_out.Text = db.langWordList(db.languageAsker())[65];
            }
        }




        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.,-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberValidationTextBox2(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void mainpageBackBtn_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Remove(this);
            mainGrid.Children.Add(welcomepage = new welcomepage(mainGrid));
        }

        private void in1_cikkszam_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputFillUp();
        }

        private void in1_charge_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputFillUp();
        }

        private void in1_beerkdatum_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputFillUp();
        }

        private void nyitott_cbox_Checked(object sender, RoutedEventArgs e)
        {
            searched_cikk_listView_loader();
        }

        private void lezart_cbox_Checked(object sender, RoutedEventArgs e)
        {
            searched_cikk_listView_loader();
        }
        private void listView_4_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            ListViewLoaders.MeasureDataSource data = (sender as ListView).SelectedItem as ListViewLoaders.MeasureDataSource;
            try
            {
                in1_cikkszam.Text = data.col1;
                in1_charge.Text = data.col2;
                in1_szallito.Text = data.col3;
                in1_beerkdatum.Text = data.col7;
                in2_cikkszam.Text = data.col1;
            }
            catch (Exception)
            {
                in1_cikkszam.Text = "";
                in1_charge.Text = "";
                in1_szallito.Text = "";
                in1_beerkdatum.Text = "";
                in2_cikkszam.Text = "";
            }


        }

        private void mainpageToAdmin_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Remove(this);
            mainGrid.Children.Add(admin = new admin(mainGrid));
        }

        private void in2_cikkszam_TextChanged(object sender, TextChangedEventArgs e)
        {
            cikkInputFillUp();
        }

        private void save_btn3_Click(object sender, RoutedEventArgs e)
        {
            string cikk = in1_cikkszam.Text;
            string charge = in1_charge.Text;
            string beerk = in1_beerkdatum.Text;
            string szallito = in1_szallito.Text;
            string cikk2 = in2_cikkszam.Text;
            szallitokListazo();
            if (inputAllFilled_Bool("new") == true)
            {
                ujCikkSave();
                inputCleaner("new");
                szallitokListazo();
                searched_cikk_listView_loader();
                in2_cikkszam.Text = cikk2;
                in1_cikkszam.Text = cikk;
                in1_charge.Text = charge;
                in1_beerkdatum.Text = beerk;
                in1_szallito.Text = szallito;
                db.UserActivityLogger(session.UserData[3], "Új Felvitel", cikk, charge, beerk, date.ToString());
            }
            else
            {
                main_info_out.Text = db.langWordList(db.languageAsker())[63];
                pc.idozito(main_info_out);
            }
        }
        private void input_cikkszam_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Thread runThread = new Thread(() => searched_cikk_listView_loader());
            //runThread.Start();
            searched_cikk_listView_loader();
        }
        private void input_charge_TextChanged(object sender, TextChangedEventArgs e)
        {
            searched_cikk_listView_loader();
        }
        private void input_beerk_datum_TextChanged(object sender, TextChangedEventArgs e)
        {
            searched_cikk_listView_loader();
        }
        private void input_anyagnev_TextChanged(object sender, TextChangedEventArgs e)
        {
            searched_cikk_listView_loader();
        }
        private void input_szallito_TextChanged(object sender, TextChangedEventArgs e)
        {
            searched_cikk_listView_loader();
        }
        private void save_btn2_Click(object sender, RoutedEventArgs e)
        {
            string cikk = in1_cikkszam.Text;
            string charge = in1_charge.Text;
            string beerk = in1_beerkdatum.Text;
            string szallito = in1_szallito.Text;
            if (inputAllFilled_Bool("change") == true)
            {
                changeCikkSave();
                inputFillUp();
                searched_cikk_listView_loader();

                in1_cikkszam.Text = cikk;
                in1_charge.Text = charge;
                in1_beerkdatum.Text = beerk;
                in1_szallito.Text = szallito;
                db.UserActivityLogger(session.UserData[3], "Módosítás", cikk, charge, beerk, date.ToString());
            }
            else
            {
                main_info_out.Text = db.langWordList(db.languageAsker())[63];
            }
        }

        private void comboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string itemText = null;
            try
            {
                if (in2_szallito.IsEnabled == true)
                {
                    itemText = comboBox3.SelectedItem.ToString();
                    in2_szallito.Text = itemText;
                }
            }
            catch { }
            itemText = null;
        }

        private void in1_szakszig_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputEvaulationColorer(in1_szakszig, null, in1_szakszig_min.Text, in1_szakszig_max.Text, in1_szakszig.Text, "null");
        }

        private void in1_utesallosag_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputEvaulationColorer(in1_utesallosag, null, in1_utesal_min.Text, in1_utesal_max.Text, in1_utesallosag.Text, "null");
        }

        private void in1_folykep_g_1_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputEvaulationColorer(in1_folykep_g_1, in1_folykep_g_2, in1_foly_g_min.Text, in1_foly_g_max.Text, in1_folykep_g_1.Text, in1_folykep_g_2.Text);
        }

        private void in1_folykep_cm_1_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputEvaulationColorer(in1_folykep_cm_1, in1_folykep_cm_2, in1_foly_cm_min.Text, in1_foly_cm_max.Text, in1_folykep_cm_1.Text, in1_folykep_cm_2.Text);
        }

        private void in1_toltoanyag_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputEvaulationColorer(in1_toltoanyag, null, in1_toltotart_min.Text, in1_toltotart_max.Text, in1_toltoanyag.Text, "null");
        }

        private void in1_folykep_g_2_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputEvaulationColorer(in1_folykep_g_2, in1_folykep_g_1, in1_foly_g_min.Text, in1_foly_g_max.Text, in1_folykep_g_1.Text, in1_folykep_g_2.Text);
        }

        private void in1_folykep_cm_2_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputEvaulationColorer(in1_folykep_cm_2, in1_folykep_cm_1, in1_foly_cm_min.Text, in1_foly_cm_max.Text, in1_folykep_cm_1.Text, in1_folykep_cm_2.Text);
        }

        private void tab3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            szallitokListazo();
        }

        private void main_x_Click(object sender, RoutedEventArgs e)
        {
            pc.exitApplication();
        }
        private void main_minimize_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.WindowState = WindowState.Minimized;
        }

        private void btn_hu_Click(object sender, RoutedEventArgs e)
        {
            db.langWrite(1);
            legendChanger_Mainpage(db.langWordList(db.languageAsker()));
            searched_cikk_listView_loader();
        }

        private void btn_de_Click(object sender, RoutedEventArgs e)
        {
            db.langWrite(2);
            legendChanger_Mainpage(db.langWordList(db.languageAsker()));
            searched_cikk_listView_loader();
        }

        private void btn_en_Click(object sender, RoutedEventArgs e)
        {
            db.langWrite(3);
            legendChanger_Mainpage(db.langWordList(db.languageAsker()));
            searched_cikk_listView_loader();
        }
    }
}