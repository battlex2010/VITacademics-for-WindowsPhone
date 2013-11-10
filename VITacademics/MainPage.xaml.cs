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
                System.Diagnostics.Debug.WriteLine(date);
                dater = DateTime.Parse(date, culture);
                datePicker.Value = dater;
                App.ViewModel.isCache = true;
                App.ViewModel.LoadData();
            }
        }

        void messagePrompt_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            App.ViewModel.isCache = false;
            App.ViewModel.LoadData();
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

        private void PageChanged(object sender, SelectionChangedEventArgs e) { }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New) {
                if (newUser)
                {
                    Controller.DefaultItem = Controller.Items[3];
                }
                else
                {
                    App.ViewModel.isCache = true;
                    App.ViewModel.LoadData();
                }
            }
            /*if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }*/
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
    }
}