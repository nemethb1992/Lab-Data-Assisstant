using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Class_Schedule.Control;
using System.Linq;
using System.Text;
using System.Windows;

namespace Class_Schedule.Database
{

    public class dbEntities
    {
        static Session sess = new Session();
        public void dballapotchanger()
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            var command = conn.CreateCommand();
            conn.Open();
            
            command.CommandText = "CREATE TABLE IF NOT EXISTS `langMemory` (`lastLang`	INTEGER DEFAULT 1);";
            command.ExecuteNonQuery();
            command.CommandText = "DELETE FROM `langMemory`";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO  `langMemory` (lastLang) VALUES (1);";
            command.ExecuteNonQuery();
            command.CommandText = "DELETE FROM userActivity;";
            command.ExecuteNonQuery();
            try
            {
                command.CommandText = "ALTER TABLE users ADD language INTEGER DEFAULT 1";
                command.ExecuteNonQuery();
                command.CommandText = "ALTER TABLE charge MODIFY COLUMN allapot INTEGER DEFAULT 0";
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            command.CommandText = "UPDATE charge SET allapot = 0 WHERE allapot = 'nyitott' OR allapot = 'Nyitott'";
            command.ExecuteNonQuery();
            command.CommandText = "UPDATE charge SET allapot = 1 WHERE allapot = 'zárt' OR allapot = 'Zárt'";
            command.ExecuteNonQuery();
            command.CommandText = "UPDATE userActivity SET allapot = 0 WHERE allapot = 'nyitott' OR allapot = 'Nyitott'";
            command.ExecuteNonQuery();
            command.CommandText = "UPDATE userActivity SET allapot = 1 WHERE allapot = 'zárt' OR allapot = 'Zárt'";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void lastDbOpenUrl()
        {
            //A saját adatbázisból kinyerni a legutóbb megnyitott db elérési útját
            SQLiteConnection conn = new SQLiteConnection("Data Source=InnerData.db");
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS dburl (id INTEGER, databaseUrl TEXT)";
            command.ExecuteNonQuery();
            command.CommandText = "SELECT * FROM dburl WHERE id=1";
            SQLiteDataReader sdr = command.ExecuteReader();
            string dbdata = null;
            while (sdr.Read())
            {
                dbdata = sdr.GetValue(1).ToString();
            }
            sess.databaseUrl = dbdata;
            sdr.Close();
            conn.Close();
        }
        public void langWrite(int lang)
        {

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            var command = conn.CreateCommand();
            conn.Open();
            try
            {
                string user = sess.UserData[1];
                command.CommandText = "UPDATE users SET language=" + lang + " WHERE username='" + user + "'";
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }

            command.CommandText = "DELETE FROM `langMemory`";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO langMemory (lastLang) VALUES("+lang+")";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public int languageAsker()
        {
            string query = "SELECT lastLang FROM langMemory";
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            var command = conn.CreateCommand();
            conn.Open();

            command.CommandText = "CREATE TABLE IF NOT EXISTS `langMemory` (`lastLang`	INTEGER DEFAULT 1);";
            command.ExecuteNonQuery();
            try
            {
                string user = sess.UserData[1];
                query = "SELECT language FROM 'users' WHERE username LIKE '%" + user + "%'";
            }
            catch (Exception)
            {

            }
            int dbdata = 1;
            try
            {
                command.CommandText = query;
                SQLiteDataReader sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    dbdata = sdr.GetInt32(0);
                }
                if (dbdata != 1 && dbdata != 2 && dbdata != 3)
                {
                    dbdata = 1;
                }
                sdr.Close();
            }
            catch (Exception)
            {
            }

            conn.Close();
            return dbdata;
        }
        public void lastDbOpenUrlUpdater(string newUrl)
        {
            //Az új elérési utat felülírja a saját adatbázisában

            SQLiteConnection conn = new SQLiteConnection("Data Source=InnerData.db");
            string dbdata = null;
            conn.Open();
            var command = conn.CreateCommand();
            try
            {
                command.CommandText = "SELECT * FROM dburl WHERE id=1";
                SQLiteDataReader sdr = command.ExecuteReader();
                dbdata = null;
                while (sdr.Read())
                {
                    dbdata = sdr.GetValue(1).ToString();
                }
                sdr.Close();
            }
            catch (Exception)
            {
                dbdata = null;
            }
            if (dbdata != null)
            {
                command.CommandText = "UPDATE dburl SET databaseUrl='Data Source=" + newUrl + "' WHERE id=1";
                command.ExecuteNonQuery();
            }
            else
            {
                command.CommandText = "INSERT INTO dburl (id, databaseUrl) VALUES (1,'Data Source=" + newUrl + "')";
                command.ExecuteNonQuery();
            }
            conn.Close();
        }
        public bool dbConncetionValider()
        {
            //Ellenőrzi van-e kapcsolat adatbázissal
            bool valid = false;
            try
            {
                SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM cikk";
                command.ExecuteNonQuery();
                valid = true;
                conn.Close();

            }
            catch (Exception)
            {
                valid = false;
                dbUrlChanger();
            }
            return valid;
        }
        public void dbUrlChanger()
        {
            //Fájl megnyitó panelt dob fel amelynek az elérési útját nyerjük ki majd ír a belső adatbázisba
            try
            {
                MessageBox.Show("You have to give the location of the database.\n\n(L.D.A Database)", "Lab Data Assistant", MessageBoxButton.OK, MessageBoxImage.Information);
                string newUrl = "";
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true)
                {
                    newUrl = ofd.FileName.Replace(@"\", "/");
                }
                sess.databaseUrl = "Data Source=" + newUrl;
                lastDbOpenUrlUpdater(newUrl);
            }
            catch (Exception)
            {
            }

        }
        bool meresChecker(string cikkVal, string chargeVal, string beerkVal)
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            bool valider = false;
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT count(charge) FROM cikk INNER JOIN charge ON cikk.cikkszam=charge.charge_cikkszam WHERE cikk.cikkszam='" + cikkVal + "' AND charge.charge_cikkszam='"+cikkVal+"' AND charge.charge='"+chargeVal+ "' AND charge.beerk_datum='" + beerkVal + "'";
            SQLiteDataReader sdr = command.ExecuteReader();
            int dbdata = 0;
            while (sdr.Read())
            {
                dbdata = sdr.GetInt32(0);
            }
            sdr.Close();
            if (dbdata == 0)
            {
                valider = true;
            }
            conn.Close();
            return valider;
        }
        bool elemChecker(string ertek, string oszlop, string tabla)
        {
            //ellenőrzi hogy létezik-e az adott cikk vagy charge

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            bool valider = false;
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT count(charge) FROM cikk INNER JOIN charge ON cikk.cikkszam=charge.charge_cikkszam WHERE "+tabla+"." + oszlop+"='" + ertek + "'";
            SQLiteDataReader sdr = command.ExecuteReader();
            int dbdata = 0;
            while (sdr.Read())
            {
                dbdata = sdr.GetInt32(0);
            }
            sdr.Close();
            if (dbdata == 0)
            {
                valider = true;
            }
            conn.Close();
            return valider;
        }
        bool chargeChecker(string cikk, string charge, string beerk)
        {
            //ellenőrzi hogy létezik-e az adott cikk vagy charge

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            bool valider = false;
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT count(charge) FROM charge WHERE charge_cikkszam='"+cikk+"' AND charge='"+charge+"' AND beerk_datum='"+beerk+"' ";
            SQLiteDataReader sdr = command.ExecuteReader();
            int dbdata = 0;
            while (sdr.Read())
            {
                dbdata = sdr.GetInt32(0);
            }
            sdr.Close();
            if (dbdata == 0)
            {
                valider = true;
            }
            conn.Close();
            return valider;
        }
        public void dbDefault(string turn)
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();
            if (turn == "cikk")
            {
                command.CommandText = "DROP TABLE IF EXISTS cikk;";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS cikk ( `id` INTEGER PRIMARY KEY AUTOINCREMENT, `cikkszam` TEXT NOT NULL, `szallito` TEXT NOT NULL, `anyag_nev` TEXT NOT NULL, `anyag_tipus` TEXT NOT NULL, `profit_center` TEXT NOT NULL, `utomun_metszve` TEXT NOT NULL, `folyokep_homerseklet` TEXT NOT NULL, `utokalapacs_meret_j` TEXT NOT NULL, `folyokep_terheles_kg` TEXT NOT NULL, `suruseg` TEXT NOT NULL, `szin` TEXT NOT NULL, `szakszig_min` TEXT NOT NULL, `szakszig_max` TEXT NOT NULL, `utesallosag_min` TEXT NOT NULL, `utesallosag_max` TEXT NOT NULL, `folyokep_min_g` TEXT NOT NULL, `folyokep_max_g` TEXT NOT NULL, `folyokep_min_cm` TEXT NOT NULL, `folyokep_max_cm` TEXT NOT NULL, `toltoanyag_min` TEXT NOT NULL, `toltoanyag_max` TEXT NOT NULL )";     
                command.ExecuteNonQuery();
            }

