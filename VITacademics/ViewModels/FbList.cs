using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace VITacademics.ViewModels
{
    public class FbList : INotifyPropertyChanged
    {
        private string fbName;

        public string Fb_Name {
            get { return fbName; }
            set { if (value != Fb_Name) { fbName = value; NotifyPropertyChanged("Fb_Name"); } }
        }

        private Uri fbPic;

        public Uri Fb_Pic {
            get { return fbPic; }
            set { if (value != Fb_Pic) { fbPic = value; NotifyPropertyChanged("Fb_Pic"); } }
        
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


}
