using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Class_Schedule.Database;
using Class_Schedule.Control;
using System.Windows.Controls;
using System.Windows.Data;

namespace Class_Schedule.Control
{
    class ListViewLoaders
    {
        dbEntities db = new dbEntities();
        Session sess = new Session();

        public class MeasureDataSource
        {
            public string col1 { get; set; }
            public string col2 { get; set; }
            public string col3 { get; set; }
            public string col4 { get; set; }
            public string col5 { get; set; }
            public string col6 { get; set; }
            public string col7 { get; set; }
        }
        public class ActivityItems
        {
            public string col1 { get; set; }
            public string col2 { get; set; }
            public string col3 { get; set; }
            public string col4 { get; set; }
            public string col5 { get; set; }
            public string col6 { get; set; }
            public string col7 { get; set; }
        }
        public class UserItems
        {
            public string col1 { get; set; }
            public string col2 { get; set; }
            public string col3 { get; set; }
            public string col4 { get; set; }
            public string col5 { get; set; }
            public string col6 { get; set; }
            public string col7 { get; set; }
        }
        public class AdminMeasureItems
        {
            public string col1 { get; set; }
            public string col2 { get; set; }
            public string col3 { get; set; }
            public string col4 { get; set; }
        }
        public class SzallitoItems
        {
            public string col1 { get; set; }
        }
        public void listViewLoader(object[,] li, ListView listView)
        {
            try
            {
                List<MeasureDataSource> items = new List<MeasureDataSource>();
                int i = 0;
                int hossz = li.Length / 7;
                while (i != hossz)
                {
                    items.Add(new MeasureDataSource() { col1 = li[i, 0].ToString(), col2 = li[i, 1].ToString(), col3 = li[i, 2].ToString(), col4 = li[i, 3].ToString(), col5 = li[i, 4].ToString(), col6 = li[i, 5].ToString(), col7 = li[i, 6].ToString(), });
                    i++;
                }
                listView.ItemsSource = items;
            }
            catch (Exception)
            {
            }
        }
        public void szallitokListLoader(ListView listView)
        {
            List<object> szallitokLi = new List<object>();
            try
            {
                szallitokLi = db.szallitoListaStr();
                List<SzallitoItems> items = new List<SzallitoItems>();
                int i = 0;
                while (i < szallitokLi.Count)
                {
                    items.Add(new SzallitoItems() { col1 = szallitokLi[i].ToString() });
                    i++;
                }
                listView.ItemsSource = items;
            }
            catch (Exception)
            {

            }

        }
        public void Activity_ListLoader(object[,] li, ListView listView)
        {

            List<string> szolista = db.langWordList(db.languageAsker());
                int i = 0;

            List<ActivityItems> items = new List<ActivityItems>();
            int hossz = li.Length / 7;
                while (i != hossz)
                {
                string allapot = "";
                if (li[i, 2].ToString() == "1")
                {
                    allapot = szolista[5];
                }
                else if (li[i, 2].ToString() == "0")
                {
                    allapot = szolista[4];
                }
                items.Add(new ActivityItems() { col1 = li[i, 0].ToString(), col2 = li[i, 1].ToString(), col3 = allapot, col4 = li[i, 3].ToString(), col5 = li[i, 4].ToString(), col6 = li[i, 5].ToString(), col7 = li[i, 6].ToString() });                
                i++;
            }
                listView.ItemsSource = items;

        }
        public void userListLoader(ListView listview)
        {
            try
            {
                List<UserItems> items = new List<UserItems>();
                object[,] li;
                li = db.DbSelectUsers();
                int i = 0;
                while (li[i, 1] != null)
                {
                    if (li[i, 0].ToString() != "admin")
                    {
                        string valid;
                        string admin;
                        if (li[i, 5].ToString() == "1") valid = "igen";
                        else valid = "-";
                        if (li[i, 3].ToString() == "1") admin = "igen";
                        else admin = "-";
                        items.Add(new UserItems() { col1 = li[i, 0].ToString(), col2 = li[i, 1].ToString(), col3 = li[i, 2].ToString(), col4 = admin, col5 = li[i, 4].ToString(), col6 = valid, col7 = li[i, 6].ToString() });
                    }
                        i++;
                }
                listview.ItemsSource = items;
            }
            catch (Exception)
            {
            }
        }
        public void adminMeasureLoader(object[,] li, ListView listView)
        {
            List<AdminMeasureItems> items = new List<AdminMeasureItems>();
            
                int i = 0;
                int hossz = li.Length / 4;
                while (i != hossz)
                {
                    items.Add(new AdminMeasureItems() { col1 = li[i, 0].ToString(), col2 = li[i, 1].ToString(), col3 = li[i, 2].ToString(), col4 = li[i, 3].ToString() });
                    i++;
            }
            listView.ItemsSource = items;
        
        }
    }
}
