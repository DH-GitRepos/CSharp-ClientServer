﻿#pragma checksum "..\..\..\..\ViewAllMembers.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F32AE9DD7BC27516048B369B8F3F4897E97A8849"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DH_GUIClient;
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


namespace DH_GUIClient {
    
    
    /// <summary>
    /// ViewAllMembers
    /// </summary>
    public partial class ViewAllMembers : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\ViewAllMembers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CID_Label;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\ViewAllMembers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CID_ID;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\ViewAllMembers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ViewAllMembers_CloseWindow;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\ViewAllMembers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Members_Datagrid;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\ViewAllMembers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Main_SystemMessages;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\ViewAllMembers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Main_ErrorMessages;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\ViewAllMembers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Title_SystemMessages;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\ViewAllMembers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Title_ErrorMessages;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DH_GUIClient;component/viewallmembers.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ViewAllMembers.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.CID_Label = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.CID_ID = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.ViewAllMembers_CloseWindow = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\..\ViewAllMembers.xaml"
            this.ViewAllMembers_CloseWindow.Click += new System.Windows.RoutedEventHandler(this.CloseWindow);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Members_Datagrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.Main_SystemMessages = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.Main_ErrorMessages = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.Title_SystemMessages = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.Title_ErrorMessages = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

