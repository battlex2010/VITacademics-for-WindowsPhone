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
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _UID;

        public string UID {
            get {
                return _UID;
            }
            set {
                if (value != _UID) {
                    _UID = value;
                    NotifyPropertyChanged("UID");
                }
            }
        }
        private string _lineOne;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Title
        {
            get
            {
                return _lineOne;
            }
            set
            {
                if (value != _lineOne)
                {
                    _lineOne = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private string _lineTwo;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Slot
        {
            get
            {
                return _lineTwo;
            }
            set
            {
                if (value != _lineTwo)
                {
                    _lineTwo = value;
                    NotifyPropertyChanged("Slot");
                }
            }
        }

        private string _lineThree;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Type
        {
            get
            {
                return _lineThree;
            }
            set
            {
                if (value != _lineThree)
                {
                    _lineThree = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        private int prg_val;

        public int prgVal {
            get
            {
                return prg_val;
            }
            set {
                if (value != prg_val) {
                    prg_val = value;
                    NotifyPropertyChanged("prgVal");
                }
            }
        }

        private string _percent;

        public string Percentage {
            get {
                return _percent;
            }
            set
            {
                if (value != _percent) {
                    _percent = value;
                    NotifyPropertyChanged("Percentage");
                }
            }
        }

        private System.Windows.Media.Brush prg_color;

        public System.Windows.Media.Brush prgColor
        {
            get { return prg_color; }
            set{
                if (value != prg_color)
                {
                    prg_color = value;
                    NotifyPropertyChanged("prgColor");
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