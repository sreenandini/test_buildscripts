﻿#pragma checksum "..\..\..\Views\CPositionCurrentStatus.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5562C6715A3D02DB4F3B64ABF255F5AF"
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


namespace BMC.Presentation {
    
    
    /// <summary>
    /// CPositionCurrentStatus
    /// </summary>
    public partial class CPositionCurrentStatus : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\Views\CPositionCurrentStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal BMC.Presentation.CPositionCurrentStatus UserControl;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\..\Views\CPositionCurrentStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstPosCurrent;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\..\Views\CPositionCurrentStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton chkVLTAAMSVerification;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\..\Views\CPositionCurrentStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton chkVLTAAMSStatus;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\..\Views\CPositionCurrentStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton chkGameVerification;
        
        #line default
        #line hidden
        
        
        #line 171 "..\..\..\Views\CPositionCurrentStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton chkGameInstallAAMS;
        
        #line default
        #line hidden
        
        
        #line 172 "..\..\..\Views\CPositionCurrentStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton chkGameEnableAAMS;
        
        #line default
        #line hidden
        
        
        #line 173 "..\..\..\Views\CPositionCurrentStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton chkAAMSEnableDiable;
        
        #line default
        #line hidden
        
        
        #line 174 "..\..\..\Views\CPositionCurrentStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton chkBMCEnterpriseStatus;
        
        #line default
        #line hidden
        
        
        #line 175 "..\..\..\Views\CPositionCurrentStatus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton chkAll;
        
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
            System.Uri resourceLocater = new System.Uri("/BMC.Presentation.POS;component/views/cpositioncurrentstatus.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\CPositionCurrentStatus.xaml"
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
            this.UserControl = ((BMC.Presentation.CPositionCurrentStatus)(target));
            return;
            case 2:
            this.lstPosCurrent = ((System.Windows.Controls.ListView)(target));
            return;
            case 3:
            this.chkVLTAAMSVerification = ((System.Windows.Controls.RadioButton)(target));
            
            #line 168 "..\..\..\Views\CPositionCurrentStatus.xaml"
            this.chkVLTAAMSVerification.Click += new System.Windows.RoutedEventHandler(this.CheckBoxes_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.chkVLTAAMSStatus = ((System.Windows.Controls.RadioButton)(target));
            
            #line 169 "..\..\..\Views\CPositionCurrentStatus.xaml"
            this.chkVLTAAMSStatus.Click += new System.Windows.RoutedEventHandler(this.CheckBoxes_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.chkGameVerification = ((System.Windows.Controls.RadioButton)(target));
            
            #line 170 "..\..\..\Views\CPositionCurrentStatus.xaml"
            this.chkGameVerification.Click += new System.Windows.RoutedEventHandler(this.CheckBoxes_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.chkGameInstallAAMS = ((System.Windows.Controls.RadioButton)(target));
            
            #line 171 "..\..\..\Views\CPositionCurrentStatus.xaml"
            this.chkGameInstallAAMS.Click += new System.Windows.RoutedEventHandler(this.CheckBoxes_Checked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.chkGameEnableAAMS = ((System.Windows.Controls.RadioButton)(target));
            
            #line 172 "..\..\..\Views\CPositionCurrentStatus.xaml"
            this.chkGameEnableAAMS.Click += new System.Windows.RoutedEventHandler(this.CheckBoxes_Checked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.chkAAMSEnableDiable = ((System.Windows.Controls.RadioButton)(target));
            
            #line 173 "..\..\..\Views\CPositionCurrentStatus.xaml"
            this.chkAAMSEnableDiable.Click += new System.Windows.RoutedEventHandler(this.CheckBoxes_Checked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.chkBMCEnterpriseStatus = ((System.Windows.Controls.RadioButton)(target));
            
            #line 174 "..\..\..\Views\CPositionCurrentStatus.xaml"
            this.chkBMCEnterpriseStatus.Click += new System.Windows.RoutedEventHandler(this.CheckBoxes_Checked);
            
            #line default
            #line hidden
            return;
            case 10:
            this.chkAll = ((System.Windows.Controls.RadioButton)(target));
            
            #line 175 "..\..\..\Views\CPositionCurrentStatus.xaml"
            this.chkAll.Click += new System.Windows.RoutedEventHandler(this.CheckBoxes_Checked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
