﻿#pragma checksum "..\..\..\..\Pages\NavigationPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DDBCF6B91CDA5116AA72CA2D65C21E98B41410E4"
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
    /// Navigation
    /// </summary>
    public partial class Navigation : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\Pages\NavigationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Loans_BorrowBook;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\Pages\NavigationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Loans_ReturnBook;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\Pages\NavigationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Loans_RenewLoan;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Pages\NavigationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Reports_ViewAllBooks;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Pages\NavigationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Reports_ViewAllMembers;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Pages\NavigationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Reports_ViewAllLoans;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\Pages\NavigationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Main_ExitApplication;
        
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
            System.Uri resourceLocater = new System.Uri("/DH_GUIClient;component/pages/navigationpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\NavigationPage.xaml"
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
            this.Loans_BorrowBook = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\..\Pages\NavigationPage.xaml"
            this.Loans_BorrowBook.Click += new System.Windows.RoutedEventHandler(this.Click_BorrowBook);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Loans_ReturnBook = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\..\Pages\NavigationPage.xaml"
            this.Loans_ReturnBook.Click += new System.Windows.RoutedEventHandler(this.Click_ReturnBook);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Loans_RenewLoan = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\..\Pages\NavigationPage.xaml"
            this.Loans_RenewLoan.Click += new System.Windows.RoutedEventHandler(this.Click_RenewLoan);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Reports_ViewAllBooks = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\..\Pages\NavigationPage.xaml"
            this.Reports_ViewAllBooks.Click += new System.Windows.RoutedEventHandler(this.Click_ViewAllBooks);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Reports_ViewAllMembers = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\..\Pages\NavigationPage.xaml"
            this.Reports_ViewAllMembers.Click += new System.Windows.RoutedEventHandler(this.Click_ViewAllMembers);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Reports_ViewAllLoans = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\..\Pages\NavigationPage.xaml"
            this.Reports_ViewAllLoans.Click += new System.Windows.RoutedEventHandler(this.Click_ViewAllLoans);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Main_ExitApplication = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\Pages\NavigationPage.xaml"
            this.Main_ExitApplication.Click += new System.Windows.RoutedEventHandler(this.Click_ExitApplication);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
