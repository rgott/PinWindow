﻿#pragma checksum "..\..\..\View(1)\menuBlock_Items.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7D2CB759C126EA568CA66AA1AEC4FC7E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Pin.View;
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


namespace Pin.View {
    
    
    /// <summary>
    /// menuBlock_Items
    /// </summary>
    public partial class menuBlock_Items : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\View(1)\menuBlock_Items.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PinnedHolder;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\View(1)\menuBlock_Items.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button pin_btn;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\View(1)\menuBlock_Items.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image pin_btn_image;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\View(1)\menuBlock_Items.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button menu_btn;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\View(1)\menuBlock_Items.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sizing_btn;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\View(1)\menuBlock_Items.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image sizing_btn_image;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\View(1)\menuBlock_Items.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exit_btn;
        
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
            System.Uri resourceLocater = new System.Uri("/Pin;component/view(1)/menublock_items.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View(1)\menuBlock_Items.xaml"
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
            this.PinnedHolder = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.pin_btn = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\View(1)\menuBlock_Items.xaml"
            this.pin_btn.Click += new System.Windows.RoutedEventHandler(this.pin_btn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.pin_btn_image = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.menu_btn = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.sizing_btn = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\View(1)\menuBlock_Items.xaml"
            this.sizing_btn.Click += new System.Windows.RoutedEventHandler(this.sizing_btn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.sizing_btn_image = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            this.exit_btn = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\View(1)\menuBlock_Items.xaml"
            this.exit_btn.Click += new System.Windows.RoutedEventHandler(this.exit_btn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

