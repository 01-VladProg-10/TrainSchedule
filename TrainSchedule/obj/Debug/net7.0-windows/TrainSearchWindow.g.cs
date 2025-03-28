﻿#pragma checksum "..\..\..\TrainSearchWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DD2D9469D1D4F3EDD67C846AC3AAF24540AC298F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TrainSchedule.Converters;


namespace TrainSchedule {
    
    
    /// <summary>
    /// TrainSearchWindow
    /// </summary>
    public partial class TrainSearchWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 135 "..\..\..\TrainSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FromComboBox;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\TrainSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ToComboBox;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\..\TrainSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker TravelDatePicker;
        
        #line default
        #line hidden
        
        
        #line 156 "..\..\..\TrainSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TimeButton;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\..\TrainSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup TimePopup;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\..\TrainSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock HourText;
        
        #line default
        #line hidden
        
        
        #line 179 "..\..\..\TrainSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MinuteText;
        
        #line default
        #line hidden
        
        
        #line 199 "..\..\..\TrainSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView TrainListView;
        
        #line default
        #line hidden
        
        
        #line 251 "..\..\..\TrainSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock UserInitialTextBlock;
        
        #line default
        #line hidden
        
        
        #line 256 "..\..\..\TrainSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock UserNameTextBlock;
        
        #line default
        #line hidden
        
        
        #line 263 "..\..\..\TrainSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddTrainButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TrainSchedule;component/trainsearchwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\TrainSearchWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.FromComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 136 "..\..\..\TrainSearchWindow.xaml"
            this.FromComboBox.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(this.FromComboBox_TextChanged));
            
            #line default
            #line hidden
            
            #line 137 "..\..\..\TrainSearchWindow.xaml"
            this.FromComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.FromComboBox_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 138 "..\..\..\TrainSearchWindow.xaml"
            this.FromComboBox.DropDownOpened += new System.EventHandler(this.FromComboBox_DropDownOpened);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ToComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 146 "..\..\..\TrainSearchWindow.xaml"
            this.ToComboBox.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(this.ToComboBox_TextChanged));
            
            #line default
            #line hidden
            
            #line 147 "..\..\..\TrainSearchWindow.xaml"
            this.ToComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ToComboBox_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 148 "..\..\..\TrainSearchWindow.xaml"
            this.ToComboBox.DropDownOpened += new System.EventHandler(this.ToComboBox_DropDownOpened);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TravelDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 4:
            this.TimeButton = ((System.Windows.Controls.Button)(target));
            
            #line 156 "..\..\..\TrainSearchWindow.xaml"
            this.TimeButton.Click += new System.Windows.RoutedEventHandler(this.ShowTimePopup_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 158 "..\..\..\TrainSearchWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SearchButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TimePopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 7:
            
            #line 169 "..\..\..\TrainSearchWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.IncreaseHour_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.HourText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            
            #line 171 "..\..\..\TrainSearchWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DecreaseHour_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 178 "..\..\..\TrainSearchWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.IncreaseMinute_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.MinuteText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            
            #line 180 "..\..\..\TrainSearchWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DecreaseMinute_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 185 "..\..\..\TrainSearchWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ConfirmTime_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.TrainListView = ((System.Windows.Controls.ListView)(target));
            
            #line 200 "..\..\..\TrainSearchWindow.xaml"
            this.TrainListView.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.TrainListView_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 200 "..\..\..\TrainSearchWindow.xaml"
            this.TrainListView.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TrainListView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 17:
            this.UserInitialTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 18:
            this.UserNameTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 19:
            this.AddTrainButton = ((System.Windows.Controls.Button)(target));
            
            #line 264 "..\..\..\TrainSearchWindow.xaml"
            this.AddTrainButton.Click += new System.Windows.RoutedEventHandler(this.AddTrainButton_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 267 "..\..\..\TrainSearchWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LogOutButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 15:
            
            #line 217 "..\..\..\TrainSearchWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditTrainButton_Click);
            
            #line default
            #line hidden
            break;
            case 16:
            
            #line 221 "..\..\..\TrainSearchWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteTrainButton_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

