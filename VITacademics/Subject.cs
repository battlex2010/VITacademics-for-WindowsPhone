using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace VITacademics
{
    public class Subject
    {
        public String title, code, type, slot, regdate, classnbr, atten_last_status, atten_last_date,percentage, attendance_details,venue;
        public int attended, conducted;
        public int attendance_length;
        public bool att_valid;

        public List<DayByDay> attendance;
    }

    public class DayByDay
    {
        public string date { get; set; }
        public string status { get; set; }

        public Brush clour { get; set; }
        
        public DayByDay(String date, String status) {
            this.date = date;
            this.status = status;
            if (status.Equals("Absent"))
                this.clour = new System.Windows.Media.SolidColorBrush(Colors.Orange);
            else
                this.clour = new System.Windows.Media.SolidColorBrush(Colors.White);
        }
       
    }
}
