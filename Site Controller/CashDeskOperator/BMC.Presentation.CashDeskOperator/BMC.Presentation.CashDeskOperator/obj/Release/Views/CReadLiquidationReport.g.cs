﻿#pragma checksum "..\..\..\Views\CReadLiquidationReport.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "68C40FEF313DFC3C57EAB397104801BF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BMC.Presentation.POS;
using BMC.Presentation.POS.Helper_classes;
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
    /// CReadLiquidationReport
    /// </summary>
    public partial class CReadLiquidationReport : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Views\CReadLiquidationReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal BMC.Presentation.CReadLiquidationReport UserControl;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\Views\CReadLiquidationReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\Views\CReadLiquidationReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chkLast20;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\Views\CReadLiquidationReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstReadReport;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\Views\CReadLiquidationReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrint;
        
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
            System.Uri resourceLocater = new System.Uri("/BMC.Presentation.POS;component/views/creadliquidationreport.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\CReadLiquidationReport.xaml"
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
            this.UserControl = ((BMC.Presentation.CReadLiquidationReport)(target));
            
            #line 13 "..\..\..\Views\CReadLiquidationReport.xaml"
            this.UserControl.Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.chkLast20 = ((System.Windows.Controls.CheckBox)(target));
            
            #line 117 "..\..\..\Views\CReadLiquidationReport.xaml"
            this.chkLast20.Checked += new System.Windows.RoutedEventHandler(this.chkLast20_Checked);
            
            #line default
            #line hidden
            
            #line 117 "..\..\..\Views\CReadLiquidationReport.xaml"
            this.chkLast20.Unchecked += new System.Windows.RoutedEventHandler(this.chkLast20_Unchecked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lstReadReport = ((System.Windows.Controls.ListView)(target));
            return;
            case 5:
            this.btnPrint = ((System.Windows.Controls.Button)(target));
            
            #line 139 "..\..\..\Views\CReadLiquidationReport.xaml"
            this.btnPrint.Click += new System.Windows.RoutedEventHandler(this.btnPrint_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
