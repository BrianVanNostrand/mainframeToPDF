// Updated by XamlIntelliSenseFileGenerator 10/28/2020 6:10:27 PM
#pragma checksum "..\..\jobUserControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2D3CA605B83EB3CD3D9D4976D45DDBD95E07075C70E795DC9E2A0B8DD7D16F93"
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


namespace mf2pdfWpfFramework
{


    /// <summary>
    /// jobUserControl
    /// </summary>
    public partial class jobUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {


#line 19 "..\..\jobUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox reportTitleTextbox;

#line default
#line hidden


#line 20 "..\..\jobUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteJobButton;

#line default
#line hidden


#line 46 "..\..\jobUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton highwayLogRadio;

#line default
#line hidden


#line 47 "..\..\jobUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton locatorLogRadio;

#line default
#line hidden


#line 63 "..\..\jobUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton topBottomRadio;

#line default
#line hidden


#line 64 "..\..\jobUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton bottomTopRadio;

#line default
#line hidden


#line 77 "..\..\jobUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button inputFileButton;

#line default
#line hidden


#line 78 "..\..\jobUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox inputFileTextBox;

#line default
#line hidden


#line 88 "..\..\jobUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button outputFileButton;

#line default
#line hidden


#line 89 "..\..\jobUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox outputFileTextBox;

#line default
#line hidden


#line 92 "..\..\jobUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock jobProgressBarLabel;

#line default
#line hidden


#line 94 "..\..\jobUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button pdfCreateResponseButton;

#line default
#line hidden


#line 95 "..\..\jobUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image jobStatusImage;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/mf2pdfWpfFramework;component/jobusercontrol.xaml", System.UriKind.Relative);

#line 1 "..\..\jobUserControl.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.reportTitleTextbox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 2:
                    this.deleteJobButton = ((System.Windows.Controls.Button)(target));

#line 20 "..\..\jobUserControl.xaml"
                    this.deleteJobButton.Click += new System.Windows.RoutedEventHandler(this.deleteJobButton_Click);

#line default
#line hidden
                    return;
                case 3:
                    this.highwayLogRadio = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 4:
                    this.locatorLogRadio = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 5:
                    this.topBottomRadio = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 6:
                    this.bottomTopRadio = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 7:
                    this.inputFileButton = ((System.Windows.Controls.Button)(target));

#line 77 "..\..\jobUserControl.xaml"
                    this.inputFileButton.Click += new System.Windows.RoutedEventHandler(this.inputFileButton_Click);

#line default
#line hidden
                    return;
                case 8:
                    this.inputFileTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 9:
                    this.outputFileButton = ((System.Windows.Controls.Button)(target));

#line 88 "..\..\jobUserControl.xaml"
                    this.outputFileButton.Click += new System.Windows.RoutedEventHandler(this.outputFileButton_Click);

#line default
#line hidden
                    return;
                case 10:
                    this.outputFileTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 11:
                    this.jobProgressBarLabel = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 12:
                    this.pdfCreateResponseButton = ((System.Windows.Controls.Button)(target));
                    return;
                case 13:
                    this.jobStatusImage = ((System.Windows.Controls.Image)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.ProgressBar jobProgressBar;
    }
}

