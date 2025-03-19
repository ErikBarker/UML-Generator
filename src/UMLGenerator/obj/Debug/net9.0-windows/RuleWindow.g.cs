﻿#pragma checksum "..\..\..\RuleWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CA6EF939FBD6D6863A2DDC95AE278A2AED401622"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
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
using System.Windows.Controls.Ribbon;
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
using UMLGenerator;


namespace UMLGenerator {
    
    
    /// <summary>
    /// RuleWindow
    /// </summary>
    public partial class RuleWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\RuleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainRuleGrid;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\RuleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer RuleScrollBar;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\RuleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ButtonPanel;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\RuleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid RuleSetEditorHeader;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\RuleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer RuleSetScrollBar;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\RuleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel RuleSetPanel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UMLGenerator;component/rulewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\RuleWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainRuleGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.RuleScrollBar = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 3:
            this.ButtonPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.RuleSetEditorHeader = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            
            #line 39 "..\..\..\RuleWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CreateNewRuleSet_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 40 "..\..\..\RuleWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveRuleSet_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 41 "..\..\..\RuleWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteRuleSet_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.RuleSetScrollBar = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 9:
            this.RuleSetPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

