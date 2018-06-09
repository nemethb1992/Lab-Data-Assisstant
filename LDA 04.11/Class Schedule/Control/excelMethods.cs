using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Class_Schedule.Database;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Class_Schedule.Control
{
    struct Adatok
    {
        public string col_1;
        public string col_2;
        public string col_3;
        public string col_4;
        public string col_5;
        public string col_6;
        public string col_7;
        public string col_8;
        public string col_9;
        public string col_10;
        public string col_11;
        public string col_12;
        public string col_13;
        public string col_14;
        public string col_15;
        public string col_16;
        public string col_17;
        public string col_18;
        public string col_19;
        public string col_20;
        public string col_21;
        public string col_22;
        public string col_23;
        public string col_24;
        public string col_25;
        public string col_26;
        public string col_27;
        public string col_28;
        public string col_29;
        public string col_30;
        public string col_31;
        public string col_32;
        public string col_33;
        public string col_34;
        public string col_35;
        public string col_36;
        public string col_37;
        public string col_38;
        //public string col_39;
    }
    public class excelMethods
    {
        private Range Col1;
        private Range Col2;
        private Range Col3;
        private Range Col4;
        private Range Col5;
        private Range Col6;
        private Range Col7;
        private Range Col8;
        private Range Col9;
        private Range Col10;
        private Range Col11;
        private Range Col12;
        private Range Col13;
        private Range Col14;
        private Range Col15;
        private Range Col16;
        private Range Col17;
        private Range Col18;
        private Range Col19;
        private Range Col20;
        private Range Col21;
        private Range Col22;
        private Range Col23;
        private Range Col24;
        private Range Col25;
        private Range Col26;
        private Range Col27;
        private Range Col28;
        private Range Col29;
        private Range Col30;
        private Range Col31;
        private Range Col32;
        private Range Col33;
        private Range Col34;
        private Range Col35;
        private Range Col36;
        private Range Col37;
        private Range Col38;
        private Range ColLangHu;
        private Range ColLangDe;
        private Range ColLangEn;
        //private Range Col39;
        private Workbook wb;
        private Worksheet ws;
        private Microsoft.Office.Interop.Excel.Application excel;
        public static string path = "";
        private Adatok[] a;
        dbEntities db = new dbEntities();


        // Kapcsolódás a táblázathoz
        public string connection()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true)
                {
                    ofd.InitialDirectory = @"C:\\Users\\balazs.nemeth\\Desktop";
                    return path = ofd.FileName;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        // Táblázat beolvasása
        public void readIn()
        {

            excel = new Microsoft.Office.Interop.Excel.Application();
            wb = (excel.Workbooks.Open(connection()));
            ws = (Worksheet)wb.ActiveSheet;
            try
            {
                Col1 = ws.Columns[1];
                Col2 = ws.Columns[2];
                Col3 = ws.Columns[3];
                Col4 = ws.Columns[4];
                Col5 = ws.Columns[5];
                Col6 = ws.Columns[6];
                Col7 = ws.Columns[7];
                Col8 = ws.Columns[8];
                Col9 = ws.Columns[9];
                Col10 = ws.Columns[10];
                Col11 = ws.Columns[11];
                Col12 = ws.Columns[12];
                Col13 = ws.Columns[13];
                Col14 = ws.Columns[14];
                Col15 = ws.Columns[15];
                Col16 = ws.Columns[16];
                Col17 = ws.Columns[17];
                Col18 = ws.Columns[18];
                Col19 = ws.Columns[19];
                Col20 = ws.Columns[20];
                Col21 = ws.Columns[21];
                Col22 = ws.Columns[22];
                Col23 = ws.Columns[23];
                Col24 = ws.Columns[24];
                Col25 = ws.Columns[25];
                Col26 = ws.Columns[26];
                Col27 = ws.Columns[27];
                Col28 = ws.Columns[28];
                Col29 = ws.Columns[29];
                Col30 = ws.Columns[30];
                Col31 = ws.Columns[31];
                Col32 = ws.Columns[32];
                Col33 = ws.Columns[33];
                Col34 = ws.Columns[34];
                Col35 = ws.Columns[35];
                Col36 = ws.Columns[36];
                Col37 = ws.Columns[37];
                Col38 = ws.Columns[38];
                //Col39 = ws.Columns[39];


                int rowsCount = 0;
                //int rowsCount2 = 0;

                // Sorok számának megállapítása a táblára.
                foreach (Range item in Col1.Cells)
                {
                    if (item.Text != "") { rowsCount++; } else { break; }
                }

                // Sorok számának megállapítása a szállítók listára.
                //foreach (Range item in Col37.Cells)
                //{
                //    if (item.Text != "") { rowsCount2++; } else { break; }
                //}
                ////Struktúra példányok
                a = new Adatok[rowsCount];
                //b = new Adatok[rowsCount2];

                //1. oszlop mentése     (Cikkszám)
                
                int index = 0;
                foreach (Range item in Col1.Cells)
                {
                    if (item.Text != "") { a[index].col_1 = item.Text; index++; }
                    else { break; }
                }
                //2. oszlop mentése     
                index = 0;
                foreach (Range item in Col2.Cells)
                {
                    if (item.Text != "") { a[index].col_2 = item.Text; index++; }
                    else { break; }
                }
                //3. oszlop mentése   
                index = 0;
                foreach (Range item in Col3.Cells)
                {
                    if (item.Text != "") { a[index].col_3 = item.Text; index++; }
                    else { break; }
                }
                //4. oszlop mentése    
                index = 0;
                foreach (Range item in Col4.Cells)
                {
                    if (item.Text != "") { a[index].col_4 = item.Text; index++; }
                    else { break; }
                }
                //5. oszlop mentése  
                index = 0;
                foreach (Range item in Col5.Cells)
                {
                    if (item.Text != "") { a[index].col_5 = item.Text; index++; }
                    else { break; }
                }
                //6. oszlop mentése  
                index = 0;
                foreach (Range item in Col6.Cells)
                {
                    if (item.Text != "") { a[index].col_6 = item.Text; index++; }
                    else { break; }
                }
                //7. oszlop mentése    
                index = 0;
                foreach (Range item in Col7.Cells)
                {
                    if (item.Text != "") { a[index].col_7 = item.Text; index++; }
                    else { break; }
                }
                //8. oszlop mentése    
                index = 0;
                foreach (Range item in Col8.Cells)
                {
                    if (item.Text != "") { a[index].col_8 = item.Text; index++; }
                    else { break; }
                }
                //9. oszlop mentése    
                index = 0;
                foreach (Range item in Col9.Cells)
                {
                    if (item.Text != "") { a[index].col_9 = item.Text; index++; }
                    else { break; }
                }
                //10. oszlop mentése   
                index = 0;
                foreach (Range item in Col10.Cells)
                {
                    if (item.Text != "") { a[index].col_10 = item.Text; index++; }
                    else { break; }
                }
                //11. oszlop mentése    
                index = 0;
                foreach (Range item in Col11.Cells)
                {
                    if (item.Text != "") { a[index].col_11 = item.Text; index++; }
                    else { break; }
                }
                //12. oszlop mentése        
                index = 0;
                foreach (Range item in Col12.Cells)
                {
                    if (item.Text != "") { a[index].col_12 = item.Text; index++; }
                    else { break; }
                }
                //13. oszlop mentése       
                index = 0;
                foreach (Range item in Col13.Cells)
                {
                    if (item.Text != "") { a[index].col_13 = item.Text; index++; }
                    else { break; }
                }
                //14. oszlop mentése       
                index = 0;
                foreach (Range item in Col14.Cells)
                {
                    if (item.Text != "") { a[index].col_14 = item.Text; index++; }
                    else { break; }
                }
                //15. oszlop mentése       
                index = 0;
                foreach (Range item in Col15.Cells)
                {
                    if (item.Text != "") { a[index].col_15 = item.Text; index++; }
                    else { break; }
                }
                //16. oszlop mentése       
                index = 0;
                foreach (Range item in Col16.Cells)
                {
                    if (item.Text != "") { a[index].col_16 = item.Text; index++; }
                    else { break; }
                }
                //17. oszlop mentése       
                index = 0;
                foreach (Range item in Col17.Cells)
                {
                    if (item.Text != "") { a[index].col_17 = item.Text; index++; }
                    else { break; }
                }
                //18. oszlop mentése       
                index = 0;
                foreach (Range item in Col18.Cells)
                {
                    if (item.Text != "") { a[index].col_18 = item.Text; index++; }
                    else { break; }
                }
                //19. oszlop mentése        
                index = 0;
                foreach (Range item in Col19.Cells)
                {
                    if (item.Text != "") { a[index].col_19 = item.Text; index++; }
                    else { break; }
                }
                //20. oszlop mentése       
                index = 0;
                foreach (Range item in Col20.Cells)
                {
                    if (item.Text != "") { a[index].col_20 = item.Text; index++; }
                    else { break; }
                }
                //21. oszlop mentése       
                index = 0;
                foreach (Range item in Col21.Cells)
                {
                    if (item.Text != "") { a[index].col_21 = item.Text; index++; }
                    else { break; }
                }
                //22. oszlop mentése       
                index = 0;
                foreach (Range item in Col22.Cells)
                {
                    if (item.Text != "") { a[index].col_22 = item.Text; index++; }
                    else { break; }
                }
                //23. oszlop mentése       
                index = 0;
                foreach (Range item in Col23.Cells)
                {
                    if (item.Text != "") { a[index].col_23 = item.Text; index++; }
                    else { break; }
                }
                //24. oszlop mentése       
                index = 0;
                foreach (Range item in Col24.Cells)
                {
                    if (item.Text != "") { a[index].col_24 = item.Text; index++; }
                    else { break; }
                }
                //25. oszlop mentése        
                index = 0;
                foreach (Range item in Col25.Cells)
                {
                    if (item.Text != "") { a[index].col_25 = item.Text; index++; }
                    else { break; }
                }
                //26. oszlop mentése        
                index = 0;
                foreach (Range item in Col26.Cells)
                {
                    if (item.Text != "") { a[index].col_26 = item.Text; index++; }
                    else { break; }
                }
                //27. oszlop mentése      
                index = 0;
                foreach (Range item in Col27.Cells)
                {
                    if (item.Text != "") { a[index].col_27 = item.Text; index++; }
                    else { break; }
                }
                //28. oszlop mentése        
                index = 0;
                foreach (Range item in Col28.Cells)
                {
                    if (item.Text != "") { a[index].col_28 = item.Text; index++; }
                    else { break; }
                }
                //29. oszlop mentése       
                index = 0;
                foreach (Range item in Col29.Cells)
                {
                    if (item.Text != "") { a[index].col_29 = item.Text; index++; }
                    else { break; }
                }
                //30. oszlop mentése       
                index = 0;
                foreach (Range item in Col30.Cells)
                {
                    if (item.Text != "") { a[index].col_30 = item.Text; index++; }
                    else { break; }
                }
                //31. oszlop mentése        
                index = 0;
                foreach (Range item in Col31.Cells)
                {
                    if (item.Text != "") { a[index].col_31 = item.Text; index++; }
                    else { break; }
                }
                //32. oszlop mentése        
                index = 0;
                foreach (Range item in Col32.Cells)
                {
                    if (item.Text != "") { a[index].col_32 = item.Text; index++; }
                    else { break; }
                }
                //33. oszlop mentése       
                index = 0;
                foreach (Range item in Col33.Cells)
                {
                    if (item.Text != "") { a[index].col_33 = item.Text; index++; }
                    else { break; }
                }
                //34. oszlop mentése        
                index = 0;
                foreach (Range item in Col34.Cells)
                {
                    if (item.Text != "") { a[index].col_34 = item.Text; index++; }
                    else { break; }
                }
                //35. oszlop mentése       
                index = 0;
                foreach (Range item in Col35.Cells)
                {
                    if (item.Text != "") { a[index].col_35 = item.Text; index++; }
                    else { break; }
                }
                //36. oszlop mentése       
                index = 0;
                foreach (Range item in Col36.Cells)
                {
                    if (item.Text != "") { a[index].col_36 = item.Text; index++; }
                    else { break; }
                }
                //37. oszlop mentése       
                index = 0;
                foreach (Range item in Col37.Cells)
                {
                    if (item.Text != "") { a[index].col_37 = item.Text; index++; }
                    else { break; }
                }
                //38. oszlop mentése       
                index = 0;
                foreach (Range item in Col38.Cells)
                {
                    if (item.Text != "") { a[index].col_38 = item.Text; index++; }
                    else { break; }
                }
                //39. oszlop mentése       
                //index = 0;
                //foreach (Range item in Col39.Cells)
                //{
                //    if (item.Text != "") { a[index].col_39 = item.Text; index++; }
                //    else { break; }
                //}
            }
            catch (Exception)
            {
            }
        }
        void dbWriterCharge(int row)
        {
            for (int i = 1; i < row; i++)
            {
                List<object> charge = new List<object>();
                charge.Add(a[i].col_1);
                charge.Add(a[i].col_22);
                charge.Add(a[i].col_23);
                charge.Add(a[i].col_24);
                charge.Add(a[i].col_30);
                charge.Add(a[i].col_36);
                charge.Add(a[i].col_38);
                charge.Add(a[i].col_25);
                charge.Add(a[i].col_32);
                charge.Add(a[i].col_26);
                charge.Add(a[i].col_33);
                charge.Add(a[i].col_27);
                charge.Add(a[i].col_34);
                charge.Add(a[i].col_28);
                charge.Add(a[i].col_35);
                charge.Add(a[i].col_29);
                charge.Add(a[i].col_31);
                charge.Add(a[i].col_37);

                //adatbázisba íratás
                db.chargeWriteFromExcel(charge);
            }
        }
        void dbWriterCikk(int row)
        {
            List<string> letezoCikk = new List<string>();
            for (int i = 1; i < row; i++)
            {
                bool bool1 = true;
                foreach (var item in letezoCikk)
                {
                    if (item == a[i].col_1)
                    {
                        bool1 = false;
                        break;
                    }
                }
                List<object> cikk = new List<object>();
                if (bool1 == true)
                {
                    cikk.Add(a[i].col_1);
                    cikk.Add(a[i].col_16);
                    cikk.Add(a[i].col_18);
                    cikk.Add(a[i].col_17);
                    cikk.Add(a[i].col_19);
                    cikk.Add(a[i].col_15);
                    cikk.Add(a[i].col_8);
                    cikk.Add(a[i].col_4);
                    cikk.Add(a[i].col_7);
                    cikk.Add(a[i].col_20);
                    cikk.Add(a[i].col_21);
                    cikk.Add(a[i].col_2);
                    cikk.Add(a[i].col_3);
                    cikk.Add(a[i].col_5);
                    cikk.Add(a[i].col_6);
                    cikk.Add(a[i].col_9);
                    cikk.Add(a[i].col_10);
                    cikk.Add(a[i].col_11);
                    cikk.Add(a[i].col_12);
                    cikk.Add(a[i].col_13);
                    cikk.Add(a[i].col_14);
                    letezoCikk.Add(a[i].col_1);

                    db.cikkWriteFromExcel(cikk);
                }
            }
        
        }
        public void excelTablaBeolvaso()
        {
            readIn();
            int rowsCount = 0;
            foreach (Range item in Col1.Cells)
            {
                if (item.Text != "") { rowsCount++; } else { break; }
            }
            db.dbDefault("cikk");
            dbWriterCikk(rowsCount);
            db.dbDefault("charge");
            dbWriterCharge(rowsCount);

            wb.Close();
            excel.Quit();
        }
        public void excelAdatbazisKimento()
        {
            excel = new Microsoft.Office.Interop.Excel.Application();
            wb = (excel.Workbooks.Open(connection()));
            ws = (Worksheet)wb.ActiveSheet;
            object[,] ob = db.dbFullListForExcel();
            int lengthOfList = 0;
            try
            {
                for (int i = 0; i < 16000; i++)
                {
                    if (ob[i, 0].ToString() == "" || ob[i, 0] == null)
                    {
                        break;
                    }
                    else
                    {
                        lengthOfList++;
                    }
                }
            }
            catch (Exception)
            {
                
            }

            for (int i = 0; i < lengthOfList; i++)
            {

                ws.Cells[i + 2, 1] = ob[i, 0];
                ws.Cells[i + 2, 2] = ob[i, 30];
                ws.Cells[i + 2, 3] = ob[i, 31];
                ws.Cells[i + 2, 4] = ob[i, 26];
                ws.Cells[i + 2, 5] = ob[i, 32];
                ws.Cells[i + 2, 6] = ob[i, 33];
                ws.Cells[i + 2, 7] = ob[i, 27];
                ws.Cells[i + 2, 8] = ob[i, 25];
                ws.Cells[i + 2, 9] = ob[i, 34];
                ws.Cells[i + 2, 10] = ob[i, 35];
                ws.Cells[i + 2, 11] = ob[i, 36];
                ws.Cells[i + 2, 12] = ob[i, 37];
                ws.Cells[i + 2, 13] = ob[i, 38];
                ws.Cells[i + 2, 14] = ob[i, 39];
                ws.Cells[i + 2, 15] = ob[i, 24];
                ws.Cells[i + 2, 16] = ob[i, 20];
                ws.Cells[i + 2, 17] = ob[i, 22];
                ws.Cells[i + 2, 18] = ob[i, 21];
                ws.Cells[i + 2, 19] = ob[i, 23];
                ws.Cells[i + 2, 20] = ob[i, 28];
                ws.Cells[i + 2, 21] = ob[i, 29];
                ws.Cells[i + 2, 22] = ob[i, 1];
                ws.Cells[i + 2, 23] = ob[i, 2];
                ws.Cells[i + 2, 24] = ob[i, 3];
                ws.Cells[i + 2, 25] = ob[i, 7];
                ws.Cells[i + 2, 26] = ob[i, 9];
                ws.Cells[i + 2, 27] = ob[i, 11];
                ws.Cells[i + 2, 28] = ob[i, 13];
                ws.Cells[i + 2, 29] = ob[i, 15];
                ws.Cells[i + 2, 30] = ob[i, 4];
                ws.Cells[i + 2, 31] = ob[i, 16];
                ws.Cells[i + 2, 32] = ob[i, 8];
                ws.Cells[i + 2, 33] = ob[i, 10];
                ws.Cells[i + 2, 34] = ob[i, 12];
                ws.Cells[i + 2, 35] = ob[i, 14];
                ws.Cells[i + 2, 36] = ob[i, 5];
                ws.Cells[i + 2, 37] = ob[i, 17];
                ws.Cells[i + 2, 38] = ob[i, 6];
            }
            try
            {
                ws.SaveAs(path);
            }
            catch (Exception)
            {
                throw;
            }

            wb.Close();
            excel.Quit();
        }
        // Nyelvi adatok listázása excelből

        public void langListRead()
        {

            //Szójegyzéket olvassa xls fájlból az adatbázisba.

            List<string> langList1 = new List<string>();
            List<string> langList2 = new List<string>();
            List<string> langList3 = new List<string>();
            excel = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                wb = (excel.Workbooks.Open(connection()));
                ws = (Worksheet)wb.ActiveSheet;
                ColLangHu = ws.Columns[1];
                ColLangDe = ws.Columns[2];
                ColLangEn = ws.Columns[3];
                int rowsCount = 0;
                a = new Adatok[rowsCount];
                foreach (Range item in ColLangHu.Cells)
                {
                    if (item.Text != "")
                    {
                        rowsCount++;
                        langList1.Add(item.Text);
                    }
                    else { break; }
                }
                rowsCount = 0;
                foreach (Range item in ColLangDe.Cells)
                {
                    if (item.Text != "")
                    {
                        rowsCount++;
                        langList2.Add(item.Text);
                    }
                    else { break; }
                }
                rowsCount = 0;
                foreach (Range item in ColLangEn.Cells)
                {
                    if (item.Text != "")
                    {
                        rowsCount++;
                        langList3.Add(item.Text);
                    }
                    else { break; }
                }
                db.language(langList1, langList2, langList3);
                wb.Close();
                excel.Quit();
            }
            catch (Exception)
            {
                Environment.Exit(0);
            }
  
        }
        
    }
}
