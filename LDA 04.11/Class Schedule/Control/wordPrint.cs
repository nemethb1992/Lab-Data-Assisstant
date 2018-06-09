using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Class_Schedule.Database;
using Class_Schedule.Control;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using Word = Microsoft.Office.Interop.Word;

namespace Class_Schedule.Control
{
    class wordPrint
    {
        public void dataPrintToDoc()
        {
            var wordApp = new Word.Application();
            wordApp.Visible = true;
            wordApp.Documents.Add();
        }
    }

}
