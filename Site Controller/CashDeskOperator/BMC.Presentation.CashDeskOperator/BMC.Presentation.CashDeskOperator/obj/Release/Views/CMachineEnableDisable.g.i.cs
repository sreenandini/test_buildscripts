﻿#pragma checksum "..\..\..\Views\CMachineEnableDisable.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E6D536E07465925052F531FD8BCEF9B7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace BMC.Presentation.POS.Views {
    
    
    /// <summary>
    /// CMachineEnableDisable
    /// </summary>
    public partial class CMachineEnableDisable : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\Views\CMachineEnableDisable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textHeader;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Views\CMachineEnableDisable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstMachineDetails;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Views\CMachineEnableDisable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn CheckBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Views\CMachineEnableDisable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Machine_Name;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Views\CMachineEnableDisable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn BarPosName;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Views\CMachineEnableDisable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn MachineStatus;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\Views\CMachineEnableDisable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Message_;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\Views\CMachineEnableDisable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEnable;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\Views\CMachineEnableDisable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDisable;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\Views\CMachineEnableDisable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bttnSelectAll;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\Views\CMachineEnableDisable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bttnDeSelectAll;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BMC.Presentation.POS;component/views/cmachineenabledisable.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\CMachineEnableDisable.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 5 "..\..\..\Views\CMachineEnableDisable.xaml"
            ((BMC.Presentation.POS.Views.CMachineEnableDisable)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.textHeader = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.lstMachineDetails = ((System.Windows.Controls.ListView)(target));
            return;
            case 4:
            this.CheckBox = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 5:
            this.Machine_Name = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 6:
            this.BarPosName = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 7:
            this.MachineStatus = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 8:
            this.Message_ = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 9:
            this.btnEnable = ((System.Windows.Controls.Button)(target));
            
            #line 97 "..\..\..\Views\CMachineEnableDisable.xaml"
            this.btnEnable.Click += new System.Windows.RoutedEventHandler(this.btnEnable_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnDisable = ((System.Windows.Controls.Button)(target));
            
            #line 98 "..\..\..\Views\CMachineEnableDisable.xaml"
            this.btnDisable.Click += new System.Windows.RoutedEventHandler(this.btnDisable_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.bttnSelectAll = ((System.Windows.Controls.Button)(target));
            
            #line 99 "..\..\..\Views\CMachineEnableDisable.xaml"
            this.bttnSelectAll.Click += new System.Windows.RoutedEventHandler(this.bttnSelectAll_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.bttnDeSelectAll = ((System.Windows.Controls.Button)(target));
            
            #line 100 "..\..\..\Views\CMachineEnableDisable.xaml"
            this.bttnDeSelectAll.Click += new System.Windows.RoutedEventHandler(this.bttnDeSelectAll_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
