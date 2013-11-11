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

    }
}