using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Class_Schedule.Control
{
    public class MeasureData
    {
        private static List<string> MeasureDatas;
        public List<string> measureData { get { return MeasureDatas; } set { MeasureDatas = value; } }
    }
}
