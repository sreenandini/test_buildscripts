﻿#pragma checksum "..\..\..\Views\CAttendantPays.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6E6558054812F66C49D9170F07A21844"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BMC.CoreLib.WPF.Controls;
using BMC.Presentation;
using BMC.Presentation.POS;
using BMC.Presentation.POS.Helper_classes;
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
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


namespace BMC.Presentation {
    
    
    /// <summary>
    /// CAttendantPays
    /// </summary>
    public partial class CAttendantPays : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\Views\CAttendantPays.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal BMC.Presentation.CAttendantPays AttendantPay;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Views\CAttendantPays.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Views\CAttendantPays.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GHandpay;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Views\CAttendantPays.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Windows.Controls.DataGrid dgHandpay;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\Views\CAttendantPays.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnProcess;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Views\CAttendantPays.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnVoid;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\Views\CAttendantPays.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGenerateSlipNo;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\Views\CAttendantPays.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlock_11;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\Views\CAttendantPays.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAmount;
        
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
            System.Uri resourceLocater = new System.Uri("/BMC.Presentation.POS;component/views/cattendantpays.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\CAttendantPays.xaml"
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
            this.AttendantPay = ((BMC.Presentation.CAttendantPays)(target));
            
            #line 13 "..\..\..\Views\CAttendantPays.xaml"
            this.AttendantPay.Loaded += new System.Windows.RoutedEventHandler(this.AttendantPay_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.GHandpay = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.dgHandpay = ((Microsoft.Windows.Controls.DataGrid)(target));
            
            #line 53 "..\..\..\Views\CAttendantPays.xaml"
            this.dgHandpay.Loaded += new System.Windows.RoutedEventHandler(this.dgHandpay_Loaded);
            
            #line default
            #line hidden
            
            #line 53 "..\..\..\Views\CAttendantPays.xaml"
            this.dgHandpay.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgHandpay_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnProcess = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\..\Views\CAttendantPays.xaml"
            this.btnProcess.Click += new System.Windows.RoutedEventHandler(this.btnProcess_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnVoid = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\Views\CAttendantPays.xaml"
            this.btnVoid.Click += new System.Windows.RoutedEventHandler(this.btnVoid_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnGenerateSlipNo = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\..\Views\CAttendantPays.xaml"
            this.btnGenerateSlipNo.Click += new System.Windows.RoutedEventHandler(this.btnGenerateSlipNo_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.TextBlock_11 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.txtAmount = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
