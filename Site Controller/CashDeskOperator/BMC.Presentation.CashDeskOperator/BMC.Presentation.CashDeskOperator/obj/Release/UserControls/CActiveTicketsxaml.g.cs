﻿#pragma checksum "..\..\..\UserControls\CActiveTicketsxaml.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "686D05542532BADD3F29D8193F59248A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AC.AvalonControlsLibrary.Controls;
using BMC.Presentation.POS.Helper_classes;
using Microsoft.Windows.Controls.Primitives;
using Microsoft.Windows.Themes;
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


namespace BMC.Presentation.CashDeskManager.UserControls {
    
    
    /// <summary>
    /// CActiveTicketsxaml
    /// </summary>
    public partial class CActiveTicketsxaml : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 5 "..\..\..\UserControls\CActiveTicketsxaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal BMC.Presentation.CashDeskManager.UserControls.CActiveTicketsxaml Window;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\UserControls\CActiveTicketsxaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid pnlHeader;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\UserControls\CActiveTicketsxaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnExit;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\UserControls\CActiveTicketsxaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbHeader;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\UserControls\CActiveTicketsxaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvActiveTickets;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\UserControls\CActiveTicketsxaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar prgActiveTickets;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\UserControls\CActiveTicketsxaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrint;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\UserControls\CActiveTicketsxaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnExport;
        
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
            System.Uri resourceLocater = new System.Uri("/BMC.Presentation.POS;component/usercontrols/cactiveticketsxaml.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\CActiveTicketsxaml.xaml"
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
            this.Window = ((BMC.Presentation.CashDeskManager.UserControls.CActiveTicketsxaml)(target));
            return;
            case 2:
            
            #line 11 "..\..\..\UserControls\CActiveTicketsxaml.xaml"
            ((System.Windows.Controls.UserControl)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 3:
            this.pnlHeader = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.btnExit = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\..\UserControls\CActiveTicketsxaml.xaml"
            this.btnExit.Click += new System.Windows.RoutedEventHandler(this.btnExit_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbHeader = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.lvActiveTickets = ((System.Windows.Controls.ListView)(target));
            return;
            case 7:
            this.prgActiveTickets = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 8:
            this.btnPrint = ((System.Windows.Controls.Button)(target));
            
            #line 88 "..\..\..\UserControls\CActiveTicketsxaml.xaml"
            this.btnPrint.Click += new System.Windows.RoutedEventHandler(this.btnPrint_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnExport = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\..\UserControls\CActiveTicketsxaml.xaml"
            this.btnExport.Click += new System.Windows.RoutedEventHandler(this.btnExport_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