            if (turn == "charge")
            {
                command.CommandText = "DROP TABLE IF EXISTS charge;";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS charge ( `charge_id` INTEGER PRIMARY KEY AUTOINCREMENT, `charge_cikkszam` TEXT NOT NULL, `charge` TEXT NOT NULL, `beerk_datum` TEXT NOT NULL, `ut_meres_datum` TEXT NOT NULL, `kw` TEXT NOT NULL, `allapot` TEXT NOT NULL, `viztartalom` TEXT, `szakszig` TEXT, `szakszig_gy` TEXT, `utesallosag` TEXT, `utesallosag_gy` TEXT, `folyokep_g` NUMERIC, `folyokep_g_gy` TEXT, `folyokep_cm` NUMERIC, `folyokep_cm_gy` TEXT, `toltoanyag` TEXT, `toltoanyag_gy` TEXT, `megjegyzes` TEXT )";
                command.ExecuteNonQuery();
            conn.Close();
            }
        }
        public void cikkWriteFromExcel(List<object> lista)
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "INSERT INTO 'cikk' " +
                 "('cikkszam','szallito','anyag_nev','anyag_tipus','profit_center','utomun_metszve','folyokep_homerseklet','utokalapacs_meret_j','folyokep_terheles_kg','suruseg','szin','szakszig_min','szakszig_max','utesallosag_min','utesallosag_max','folyokep_min_g','folyokep_max_g','folyokep_min_cm','folyokep_max_cm','toltoanyag_min','toltoanyag_max')  " +
                 "VALUES ('" +
                 lista[0] + "','" +
                 lista[1] + "','" +
                 lista[2] + "','" +
                 lista[3] + "','" +
                 lista[4] + "','" +
                 lista[5] + "','" +
                 lista[6] + "','" +
                 lista[7] + "','" +
                 lista[8] + "','" +
                 lista[9] + "','" +
                 lista[10] + "','" +
                 lista[11] + "','" +
                 lista[12] + "','" +
                 lista[13] + "','" +
                 lista[14] + "','" +
                 lista[15] + "','" +
                 lista[16] + "','" +
                 lista[17] + "','" +
                 lista[18] + "','" +
                 lista[19] + "','" +
                 lista[20] + "')";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void chargeWriteFromExcel(List<object> lista)
            {
                SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
                conn.Open();
                var command = conn.CreateCommand();
            command.CommandText = "INSERT INTO 'charge' " +
                  "('charge_cikkszam', 'charge', 'beerk_datum', 'ut_meres_datum', 'kw', 'allapot', 'viztartalom', 'szakszig', 'szakszig_gy', 'utesallosag', 'utesallosag_gy', folyokep_g, 'folyokep_g_gy', folyokep_cm, 'folyokep_cm_gy', 'toltoanyag', 'toltoanyag_gy', 'megjegyzes') " +
                  "VALUES ('" +
                  lista[0] + "','" +
                  lista[1] + "','" +
                  lista[2] + "','" +
                  lista[3] + "','" +
                  lista[4] + "'," +
                  lista[5] + ",'" +
                  lista[6] + "','" +
                  lista[7] + "','" +
                  lista[8] + "','" +
                  lista[9] + "','" +
                  lista[10] + "','" +
                  lista[11] + "','" +
                  lista[12] + "','" +
                  lista[13] + "','" +
                  lista[14] + "','" +
                  lista[15] + "','" +
                  lista[16] + "','" +
                  lista[17] + "')";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public string newRowWrite(List<object> lista, string cikkszam, string charge, string beerk)
        {
            //Új cikk bevitelénél használt író függvény

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            string respond = "";
            bool cikkBool = elemChecker(cikkszam, "cikkszam", "cikk");
            bool chargeBool = chargeChecker(cikkszam, charge, beerk);
            bool cikkChargePair = meresChecker(cikkszam, charge, beerk);
            conn.Open();
            var command = conn.CreateCommand();
            if (cikkChargePair == true)
            {
                if (cikkBool == true)
                {
                    command.CommandText = "INSERT INTO 'cikk' " +
                        "('cikkszam','szallito','anyag_nev','anyag_tipus','profit_center','utomun_metszve','folyokep_homerseklet','utokalapacs_meret_j','folyokep_terheles_kg','suruseg','szin','szakszig_min','szakszig_max','utesallosag_min','utesallosag_max','folyokep_min_g','folyokep_max_g','folyokep_min_cm','folyokep_max_cm','toltoanyag_min','toltoanyag_max')  " +
                        "VALUES ('" +
                        lista[0] + "','" +
                        lista[1] + "','" +
                        lista[2] + "','" +
                        lista[3] + "','" +
                        lista[4] + "','" +
                        lista[5] + "','" +
                        lista[6] + "','" +
                        lista[7] + "','" +
                        lista[8] + "','" +
                        lista[9] + "','" +
                        lista[10] + "','" +
                        lista[11] + "','" +
                        lista[12] + "','" +
                        lista[13] + "','" +
                        lista[14] + "','" +
                        lista[15] + "','" +
                        lista[16] + "','" +
                        lista[17] + "','" +
                        lista[18] + "','" +
                        lista[19] + "','" +
                        lista[20] + "')";
                    command.ExecuteNonQuery();
                }

                if (chargeBool == true)
                {
                    command.CommandText = "INSERT INTO 'charge' " +
                        "('charge_cikkszam', 'charge', 'beerk_datum', 'ut_meres_datum', 'kw', 'allapot', 'viztartalom', 'szakszig', 'szakszig_gy', 'utesallosag', 'utesallosag_gy', folyokep_g, 'folyokep_g_gy', folyokep_cm, 'folyokep_cm_gy', 'toltoanyag', 'toltoanyag_gy', 'megjegyzes') " +
                        "VALUES ('" +
                        lista[0] + "','" +
                        lista[21] + "','" +
                        lista[22] + "','" +
                        lista[23] + "','" +
                        lista[24] + "'," +
                        lista[25] + ",'" +
                        lista[26] + "','" +
                        lista[27] + "','" +
                        lista[28] + "','" +
                        lista[29] + "','" +
                        lista[30] + "','" +
                        lista[31] + "','" +
                        lista[32] + "','" +
                        lista[33] + "','" +
                        lista[34] + "','" +
                        lista[35] + "','" +
                        lista[36] + "','" +
                        lista[37] + "')";
                    command.ExecuteNonQuery();
                }
                conn.Close();
                if ((chargeBool == true && cikkBool == true) || chargeBool == true && cikkBool == false)
                {
                    respond = "Sikeres mentés.";
                }
                if (chargeBool == false && cikkBool == true)
                {
                    respond = "Cikkszám mentve. Charge már létezik.";
                }
                if (chargeBool == false && cikkBool == false)
                {
                    respond = "Sikertelen mentés.";
                }
            }
            else
            {
                respond = "Ez a Cikkszám / Charge párosítás létezik.";
            }
                
            return respond;

        }
        public void changeRowWrite(List<object> lista, string cikkszam, string charge,string beerkdatum)
        {
            // Meglévő cikknél használt módosító függvény

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();

            command.CommandText = "UPDATE cikk SET "+
            "'cikkszam'='" + lista[0] + "'" +
            ", 'szallito'='" + lista[1] + "'" +
            ", 'anyag_nev'='" + lista[2] + "'" +
            ", 'anyag_tipus'='" + lista[3] + "'" +
            ", 'profit_center'='" + lista[4] + "'" +
            ", 'utomun_metszve'='" + lista[5] + "'" +
            ", 'folyokep_homerseklet'='" + lista[6] + "'" +
            ", 'utokalapacs_meret_j'='" + lista[7] + "'" +
            ", 'folyokep_terheles_kg'='" + lista[8] + "'" +
            ", 'suruseg'='" + lista[9] + "'" +
            ", 'szin'='" + lista[10] + "'" +
            ", 'szakszig_min'='" + lista[11] + "'" +
            ", 'szakszig_max'='" + lista[12] + "'" +
            ", 'utesallosag_min'='" + lista[13] + "'" +
            ", 'utesallosag_max'='" + lista[14] + "'" +
            ", 'folyokep_min_g'='" + lista[15] + "'" +
            ", 'folyokep_max_g'='" + lista[16] + "'" +
            ", 'folyokep_min_cm'='" + lista[17] + "'" +
            ", 'folyokep_max_cm'='" + lista[18] + "'" +
            ", 'toltoanyag_min'='" + lista[19] + "'" +
            ", 'toltoanyag_max'='" + lista[20] + "' WHERE cikkszam = '" + cikkszam + "'";
            command.ExecuteNonQuery();
            command.CommandText = "UPDATE charge SET " +
            " 'charge_cikkszam'='" + lista[0] + "'" +
            ", 'charge'='" + lista[21] + "'" +
            ", 'beerk_datum'='" + lista[22] + "'" +
            ", 'ut_meres_datum'='" + lista[23] + "'" +
            ", 'kw'='" + lista[24] + "'" +
            ", 'allapot'='" + lista[25] + "'" +
            ", 'viztartalom'='" + lista[26] + "'" +
            ", 'szakszig'='" + lista[27] + "'" +
            ", 'szakszig_gy'='" + lista[28] + "'" +
            ", 'utesallosag'='" + lista[29] + "'" +
            ", 'utesallosag_gy'='" + lista[30] + "'" +
            ", 'folyokep_g'='" + lista[31] + "'" +
            ", 'folyokep_g_gy'='" + lista[32] + "'" +
            ", 'folyokep_cm'='" + lista[33] + "'" +
            ", 'folyokep_cm_gy'='" + lista[34] + "'" +
            ", 'toltoanyag'='" + lista[35] + "'" +
            ", 'toltoanyag_gy'='" + lista[36] + "'" +
            ", 'megjegyzes'='" + lista[37] + "'  WHERE charge_cikkszam='" + cikkszam + "' AND charge='" + charge + "' AND beerk_datum='" + beerkdatum + "'";
            command.ExecuteNonQuery();

            conn.Close();

        }
        public List<string> dbSearchRow(string article, string charge, string beerkdatum)
        {
            // 3 feltétel szerinti sor keresés (Adott cikk megtalálására)
            // Listát ad vissza a cikk minden adatával

            List<string> lista = new List<string>();
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT " +
                "szallito,anyag_tipus,anyag_nev,profit_center,utomun_metszve,folyokep_homerseklet,utokalapacs_meret_j,folyokep_terheles_kg,suruseg,szin," +
                "szakszig_min,szakszig_max,utesallosag_min,utesallosag_max,folyokep_min_g,folyokep_max_g,folyokep_min_cm,folyokep_max_cm,toltoanyag_min,toltoanyag_max," +
                "ut_meres_datum,kw,allapot,viztartalom,szakszig,szakszig_gy,utesallosag,utesallosag_gy,folyokep_g,folyokep_g_gy," +
                "folyokep_cm,folyokep_cm_gy,toltoanyag,toltoanyag_gy,megjegyzes" +
                " FROM cikk INNER JOIN charge ON cikk.cikkszam=charge.charge_cikkszam WHERE cikk.cikkszam='" + article+"' AND charge.charge='"+charge+ "' AND charge.beerk_datum='" + beerkdatum + "'";
            SQLiteDataReader sdr = command.ExecuteReader();
            
            string dbdata = null;
            while (sdr.Read())
            {
                for (int i = 0; i < 35; i++)
                {
                    dbdata = sdr.GetValue(i).ToString();
                    lista.Add(dbdata.ToString());
                }
            }


            sdr.Close();
            conn.Close();
            return lista;
        }
        public List<string> dbSearchCikk(string article)
        {
            // ÚJ cikk felvitelénél használt függvény
            // Adott cikket keres ki majd annak értékeit adja vissza egy listában

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            List<string> lista = new List<string>();
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM cikk  WHERE cikk.cikkszam='" + article + "'";
            SQLiteDataReader sdr = command.ExecuteReader();
            string dbdata = null;
            while (sdr.Read())
            {
                for (int i = 1; i < 22; i++)
                {
                    dbdata = sdr.GetValue(i).ToString();
                    lista.Add(dbdata);
                }
            }



            sdr.Close();
            conn.Close();
            return lista;
        }
        public List<object> szallitoListaStr()
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            List<object> lista = new List<object>();
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM beszallitok ORDER BY nev ASC";
            SQLiteDataReader sdr = command.ExecuteReader();
            while (sdr.Read())
            {
                    lista.Add(sdr.GetValue(1).ToString());
            }
            sdr.Close();
            conn.Close();
            return lista;
        }
        public bool szallitoValiderDb(string szallito)
        {
            bool valid = false;
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM beszallitok WHERE nev='"+szallito+"'";
            SQLiteDataReader sdr = command.ExecuteReader();
            string dbdata = null;
            while (sdr.Read())
            {
                dbdata = sdr.GetValue(1).ToString();
            }
            sdr.Close();
            if (dbdata == null)
            {
                valid = true;
            }
            sdr.Close();
            if (valid == true)
            {
                if (szallito != "" && szallito != null)
                {
                    command.CommandText = "INSERT INTO beszallitok (nev) VALUES('" + szallito + "')";
                    command.ExecuteNonQuery();
                }
            }




            conn.Close();
            return valid ;
        }
        public bool szallitoDeleteDb(string szallito)
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            bool valid = false;
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM beszallitok WHERE nev='" + szallito + "'";
            SQLiteDataReader sdr = command.ExecuteReader();
            string dbdata = null;
            while (sdr.Read())
            {
                dbdata = sdr.GetValue(1).ToString();
            }
            sdr.Close();
            if (dbdata != null)
            {
                valid = true;
            }
            sdr.Close();
            if (valid == true)
            {
                if (szallito != "" && szallito != null)
                {
                    command.CommandText = "DELETE FROM beszallitok WHERE nev='"+szallito+"'";
                    command.ExecuteNonQuery();
                }
            }
            conn.Close();
            return valid;
        }
        public object[,] dbFullListForExcel()
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT COUNT(charge) FROM charge";
            SQLiteDataReader sdr = command.ExecuteReader();
            int sorszam = 0, j = 0;
            while (sdr.Read())
            {
                sorszam = Convert.ToInt32(sdr.GetValue(0));
            }
            sdr.Close();
            object[,] lista = new object[sorszam, 40];
            command.CommandText = "SELECT * FROM charge INNER JOIN cikk ON charge.charge_cikkszam = cikk.cikkszam";
            sdr = command.ExecuteReader();
            while (sdr.Read())
            {
                for (int i = 1; i < 41; i++)
                {
                    lista[j, i - 1] = sdr.GetValue(i).ToString();
                }
                j++;
            }
            sdr.Close();
            conn.Close();

            return lista;
        }
        public object[,] dbListViewLoader(int allapot, string cikkLike, string chargeLike, string beerkLike, string anyagnev, string szallito)
        {

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT COUNT(charge) FROM charge INNER JOIN cikk ON charge.charge_cikkszam = cikk.cikkszam WHERE allapot=" + allapot + " AND cikkszam LIKE'%" + cikkLike + "%'AND charge LIKE'%" + chargeLike + "%'AND beerk_datum LIKE'%" + beerkLike + "%' AND anyag_nev LIKE '%" + anyagnev + "%' AND szallito LIKE '%" + szallito + "%'";
            SQLiteDataReader sdr = command.ExecuteReader();
            int sorszam = 0, j = 0;
            while (sdr.Read())
            {
                sorszam = Convert.ToInt32(sdr.GetValue(0));
            }
            sdr.Close();

            object[,] lista = new object[sorszam, 7];
            command.CommandText = "SELECT cikkszam,charge,szallito,anyag_nev,anyag_tipus,kw,beerk_datum FROM charge INNER JOIN cikk ON charge.charge_cikkszam = cikk.cikkszam " +
                "WHERE allapot=" + allapot + " AND cikkszam LIKE'%" + cikkLike + "%'AND charge LIKE'%" + chargeLike + "%'AND beerk_datum LIKE'%" + beerkLike + "%' AND anyag_nev LIKE '%" + anyagnev + "%' AND szallito LIKE '%" + szallito + "%' " +
                "GROUP BY charge,beerk_datum ORDER BY beerk_datum DESC";
            sdr = command.ExecuteReader();
            while (sdr.Read())
            {
                for (int i = 0; i < 7; i++)
                {
                    lista[j, i] = sdr.GetValue(i).ToString();
                }
                j++;
            }
            sdr.Close();
            conn.Close();

            return lista;
        }
        public void admin_meresDelete(string cikk, string charge, string beerk)
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "DELETE FROM charge WHERE charge_cikkszam='"+cikk+"' AND charge='"+charge+"' AND beerk_datum='"+beerk+"'";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void admin_meresModify(string cikk1, string charge1, string beerk1, string cikk2, string charge2, string beerk2)
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "UPDATE charge SET charge_cikkszam='" + cikk1 + "', charge='" + charge1 + "', beerk_datum='" + beerk1 + "' WHERE charge_cikkszam='" + cikk2 + "' AND charge='" + charge2 + "' AND beerk_datum='" + beerk2 + "'";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public object[,] admin_listviewLoader(string cikkLike, string chargeLike, string kw, string beerkLike)
        {

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT COUNT(charge) FROM charge WHERE charge_cikkszam LIKE '%" + cikkLike + "%' AND charge LIKE '%" + chargeLike + "%' AND kw LIKE '%" + kw + "%' AND beerk_datum LIKE '%" + beerkLike + "%'"; 
            SQLiteDataReader sdr = command.ExecuteReader();
            int sorszam = 0, j = 0;
            while (sdr.Read())
            {
                sorszam = Convert.ToInt32(sdr.GetValue(0));
            }
            sdr.Close();

            object[,] lista = new object[sorszam, 4];
            command.CommandText = "SELECT charge_cikkszam, charge, kw, beerk_datum FROM charge WHERE charge_cikkszam LIKE '%" + cikkLike + "%' AND charge LIKE '%" + chargeLike + "%' AND kw LIKE '%"+ kw +"%' AND beerk_datum LIKE '%" + beerkLike + "%'";
            sdr = command.ExecuteReader();
            while (sdr.Read())
            {
                for (int i = 0; i < 4; i++)
                {
                    lista[j, i] = sdr.GetValue(i).ToString();
                }
                j++;
            }
            sdr.Close();
            conn.Close();

            return lista;
        }
        public object[,] DbSelectUsers()
        {
            // Admin felület listView töltéséhez szükséges 2D string tömböt ad vissza

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            object[,] lista = new object[1000, 7];
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT username,real_name,email,admintag,auth,valid,lastlogindate FROM 'users'";
            SQLiteDataReader sdr = command.ExecuteReader();
            int j = 0;
            while (sdr.Read())
            {
                for (int i = 0; i < 7; i++)
                {
                    lista[j, i] = sdr.GetValue(i).ToString();
                }
                j++;
            }
            sdr.Close();
            conn.Close();

            return lista;
        }
        public void update_User_Last_LogDate(string username, string date)
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "UPDATE users SET lastlogindate='"+date+"' WHERE username='"+ username + "'";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void UserActivityLogger(string username, string activity, int allapot, string cikk,string charge, string beerk, string date)
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS userActivity (username TEXT,activity TEXT, allapot INT,cikk TEXT,charge TEXT,beerk TEXT,date TEXT) ";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO userActivity (username,activity,allapot,cikk,charge,beerk,date) VALUES ('"+ username + "','"+ activity + "'," + allapot + ",'" + cikk + "','"+ charge + "','"+ beerk + "','"+ date + "')";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public object[,] UserActivityLister(string user, string muvelet, string allapot, string cikk, string charge, string beerk, string date)
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS userActivity (username TEXT,activity TEXT, allapot INT,cikk TEXT,charge TEXT,beerk TEXT,date TEXT) ";
            command.ExecuteNonQuery();
            command.CommandText = "SELECT COUNT(username) FROM userActivity WHERE username LIKE '%" + user + "%' AND activity LIKE '%" + muvelet + "%' AND allapot LIKE '%" + allapot + "%' AND  cikk LIKE '%" + cikk + "%' AND charge LIKE '%" + charge + "%' AND beerk LIKE '%" + beerk + "%' AND date LIKE '%" + date + "%' ORDER BY date DESC";
            SQLiteDataReader sdr = command.ExecuteReader();
            int sorszam = 0, j = 0;
            while (sdr.Read())
            {
                sorszam = Convert.ToInt32(sdr.GetValue(0));
            }
            sdr.Close();

            object[,] lista = new object[sorszam, 7];
            command.CommandText = "SELECT username,activity,allapot,cikk,charge,beerk,date FROM userActivity WHERE username LIKE '%"+user+ "%' AND activity LIKE '%"+ muvelet + "%' AND allapot LIKE '%" + allapot + "%' AND  cikk LIKE '%" + cikk + "%' AND charge LIKE '%"+ charge + "%' AND beerk LIKE '%"+ beerk + "%' AND date LIKE '%"+ date + "%' ORDER BY date DESC";
            sdr = command.ExecuteReader();
            while (sdr.Read())
            {
                for (int i = 0; i < 7; i++)
                {
                    lista[j, i] = sdr.GetValue(i).ToString();
                }
                j++;
            }
            sdr.Close();
            conn.Close();

            return lista;
        }
        public bool dbEnterValider(string user, string pass)
        {
            // Logint validáló függvény
            // Figyelembe veszi validált-e az adott felhasználó

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            bool valid = false;
            conn.Open();
            var command = conn.CreateCommand();
            try
            {
                command.CommandText = "SELECT count() FROM 'users' WHERE username='" + user + "' AND pass='" + pass + "' AND valid=1";
                SQLiteDataReader sdr = command.ExecuteReader();
                int dbdata = 0;
                while (sdr.Read())
                {
                    dbdata = sdr.GetInt32(0);
                }
                sdr.Close();
                if (dbdata == 0)
                {
                    valid = false;
                    int dbdata2 = 0;
                    command.CommandText = "SELECT count() FROM 'users'";
                    SQLiteDataReader sdr2 = command.ExecuteReader();
                    while (sdr2.Read())
                    {
                        dbdata2 = sdr2.GetInt32(0);
                    }
                    sdr2.Close();
                    if (dbdata2 == 0)
                    {
                        command.CommandText = "INSERT INTO 'users' ('username', 'pass', 'real_name', 'auth', 'email', 'valid', 'admintag', lastlogindate) VALUES ('admin', '3hgb8wy', 'Admin felhasználó', 3, 'fzbalu92@gmail.com' , 1, 1, 0)";
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    valid = true;
                }
            }
            catch (Exception)
            {
                valid = false;
            }
            conn.Close();
            return valid;
        }
        public void deleteUser(string user)
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();

            command.CommandText = "DELETE FROM users WHERE username='"+ user + "'";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void userCahnge(List<object> lista)
        {
            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();

            command.CommandText = "UPDATE users SET " +
                " 'username'='" + lista[0] + "'," +
                " 'real_name'='" + lista[1] + "'," +
                " 'auth'=" + lista[2] + "," +
                " 'email'='" + lista[3] + "'," +
                " 'valid'=" + lista[4] + "," +
                " 'admintag'=" + lista[5] + "" +
                " WHERE username = '" + lista[0] + "' ";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public List<string> sessionDataSource(string user, string pass)
        {
            // A munkamenet számára fontos információkat adja vissza listában
            // Sikeres bejelentkezésnél hívódik meg

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            List<string> sessLista = new List<string>();
            conn.Open();
            var command = conn.CreateCommand();

                command.CommandText = "SELECT * FROM 'users' WHERE username='" + user + "' AND pass='" + pass + "' AND valid=1";
                SQLiteDataReader sdr = command.ExecuteReader();
                string dbdata = null;
                while (sdr.Read())
                {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    dbdata = sdr.GetValue(i).ToString();
                    sessLista.Add(dbdata);

                }
                }
                sdr.Close();

            
            conn.Close();
            return sessLista;
        }
        public bool dbUserRegistration(string user, string pass, string pass2, string realname, string email)
        {
            // Előzetes regisztrációt adatbázisba mentő függvény (validitás nulla marad)

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            bool valid = false;
            conn.Open();
            var command = conn.CreateCommand();
            try
            {
                command.CommandText = "SELECT * FROM 'users' WHERE username='" + user + "';";
                SQLiteDataReader sdr = command.ExecuteReader();
                string dbdata = null;
                while (sdr.Read())
                {
                    dbdata = sdr.GetString(1);
                }
                sdr.Close();
                if (dbdata == null)
                {
                    command.CommandText = "INSERT INTO 'users' ('username', 'pass', 'real_name', 'auth', 'email', 'valid', 'admintag', lastlogindate) VALUES ('"+user+"', '"+pass+"', '"+realname+"', 1, '"+email+"' , 0, 0, 0)";
                    command.ExecuteNonQuery();
                    valid = true;
                }
                else
                {
                    valid = false;
                }
            }
            catch (Exception)
            {
                valid = false;
            }

            conn.Close();
            return valid;
        }
        public void language(List<string> lista1, List<string> lista2, List<string> lista3 )
        {

            // Nyelvi adatok feltöltése excelből adatbázisba

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS 'language' ('id' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,'hu_HU' TEXT,'de_DE' TEXT,'en_EN' TEXT)";
            command.ExecuteNonQuery();
            command.CommandText = "DELETE FROM 'language'";
            command.ExecuteNonQuery();

            command.CommandText = "DELETE FROM sqlite_sequence WHERE name = 'language'";
            command.ExecuteNonQuery();

            for (int i = 0; i < lista1.Count; i++)
            {
                command.CommandText = "INSERT INTO 'language'('hu_HU','de_DE','en_EN') VALUES ('" + lista1[i] + "','" + lista2[i] + "','" + lista3[i] + "')";
                command.ExecuteNonQuery();
            }
            conn.Close();
        }
        public List<string> langWordList(int lang)
        {

            SQLiteConnection conn = new SQLiteConnection(sess.databaseUrl);
            List<string> list = new List<string>();
            var command = conn.CreateCommand();
            conn.Open();
            command.CommandText = "SELECT * FROM 'language'";

            SQLiteDataReader sdr = command.ExecuteReader();
            string dbdata;
            while (sdr.Read())
            {
                dbdata = sdr.GetString(lang);
                list.Add(dbdata);
            }
            sdr.Close();
            conn.Close();
            return list;
        }

    }
}