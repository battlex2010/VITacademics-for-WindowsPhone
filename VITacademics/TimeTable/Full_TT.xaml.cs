using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace VITacademics.TimeTable
{
    public partial class Full_TT : PhoneApplicationPage
    {

        public Full_TT()
        {
            InitializeComponent();
            TimeTable tt = new TimeTable();
            TimeTableAdapter t;
            DataHandler dat = new DataHandler();
            try
            {
                for (int i = 1; i < 6; i++)
                {
                    foreach (Slot s in tt.getForDay((DayOfWeek)i))
                    {
                        //Load the subject
                        t = new TimeTableAdapter();
                        Subject sub = dat.getSubject(s.clsnbr);
                        t.TT_Title = sub.title;
                        t.TT_Slot = s.slt.ToUpper();
                        t.TT_Time = s.frm_time.ToString("t") + " - " + s.to_time.ToString("t");
                        t.TT_Venue = "";
                        t.TT_Att = sub.percentage;
                        double per = Math.Round(((double)sub.attended / (double)sub.conducted) * 100, 1);

                        t.Att_Color = new SolidColorBrush(getClr(per));
                        switch (i)
                        {
                            //Monday
                            case 1:
                                tt_monday.Items.Add(t);
                                break;
                            case 2:
                                tt_tuesday.Items.Add(t);
                                break;
                            case 3:
                                tt_wednesday.Items.Add(t);
                                break;
                            case 4:
                                tt_thursday.Items.Add(t);
                                break;
                            case 5:
                                tt_friday.Items.Add(t);
                                break;
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private Color getClr(double per)
        {
            if (per < 80 && per >= 75)
                return Colors.Orange;
            else if (per < 75)
                return Colors.Red;
            else
                return Colors.White;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //Set today's day
            if (e.NavigationMode == NavigationMode.New) {
                GoogleAnalytics.EasyTracker.GetTracker().SendView("FullTimeTablePage");
                int today = ((int) DateTime.Now.DayOfWeek);
                if (today < 6 && today != 0) 
                    Controller.DefaultItem = Controller.Items[today - 1];
            }
        }
    }
}