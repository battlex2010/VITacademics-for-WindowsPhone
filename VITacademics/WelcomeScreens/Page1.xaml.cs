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
    public partial class Page1 : PhoneApplicationPage
    {
        
        public Page1()
        {
           
            InitializeComponent();
            txt_cont.Text = "VITacademics now integrates with your Time Table and also let's you know where your friends are!"; 
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            NavigationService.RemoveBackEntry();
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/WelcomeScreens/Page3.xaml", UriKind.Relative));
            
        }
    }
}