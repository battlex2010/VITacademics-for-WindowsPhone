using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Parse;
using System.Threading.Tasks;

namespace VITacademics.WelcomeScreens
{
    public partial class Page3 : PhoneApplicationPage
    {
        public Page3()
        {
            InitializeComponent();
            browser.ClearInternetCacheAsync();
            
        }

        public async void  LoadFB() {
            try
            {
                ParseUser user = await ParseFacebookUtils.LogInAsync(browser, null);
                NavigationService.Navigate(new Uri("/WelcomeScreens/Page2.xaml", UriKind.Relative));
                browser.Visibility = Visibility.Collapsed;
            }
            catch
            {
                btn_login.Visibility = Visibility.Visible;
                browser.Visibility = Visibility.Collapsed;
                //cancelled?
            }
            
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadFB();
            //#btn_refresh.Visibility = Visibility.Visible; (Acts wierd)
            btn_login.Visibility = Visibility.Collapsed;
            browser.Visibility = Visibility.Visible;
        }

        private void btn_skip_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/WelcomeScreens/Page2.xaml", UriKind.Relative));
        }



      
    }
}