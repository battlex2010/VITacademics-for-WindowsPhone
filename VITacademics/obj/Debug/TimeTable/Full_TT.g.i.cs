﻿#pragma checksum "C:\Users\Saurabh\Documents\Visual Studio 2012\Projects\VITacademics\VITacademics\TimeTable\Full_TT.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AE12EE54C649956ACEBC5B5FA0FAA23F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace VITacademics.TimeTable {
    
    
    public partial class Full_TT : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Panorama Controller;
        
        internal System.Windows.Controls.ListBox tt_monday;
        
        internal System.Windows.Controls.ListBox tt_tuesday;
        
        internal System.Windows.Controls.ListBox tt_wednesday;
        
        internal System.Windows.Controls.ListBox tt_thursday;
        
        internal System.Windows.Controls.ListBox tt_friday;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/VITacademics;component/TimeTable/Full_TT.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.Controller = ((Microsoft.Phone.Controls.Panorama)(this.FindName("Controller")));
            this.tt_monday = ((System.Windows.Controls.ListBox)(this.FindName("tt_monday")));
            this.tt_tuesday = ((System.Windows.Controls.ListBox)(this.FindName("tt_tuesday")));
            this.tt_wednesday = ((System.Windows.Controls.ListBox)(this.FindName("tt_wednesday")));
            this.tt_thursday = ((System.Windows.Controls.ListBox)(this.FindName("tt_thursday")));
            this.tt_friday = ((System.Windows.Controls.ListBox)(this.FindName("tt_friday")));
        }
    }
}

