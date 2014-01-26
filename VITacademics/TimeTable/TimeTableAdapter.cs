using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace VITacademics.TimeTable
{
    class TimeTableAdapter
    {

        public string TT_Title { set; get; }
        public string TT_Slot  { set; get; }
        public string TT_Venue { set; get; }
        public string TT_Time  { set; get; }
        public string TT_Att   { set; get; }
        public Brush Att_Color { set; get; }
    }
}
