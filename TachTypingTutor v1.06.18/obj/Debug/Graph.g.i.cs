﻿#pragma checksum "..\..\Graph.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "27767962B6DA09D664D5026EAF40B1F38E1A29DA"
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
using TachTypingTutor_v1._06._18;


namespace TachTypingTutor_v1._06._18 {
    
    
    /// <summary>
    /// Graph
    /// </summary>
    public partial class Graph : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\Graph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TachTypingTutor_v1._06._18.Graph @this;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\Graph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border brdColors;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\Graph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stkpColors;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\Graph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle recTrueAccuracy;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\Graph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle recAccuracy;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\Graph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle recSpeed;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\Graph.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas can;
        
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
            System.Uri resourceLocater = new System.Uri("/TachTypingTutor v1.06.18;component/graph.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Graph.xaml"
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
            this.@this = ((TachTypingTutor_v1._06._18.Graph)(target));
            return;
            case 2:
            this.brdColors = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.stkpColors = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.recTrueAccuracy = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 78 "..\..\Graph.xaml"
            this.recTrueAccuracy.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Rec_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.recAccuracy = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 82 "..\..\Graph.xaml"
            this.recAccuracy.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Rec_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.recSpeed = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 86 "..\..\Graph.xaml"
            this.recSpeed.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Rec_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.can = ((System.Windows.Controls.Canvas)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

