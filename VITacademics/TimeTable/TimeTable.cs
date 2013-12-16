/* 
 * TIMETABLE HANDLING FILE
 * Author: Saurabh Joshi
 * Date: 11th Nov 2013
 * This class recreates the timetable from the slots of users saved subjects.
 * Returns list of Slots which have the venue, start time , end time, slot and class number.
 * TODO: Make it more efficient!
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VITacademics.TimeTable
{
    public class TimeTable
    {
        //Initialize all the list
        List<Slot> Day = new List<Slot>();
        DataHandler dat = new DataHandler();
        List<Subject> subs = new List<Subject>();
        List <String> slts_today = new List<string>();

        //Get all the users list of subjects
        public TimeTable() {
            subs = dat.loadSubjects();
        }

        //Returns todays timetable
        public List<Slot> today()
        {
           
            DateTime t = DateTime.Now;
            set_Day(t.DayOfWeek);
            return scanDay();
        }

        //Returns timetable for particular day
        public List <Slot> getForDay(DayOfWeek t)
        {
            
            set_Day(t);
            return scanDay();
        }
       

        //Set the day in current context with the slots of today.
        private void set_Day(DayOfWeek t) {
            slts_today.Clear();
            switch (t) {
                case DayOfWeek.Monday:
                    slts_today.AddRange(new string[] {"a1",  "f1", "c1",  "e1", "td1", "a2", "f2", "c2", "e2", "td2", "l1", "l2", "l3", "l4", "l5", "l6", "l31", "l32", "l33", "l34", "l35", "l36"});
                    break;
                case DayOfWeek.Tuesday:
                    slts_today.AddRange(new string[] { "b1", "g1", "d1", "ta1", "tf1", "b2", "g2", "d2", "ta2", "tf2", "l7", "l8", "l9", "l10", "l11", "l12", "l37", "l38", "l39", "l40", "l41", "l42"});
                    break;
                case DayOfWeek.Wednesday:
                    slts_today.AddRange(new string[] {"c1","f1","e1","tb1","tg1","c2","f2","e2","tb2","tg2","l13","l14","l15","l16","l17","l18","l43","l44","l45","l46","l47","l48"});
                    break;
                case DayOfWeek.Thursday:
                    slts_today.AddRange(new string[] {"d1","a1","f1","c1","te1","d2","a2","f2","c2","te2","l19","l20","l21","l22","l23","l24","l49","l50","l51","l52","l53","l54"});
                    break;
                case DayOfWeek.Friday:
                    slts_today.AddRange(new string[] {"e1","b1","g1","d1","tc1","e2","b2","g2","d2","tc2","l25","l26","l27","l28 ","l29","l30","l55","l56","l57","l58","l59","l60"});
                    break;
                    //No Classes Wooohooo!!
                default:
                    slts_today.AddRange(new string[] { "" });
                    break;
            }
        }

        //Scan all the subjects and match the slots
        private List<Slot> scanDay()
        {
            Day.Clear();
            try
            {
                for (int i = 0; i < subs.Count; i++)
                {
                    //Check if multiple
                    if (subs[i].slot.Contains("+"))
                    {
                        string[] solts = subs[i].slot.Split('+');
                        foreach (string s in solts)
                        {
                            if (slts_today.Contains(s.ToLower()))
                            {

                                Slot slt = new Slot(s.ToLower(), subs[i].classnbr);
                                slt.setTimes(slts_today.IndexOf(s.ToLower()));
                                Day.Add(slt);
                            }
                        }
                    }

                    //Match with today's slots
                    else
                    {
                        if (slts_today.Contains(subs[i].slot.ToLower()))
                        {
                            Slot slt = new Slot(subs[i].slot.ToLower(), subs[i].classnbr);
                            slt.setTimes(slts_today.IndexOf(subs[i].slot.ToLower()));
                            Day.Add(slt);
                        }
                    }
                }
                Day = Day.OrderBy(x => x.frm_time).ToList();
            }catch{}
            return Day;
        }

       
    }

    //Slot class which carries all info
    public class Slot {
        public  DateTime frm_time;
        public DateTime to_time;
        public String slt, clsnbr;
        public bool isLab = false;
        public Slot(String slt , String clsnbr) {
            this.slt = slt;
            this.clsnbr = clsnbr;
        }

        //Set time for the slot
        public void setTimes(int index) {
            //Set today's date
            frm_time = new DateTime(2009, 3, 2, 0, 0, 0);
            to_time = new DateTime(2009, 3, 2, 0, 0, 0);
            
            //Check if it is lab
            if(slt.StartsWith("l"))
                isLab = true;

            //change the time keeping the date same
            switch (index) {
                case 0:
                    frm_time = frm_time.Date.AddHours(8);
                    to_time = to_time.Date.AddHours(8).AddMinutes(50);
                    break;
                case 1:
                    frm_time = frm_time.Date.AddHours(9);
                    to_time = to_time.Date.AddHours(9).AddMinutes(50);
                    break;
                case 2: 
                    frm_time = frm_time.Date.AddHours(10);
                    to_time = to_time.Date.AddHours(10).AddMinutes(50);
                    break;
                case 3: 
                    frm_time = frm_time.Date.AddHours(11);
                    to_time = to_time.Date.AddHours(11).AddMinutes(50);
                    break;
                case 4:
                    
                    if (isLab)
                    {
                        frm_time = frm_time.Date.AddHours(11).AddMinutes(50);
                        to_time = to_time.Date.AddHours(12).AddMinutes(40);
                    }
                    else
                    {
                        frm_time = frm_time.Date.AddHours(12);
                        to_time = to_time.Date.AddHours(12).AddMinutes(50);
                    }

                    break;
                case 5:
                    frm_time = frm_time.Date.AddHours(12).AddMinutes(40);
                    to_time = to_time.Date.AddHours(13).AddMinutes(30);
                    break;
                case 6:
                    frm_time = frm_time.Date.AddHours(14);
                    to_time = to_time.Date.AddHours(14).AddMinutes(50);
                    break;
                case 7:
                    frm_time = frm_time.Date.AddHours(15);
                    to_time = to_time.Date.AddHours(15).AddMinutes(50);
                    break;
                case 8:
                    frm_time = frm_time.Date.AddHours(16);
                    to_time = to_time.Date.AddHours(16).AddMinutes(50);
                    break;
                case 9:
                    frm_time = frm_time.Date.AddHours(17);
                    to_time = to_time.Date.AddHours(17).AddMinutes(50);
                    break;
                case 10:
                    if (isLab)
                    {
                        frm_time = frm_time.Date.AddHours(17).AddMinutes(50);
                        to_time = to_time.Date.AddHours(18).AddMinutes(40);
                    }
                    else {
                        frm_time = frm_time.Date.AddHours(18);
                        to_time = to_time.Date.AddHours(18).AddMinutes(50);
                    }
                    break;
                case 11:
                    frm_time = frm_time.Date.AddHours(18).AddMinutes(40);
                    to_time = to_time.Date.AddHours(19).AddMinutes(30);
                    break;
            }

        }
    }
}
