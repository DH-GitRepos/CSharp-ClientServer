﻿#pragma checksum "..\..\..\BorrowBookWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "16E03280EC78B30B1EEA93D7197A41112C9E0176"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
    /// BorrowBookWindow
    /// </summary>
    public partial class BorrowBookWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\BorrowBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CID_Label;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\BorrowBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CID_ID;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\BorrowBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Members_Datagrid;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\BorrowBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Books_Datagrid;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\BorrowBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BorrowBook_BorrowBook;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\BorrowBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BorrowBook_CloseWindow;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\BorrowBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Main_SystemMessages;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\BorrowBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Main_ErrorMessages;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\BorrowBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Title_SystemMessages;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\BorrowBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Title_ErrorMessages;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DH_GUIClient;component/borrowbookwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\BorrowBookWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
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
            this.Members_Datagrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.Books_Datagrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.BorrowBook_BorrowBook = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\BorrowBookWindow.xaml"
            this.BorrowBook_BorrowBook.Click += new System.Windows.RoutedEventHandler(this.BorrowBook_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BorrowBook_CloseWindow = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\BorrowBookWindow.xaml"
            this.BorrowBook_CloseWindow.Click += new System.Windows.RoutedEventHandler(this.CloseWindow_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Main_SystemMessages = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.Main_ErrorMessages = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.Title_SystemMessages = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.Title_ErrorMessages = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

