﻿#pragma checksum "C:\Users\Rafal\Documents\projects\windows_phone_SJP\SlownikJezykaSlaskiego\StoryDetails.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CF64C1048CC9090A972EED1833654634"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
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


namespace SlownikJezykaSlaskiego {
    
    
    public partial class StoryDetails : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock TxtTitle;
        
        internal System.Windows.Controls.RichTextBox txtDescription;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/SlownikJezykaSlaskiego;component/StoryDetails.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TxtTitle = ((System.Windows.Controls.TextBlock)(this.FindName("TxtTitle")));
            this.txtDescription = ((System.Windows.Controls.RichTextBox)(this.FindName("txtDescription")));
        }
    }
}

