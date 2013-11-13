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

namespace VITacademics
{
    public partial class subjectDetails : PhoneApplicationPage
    {

        Subject sub;
        double attended, conducted, t1, t2;

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string st_subnum;
            GoogleAnalytics.EasyTracker.GetTracker().SendView("SubjectDetailsPage");
            NavigationContext.QueryString.TryGetValue("selectedItem", out st_subnum);
            DataHandler dat = new DataHandler();

            sub = dat.getSubject(st_subnum);
            InitializeComponent();
            t1 = 0.0;
            t2 = 0.0;
            Control_Pan.Title = sub.title;
            attended = sub.attended;
            conducted = sub.conducted;
            setPer(getPer());
            loadAll();

            MarksAdapter m = new MarksAdapter();
            Mark mrk = dat.loadMarks(st_subnum);
            double q_tot = 0.0, c_tot=0.0 , tot = 0.0;
            if (!mrk.islab) {
                
                Brush brsh = new System.Windows.Media.SolidColorBrush(Colors.White);
                
                m.title = mrk.cat[0].name;
                m.mrks = mrk.cat[0].marks + "/50";
                c_tot += get_sum (mrk.cat[0].marks,0);
                m.ac_mrk = get_sum(mrk.cat[0].marks, 0).ToString("#.0") + "/15";
                m.clour = brsh;
                marks.Items.Add(m);


                m = nm();
                m.title = mrk.cat[1].name;
                m.mrks = mrk.cat[1].marks + "/50";
                c_tot += get_sum(mrk.cat[1].marks, 0);
                m.ac_mrk = get_sum(mrk.cat[1].marks, 0).ToString("#.0") + "/15";
                m.clour = brsh;
                marks.Items.Add(m);

                m = nm();
                m.title = mrk.quiz[0].name;
                q_tot += get_sum(mrk.quiz[0].marks, 1);
                m.mrks = mrk.quiz[0].marks + "/5";
                m.ac_mrk = "";
                m.clour = brsh;
                marks.Items.Add(m);

                m = nm();
                m.title = mrk.quiz[1].name;
                q_tot += get_sum(mrk.quiz[1].marks, 1);
                m.mrks = mrk.quiz[1].marks + "/5";
                m.ac_mrk = "";
                m.clour = brsh;
                marks.Items.Add(m);

                m = nm();
                m.title = mrk.quiz[2].name;
                q_tot += get_sum(mrk.quiz[2].marks, 1);
                m.mrks = mrk.quiz[2].marks + "/5";
                m.ac_mrk = "";
                m.clour = brsh;
                marks.Items.Add(m);

                m = nm();
                m.title = "Assignment";
                m.mrks = mrk.asgn.marks + "/5";
                m.ac_mrk = "";
                m.clour = brsh;
                marks.Items.Add(m);

                tot = c_tot + q_tot + get_sum(mrk.asgn.marks, 1);
                m = nm();
                m.title = "Total";
                m.mrks = tot.ToString("#.0") + "/50";
                m.ac_mrk = "";
                m.clour = new System.Windows.Media.SolidColorBrush(Colors.Orange);
                marks.Items.Add(m);
            }

            else
            {
                m = nm();
                m.title = "PBL/Lab not supported.";
                m.mrks = "";
                m.ac_mrk = "";
                m.clour = new System.Windows.Media.SolidColorBrush(Colors.Orange);
                marks.Items.Add(m);

            }
        }

        private double get_sum(String num ,int type) {
            double t = 0.0;
            try
            {
                if (type == 0)
                {
                    t = Convert.ToDouble(num);
                    
                    t = (15.0 / 50.0) * t;
                    
                    return t;
                }
                else if (type == 1){
                    t = Convert.ToDouble(num);
                   
                    return t;
                }
            }
            catch (Exception) {}
            return t;
        }
        private DataAdapter nw() {
            return new DataAdapter();
        }

        private MarksAdapter nm() {
            return new MarksAdapter();
        }

        private void loadAll() {
            DataHandler dat = new DataHandler();
            
            DataAdapter m = new DataAdapter();

            m.title = "Subject Code";
            m.description = sub.code;
            details.Items.Add(m);

            m = nw();
            m.title = "Type";
            m.description = sub.type;
            details.Items.Add(m);

            m = nw();
            m.title = "Slot";
            m.description = sub.slot;
            details.Items.Add(m);

            m = nw();
            m.title = "Attended";
            m.description = sub.attended.ToString();
            details.Items.Add(m);

            m = nw();
            m.title = "Conducted";
            m.description = sub.conducted.ToString();
            details.Items.Add(m);

            m = nw();
            m.title = "Percentage";
            m.description = sub.percentage;
            details.Items.Add(m);

            for (int i = sub.attendance.Count -1 ; i >= 0; i--)
            {
                m = nw();
                moredetails.Items.Add(sub.attendance[i]);
                
            } 
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
        private void setPer(double per) {
            lbl_percent.Foreground = new SolidColorBrush(getClr(per));
            lbl_percent.Text = Convert.ToString(per) + "%";
        }

        private double getPer() {
            return Math.Round((attended / conducted) * 100, 1);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (t1 > 0)
            {
                t1 -= 1;
                textBlock1.Text = "If you miss " + t1 + " class(s)";
                conducted -= 1;
                setPer(getPer());
            }

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (t1 >= 0)
            {
                t1 += 1;
                textBlock1.Text = "If you miss " + t1 + " class(s)";
                conducted += 1;
                setPer(getPer());
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (t2 >= 0)
            {
                t2 += 1;
                textBlock2.Text = "If you attend " + t2 + " class(s)";
                conducted += 1;
                attended += 1;
                setPer(getPer());

            }

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (t2 > 0)
            {
                t2 -= 1;
                textBlock2.Text = "If you attend " + t2 + " class(s)";
                conducted -= 1;
                attended -= 1;
                setPer(getPer());

            }

        }

        private void adControl1_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Error.Message);
        }

    }
}