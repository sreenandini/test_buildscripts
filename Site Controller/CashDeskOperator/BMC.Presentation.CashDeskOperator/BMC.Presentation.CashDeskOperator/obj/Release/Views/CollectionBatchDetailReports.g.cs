﻿#pragma checksum "..\..\..\Views\CollectionBatchDetailReports.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C7F7EE4BF72C300D663A46219B836773"
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


namespace BMC.Presentation.POS {
    
    
    /// <summary>
    /// CollectionBatchDetailReports
    /// </summary>
    public partial class CollectionBatchDetailReports : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\Views\CollectionBatchDetailReports.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Root;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Views\CollectionBatchDetailReports.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid pnlHeader;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Views\CollectionBatchDetailReports.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbHeader;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Views\CollectionBatchDetailReports.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboReports;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Views\CollectionBatchDetailReports.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGenerate;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Views\CollectionBatchDetailReports.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
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
            System.Uri resourceLocater = new System.Uri("/BMC.Presentation.POS;component/views/collectionbatchdetailreports.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\CollectionBatchDetailReports.xaml"
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
            
            #line 5 "..\..\..\Views\CollectionBatchDetailReports.xaml"
            ((BMC.Presentation.POS.CollectionBatchDetailReports)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Root = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.pnlHeader = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.tbHeader = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.cboReports = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.btnGenerate = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\Views\CollectionBatchDetailReports.xaml"
            this.btnGenerate.Click += new System.Windows.RoutedEventHandler(this.btnGenerate_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\Views\CollectionBatchDetailReports.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

