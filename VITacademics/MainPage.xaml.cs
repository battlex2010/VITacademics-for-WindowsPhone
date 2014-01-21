using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Data;
using VITacademics.TimeTable;
using System.Windows.Media.Imaging;

namespace VITacademics
{
    public partial class MainPage : PhoneApplicationPage
    {
        //Public variables
        
        DataHandler dat;
        bool newUser = false;
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            dat = new DataHandler();
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            GoogleAnalytics.EasyTracker.GetTracker().SendView("MainPanaroma");

            App.ViewModel.Items.Clear();
            txt_REG.Text = (string) dat.getReg();
            if (txt_REG.Text == "" || dat.getDob().Equals(""))
            {
                newUser = true;
            }
            else {
                DateTime dater;
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR");
                String date = (dat.getDob().Insert(2, "/")).Insert(5,"/");
                dater = DateTime.Parse(date, culture);
                datePicker.Value = dater;
                if (dat.isVellore())
                    chk_Vellore.IsChecked = true;
                else
                    chk_Chennai.IsChecked = true;
                //App.ViewModel.isCache = true;
                //App.ViewModel.LoadData();
                
            }
            
        }

        void messagePrompt_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.Result != null)
            {
                App.ViewModel.isCache = false;
                App.ViewModel.LoadData();
               
            }
        }

        private void show_captcha()
        {
            MessagePrompt messagePrompt = new MessagePrompt();
            messagePrompt.Title = "Enter Captcha";
            messagePrompt.Body = new Captcha();
            messagePrompt.Completed += messagePrompt_Completed;
            messagePrompt.IsCancelVisible = true;
            messagePrompt.Show();
        }

        private void PageChanged(object sender, SelectionChangedEventArgs e) {
            switch (Controller.SelectedIndex) { 
                case 0 :
                    GoogleAnalytics.EasyTracker.GetTracker().SendView("AttendanceScreen");
                    refresh.Visibility = System.Windows.Visibility.Visible;
                    refresh.ImageSource = new BitmapImage(new Uri(@"/Toolkit.Content/ref.png" , UriKind.RelativeOrAbsolute));
                    break;
                case 1:
                    GoogleAnalytics.EasyTracker.GetTracker().SendView("TodayScreen");
                    refresh.ImageSource = new BitmapImage(new Uri(@"/Toolkit.Content/menu.png", UriKind.RelativeOrAbsolute));
                    refresh.Visibility = System.Windows.Visibility.Visible;
                    break;
                
                case 2:
                    GoogleAnalytics.EasyTracker.GetTracker().SendView("FriendsScreen");
                    refresh.ImageSource = new BitmapImage(new Uri(@"/Toolkit.Content/wrench.png", UriKind.RelativeOrAbsolute));
                    refresh.Visibility = System.Windows.Visibility.Visible;
                    break;
                
                default:
                    refresh.Visibility = System.Windows.Visibility.Collapsed;
                    break;

            }
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New) {
                if (newUser)
                {
                    //NavigationService.Navigate(new Uri("/WelcomeScreens/Page1.xaml", UriKind.Relative));
                    Controller.DefaultItem = Controller.Items[4];
                }
                else
                {
                    if (dat.getdefPage())
                    {
                        chk_att.IsChecked = true;
                        Controller.DefaultItem = Controller.Items[0];
                    }
                    else
                    {
                        chk_tod.IsChecked = true;
                        Controller.DefaultItem = Controller.Items[1];
                    }
                    App.ViewModel.isCache = true;
                    App.ViewModel.LoadData();
                }
            }
        }

        

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DateTime dater;
            String dob;
            dater = datePicker.Value.Value;
           
            if (txt_REG.Text.TrimEnd() != "") {
                dat.saveReg(txt_REG.Text.ToUpper());
                dob = checkDate(dater.Day.ToString()) + checkDate(dater.Month.ToString()) +dater.Year.ToString();
                
                if (chk_Vellore.IsChecked == true)
                    dat.setCampus(true);
                else
                    dat.setCampus(false);

                if (chk_att.IsChecked == true)
                    dat.setdefPage(true);
                else
                    dat.setdefPage(false);
                
                dat.saveDob(dob);
                show_captcha();
            }
        }

        public void MessageBox(String title , String message) {
            System.Windows.MessageBox.Show(message, title, MessageBoxButton.OK);
        }

        private string checkDate(String dat)
        {
            if (dat.Length == 1)
                dat = "0" + dat;
            return dat;
        }
        private void AttList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {

                ViewModels.ItemViewModel item = ((FrameworkElement)e.OriginalSource).DataContext
                                                                 as ViewModels.ItemViewModel;
                NavigationService.Navigate(new Uri("/subjectDetails.xaml?selectedItem=" + item.UID, UriKind.Relative));
            }
            catch (Exception) {}   
        }

        private void refresh_Checked(object sender, RoutedEventArgs e)
        {
            switch (Controller.SelectedIndex) { 
                case 0:
                    show_captcha();
                    break;
                case 1:
                    break;
                default:
                    break;
            }
            refresh.IsChecked = false;
        }

        private void adControl1_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Error.Message);
        }
    }
}