using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VITacademics.TimeTable
{
    public class TimeTable
    {
        List<Slot> Day = new List<Slot>();
        DataHandler dat = new DataHandler();
        List<Subject> subs = new List<Subject>();
        String slts_today;

        public List<Slot> today()
        {
            DateTime t = DateTime.Now;
            subs = dat.loadSubjects();
            set_Day(t.DayOfWeek);
            return scanDay();
        }

        private void set_Day(DayOfWeek t) {

            switch (t) {
                case DayOfWeek.Monday:
                    slts_today = "a1 f1 c1 e1 td1 a2 f2 c2 e2 td2 l1 l2 l3 l4 l5 l6 l31 l32 l33 l34 l35 l36";
                    break;
                case DayOfWeek.Tuesday:
                    slts_today = "b1 g1 d1 ta1 tf1 b2 g2 d2 ta2 tf2 l7 l8 l9 l10 l11 l12 l37 l38 l39 l40 l41 l42";
                    break;
                case DayOfWeek.Wednesday:
                    slts_today = "c1 f1 e1 tb1 tg1 c2 f2 e2 tb2 tg2 l13 l14 l15 l16 l17 l18 l43 l44 l45 l46 l47 l48";
                    break;
                case DayOfWeek.Thursday:
                    slts_today = "d1 a1 f1 c1 te1 d2 a2 f2 c2 te2 l19 l20 l21 l22 l23 l24 l49 l50 l51 l52 l53 l54";
                    break;
                case DayOfWeek.Friday:
                    slts_today = "e1 b1 g1 d1 tc1 e2 b2 g2 d2 tc2 l25 l26 l27 l28 l29 l30 l55 l56 l57 l58 l59 l60";
                    break;
                default:
                    break;
            }
        }

        private List<Slot> scanDay()
        {
            for (int i = 0; i < subs.Count; i++) {
                
                //Check if multiple
                if (subs[i].slot.Contains("+")) {
                    string[] solts = subs[i].slot.Split('+');
                    foreach (string s in solts)
                    {
                        if (slts_today.Contains(s.ToLower()))
                        {
                            Slot slt = new Slot(s.ToLower(), subs[i].classnbr);
                            slt.setTimes();
                            System.Diagnostics.Debug.WriteLine(s.ToLower());
                            Day.Add(slt);
                        }

                    }
                }
               
                //Match with today's slots
                else {
                    if (slts_today.Contains (subs[i].slot.ToLower())) {
                        Slot slt = new Slot(subs[i].slot.ToLower(), subs[i].classnbr);
                        slt.setTimes();
                        Day.Add(slt);
                    }
                }
                
            }
            return Day;
        }

        public void getForDay(DayOfWeek t)
        {

        }
    }

    public class Slot {
        public  DateTime frm_time;
        public DateTime to_time;
        public String slt, clsnbr;
        public Slot(String slt , String clsnbr) {
            this.slt = slt;
            this.clsnbr = clsnbr;
        }
        public void setTimes() {

        }
    }
}
