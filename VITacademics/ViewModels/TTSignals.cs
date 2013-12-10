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
    public class TTSignals : INotifyPropertyChanged
    {
        private string tt_title;

        public string TT_Title { 
            get
            {
                return tt_title;
            
            }
            set {
                if (value != TT_Title) {
                    tt_title = value;
                    NotifyPropertyChanged("TT_Title");
                }
            }
        }

        private string tt_slot;

        public string TT_Slot { 
            get 
            {
                return tt_slot;

            }
            set{
                if (value != TT_Slot) {
                    tt_slot = value; NotifyPropertyChanged("TT_Slot");}
            } 
        }

        private string tt_time;

        public string TT_Time
        {
            get
            {
                return tt_time;

            }
            set
            {
                if (value != TT_Time)
                {
                    tt_time = value; NotifyPropertyChanged("TT_Time");
                }
            }
        }

        private string tt_att;
        public string TT_Att
        {
            get
            {
                return tt_att;

            }
            set
            {
                if (value != TT_Att)
                {
                    tt_att = value;
                    NotifyPropertyChanged("TT_Att");
                }
            }
        }

        private string tt_pre;
        public string TT_Pre
        {
            get
            {
                return tt_pre;

            }
            set
            {
                if (value != TT_Pre)
                {
                    tt_pre = value;
                    NotifyPropertyChanged("TT_Pre");
                }
            }
        }

        private string tt_abs;
        public string TT_Abs
        {
            get
            {
                return tt_abs;

            }
            set
            {
                if (value != TT_Abs)
                {
                    tt_abs = value;
                    NotifyPropertyChanged("TT_Abs");
                }
            }
        }

        private System.Windows.Media.Brush att_color;

        public System.Windows.Media.Brush Att_Color
        {
            get { return att_color; }
            set
            {
                if (value != Att_Color)
                {
                    att_color = value;
                    NotifyPropertyChanged("Att_Color");
                }
            }
        }

        private System.Windows.Media.Brush pre_color;

        public System.Windows.Media.Brush Pre_Color
        {
            get { return pre_color; }
            set
            {
                if (value != Pre_Color)
                {
                    pre_color = value;
                    NotifyPropertyChanged("Pre_Color");
                }
            }
        }

        private System.Windows.Media.Brush abs_color;
        public System.Windows.Media.Brush Abs_Color
        {
            get { return abs_color; }
            set
            {
                if (value != Abs_Color)
                {
                    abs_color = value;
                    NotifyPropertyChanged("Abs_Color");
                }
            }
        }

        private System.Windows.Media.Brush title_color;
        public System.Windows.Media.Brush Title_Color
        {
            get { return title_color; }
            set
            {
                if (value != Title_Color)
                {
                    title_color = value;
                    NotifyPropertyChanged("Title_Color");
                }
            }
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
