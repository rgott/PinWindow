﻿#pragma checksum "..\..\..\MenuContainer\PinContainer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CBE0D25C3276A3A765E2F725E7E27947"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Pin.MenuContainer;
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


namespace Pin.MenuContainer {
    
    
    /// <summary>
    /// Menu
    /// </summary>
    public partial class Menu : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\MenuContainer\PinContainer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Pin.MenuContainer.MinimizedOpen UI_Grid_MinimizedOpen;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\MenuContainer\PinContainer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Pin.MenuContainer.Maximized UI_Grid_Maximized;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\MenuContainer\PinContainer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Pin.MenuContainer.Minimized UI_Grid_MinimizedClosed;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\MenuContainer\PinContainer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Pin.MenuContainer.ProjectView UI_Grid_ProjectView;
        
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
            System.Uri resourceLocater = new System.Uri("/Pin;component/menucontainer/pincontainer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MenuContainer\PinContainer.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            
            #line 7 "..\..\..\MenuContainer\PinContainer.xaml"
            ((Pin.MenuContainer.Menu)(target)).DragEnter += new System.Windows.DragEventHandler(this.UserControl_DragEnter);
            
            #line default
            #line hidden
            return;
            case 2:
            this.UI_Grid_MinimizedOpen = ((Pin.MenuContainer.MinimizedOpen)(target));
            return;
            case 3:
            this.UI_Grid_Maximized = ((Pin.MenuContainer.Maximized)(target));
            return;
            case 4:
            this.UI_Grid_MinimizedClosed = ((Pin.MenuContainer.Minimized)(target));
            return;
            case 5:
            this.UI_Grid_ProjectView = ((Pin.MenuContainer.ProjectView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

