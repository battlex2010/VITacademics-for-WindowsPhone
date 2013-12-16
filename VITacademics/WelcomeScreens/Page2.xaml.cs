using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace VITacademics.WelcomeScreens
{
    public partial class Page2 : PhoneApplicationPage
    {
        DataHandler dat = new DataHandler();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationService.RemoveBackEntry();
        }

        
        public Page2()
        {
            InitializeComponent();
            loadSettings();
            lbl_notice.Text = "Please enter credentials for the same person who logged in with facebook.";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            lbl_notice.Text = "";
            prg_loading.Visibility = Visibility.Visible;
            button1.IsEnabled = false;
            saveIt();
            goCaptchaLess();
        }

        private string checkDate(String dat)
        {
            if (dat.Length == 1)
                dat = "0" + dat;
            return dat;
        }

        public void saveIt()
        {

            DateTime dater;
            String dob;
            dater = datePicker.Value.Value;

            if (txt_REG.Text.TrimEnd() != "")
            {
                dat.saveReg(txt_REG.Text.ToUpper());
                dob = checkDate(dater.Day.ToString()) + checkDate(dater.Month.ToString()) + dater.Year.ToString();

                if (chk_Vellore.IsChecked == true)
                    dat.setCampus(true);
                else
                    dat.setCampus(false);

                if (chk_att.IsChecked == true)
                    dat.setdefPage(true);
                else
                    dat.setdefPage(false);

                dat.saveDob(dob);
            }
        }

        private void goCaptchaLess() {
            loadPage("http://www.vitacademicsrel.appspot.com/captchaless/" + dat.getReg() + "/" + dat.getDob(), loadHTMLCallback);
        }

        private void loadPage(String url , DownloadStringCompletedEventHandler x)
        {
            WebClient wClient;
            wClient = new WebClient();
            wClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(x);
            wClient.DownloadStringAsync(new Uri(url));
        }


        //Get everything possible from server and as fast as possible ;)
        private bool mrks = false, att = false, tt = false;
        private void ultimateLoading() {
            
            if (dat.isVellore())
                loadPage("http://www.vitacademicsrel.appspot.com/attj/" + dat.getReg() + "/" + dat.getDob(), loadHTMLCallback2);
            else
                loadPage("http://www.vitacademicsrelc.appspot.com/attj/" + dat.getReg() + "/" + dat.getDob(), loadHTMLCallback2);

            if (dat.isVellore())
                loadPage("http://www.vitacademicsrel.appspot.com/marks/" + dat.getReg() + "/" + dat.getDob(), loadHTMLCallback3);
            else
                loadPage("http://www.vitacademicsrelc.appspot.com/marks/" + dat.getReg() + "/" + dat.getDob(), loadHTMLCallback3);

            if (dat.isVellore())
                loadPage("http://www.vitacademicsrel.appspot.com/tt/" + dat.getReg() + "/" + dat.getDob(), loadHTMLCallback4);
            else
                loadPage("http://www.vitacademicsrelc.appspot.com/tt/" + dat.getReg() + "/" + dat.getDob(), loadHTMLCallback4);
        }

        //Check if everything loaded and change
        private void chk_chg(){
            if(mrks == true && att == true && tt == true)
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        public void loadHTMLCallback(Object sender, DownloadStringCompletedEventArgs e) {
            try {
                String res = (string)e.Result;
                if (res != null && res.Contains("success"))
                    ultimateLoading();

                else
                {
                    prg_loading.Visibility = Visibility.Collapsed;
                    button1.IsEnabled = true;
                    MessageBox.Show("Please check your credentials and try again!");
                }
            }
            catch
            {
                prg_loading.Visibility = Visibility.Collapsed;
                button1.IsEnabled = true;
                MessageBox.Show("Unknown Error! Please check network and your credentials then try again!");
                lbl_notice.Text = "Please enter credentials for the same person who logged in with facebook.";
            }
        }

        public void loadHTMLCallback2(Object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                String res = (string)e.Result;
                if (res != null && res.Contains("valid%"))
                {
                    //SAVE ATTENDANCE
                    dat.saveAttendance(res);
                    att = true;
                    chk_chg();
                }
            }
            catch
            {
                prg_loading.Visibility = Visibility.Collapsed;
                button1.IsEnabled = true;

                MessageBox.Show("Unknown Error! Please check network and your credentials then try again!");
                lbl_notice.Text = "Please enter credentials for the same person who logged in with facebook.";
            }
        }

        public void loadHTMLCallback3(Object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                String res = (string)e.Result;
                if (res != null)
                {

                    //SAVE MARKS
                    dat.saveMarks(res);
                    mrks = true;
                    chk_chg();
                }

            }
            catch
            {
                prg_loading.Visibility = Visibility.Collapsed;
                button1.IsEnabled = true;
                MessageBox.Show("Unknown Error! Please check network and your credentials then try again!");
                lbl_notice.Text = "Please enter credentials for the same person who logged in with facebook.";
            }
        }

        public void loadHTMLCallback4(Object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                String res = (string)e.Result;
                if (res != null)
                {

                    dat.saveTT(res);
                    tt = true;
                    chk_chg();
                }
            }
            catch
            {
                prg_loading.Visibility = Visibility.Collapsed;
                button1.IsEnabled = true;
                MessageBox.Show("Unknown Error! Please check network and your credentials then try again!");
                lbl_notice.Text = "Please enter credentials for the same person who logged in with facebook.";
            }
        }

        private void loadSettings() {
            
            txt_REG.Text = (string)dat.getReg();
            if (txt_REG.Text == "" || dat.getDob().Equals(""))
            {}
            else
            {
                DateTime dater;
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR");
                String date = (dat.getDob().Insert(2, "/")).Insert(5, "/");
                dater = DateTime.Parse(date, culture);
                datePicker.Value = dater;
                if (dat.isVellore())
                    chk_Vellore.IsChecked = true;
                else
                    chk_Chennai.IsChecked = true;
            }
        }
    }
}