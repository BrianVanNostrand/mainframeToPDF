﻿#pragma checksum "..\..\..\Views\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0C1E19D9E5D64303E63FFD92028B0251BE7CA7399A77BFEE7F9B8EC7400D2012"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using mf2pdfWpfFramework;


namespace mf2pdfWpfFramework {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel mainFormGrid;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addJobbutton;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button loadSampleButton;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button clearJobsButton;
        
        #line default
        #line hidden
        
        /// <summary>
        /// jobsItemsControl Name Field
        /// </summary>
        
        #line 26 "..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public System.Windows.Controls.ListBox jobsItemsControl;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button changeTemplateDefaultsButton;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button runButton;
        
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
            System.Uri resourceLocater = new System.Uri("/mf2pdfWpfFramework;component/views/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\MainWindow.xaml"
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
            
            #line 9 "..\..\..\Views\MainWindow.xaml"
            ((mf2pdfWpfFramework.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.MainWindow_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mainFormGrid = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.addJobbutton = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\Views\MainWindow.xaml"
            this.addJobbutton.Click += new System.Windows.RoutedEventHandler(this.addJobbutton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.loadSampleButton = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\Views\MainWindow.xaml"
            this.loadSampleButton.Click += new System.Windows.RoutedEventHandler(this.loadSampleButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.clearJobsButton = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\Views\MainWindow.xaml"
            this.clearJobsButton.Click += new System.Windows.RoutedEventHandler(this.clearJobsButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.jobsItemsControl = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.changeTemplateDefaultsButton = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\Views\MainWindow.xaml"
            this.changeTemplateDefaultsButton.Click += new System.Windows.RoutedEventHandler(this.changeTemplateDefaultsButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.runButton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\Views\MainWindow.xaml"
            this.runButton.Click += new System.Windows.RoutedEventHandler(this.runButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
