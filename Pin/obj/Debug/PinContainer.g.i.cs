﻿#pragma checksum "..\..\PinContainer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AAFBA871BAA9827951C1B75B55DCA7AF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Pin;
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


namespace Pin {
    
    
    /// <summary>
    /// PinContainer
    /// </summary>
    public partial class PinContainer : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\PinContainer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid UI_Grid_MinimizedOpen;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\PinContainer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid UI_Grid_Maximized;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\PinContainer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup UI_Popup_Menu;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\PinContainer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid UI_Grid_MinimizedClosed;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\PinContainer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid UI_Grid_ProjectView;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\PinContainer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel UI_StackPanel_PinContainerProjects;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\PinContainer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock UI_TextBlock_FirstProject;
        
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
            System.Uri resourceLocater = new System.Uri("/Pin;component/pincontainer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PinContainer.xaml"
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
            
            #line 7 "..\..\PinContainer.xaml"
            ((Pin.PinContainer)(target)).DragEnter += new System.Windows.DragEventHandler(this.UserControl_DragEnter);
            
            #line default
            #line hidden
            return;
            case 2:
            this.UI_Grid_MinimizedOpen = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            
            #line 12 "..\..\PinContainer.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.UI_Btn_MouseDown_DragOut);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 15 "..\..\PinContainer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UI_Btn_Sizing_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 18 "..\..\PinContainer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UI_Btn_Exit_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 21 "..\..\PinContainer.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.UI_Btn_MouseDown_DragOut);
            
            #line default
            #line hidden
            return;
            case 7:
            this.UI_Grid_Maximized = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            
            #line 26 "..\..\PinContainer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UI_Btn_Pin_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 29 "..\..\PinContainer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UI_Btn_Menu_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 32 "..\..\PinContainer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UI_Btn_Sizing_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 35 "..\..\PinContainer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UI_Btn_Exit_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.UI_Popup_Menu = ((System.Windows.Controls.Primitives.Popup)(target));
            
            #line 41 "..\..\PinContainer.xaml"
            this.UI_Popup_Menu.MouseLeave += new System.Windows.Input.MouseEventHandler(this.UI_Popup_Menu_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 43 "..\..\PinContainer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.UI_Grid_MinimizedClosed = ((System.Windows.Controls.Grid)(target));
            return;
            case 15:
            this.UI_Grid_ProjectView = ((System.Windows.Controls.Grid)(target));
            return;
            case 16:
            
            #line 53 "..\..\PinContainer.xaml"
            ((System.Windows.Controls.StackPanel)(target)).DragEnter += new System.Windows.DragEventHandler(this.UI_UserControl_DragEnter);
            
            #line default
            #line hidden
            
            #line 53 "..\..\PinContainer.xaml"
            ((System.Windows.Controls.StackPanel)(target)).DragLeave += new System.Windows.DragEventHandler(this.UI_UserControl_DragLeave);
            
            #line default
            #line hidden
            return;
            case 17:
            this.UI_StackPanel_PinContainerProjects = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 18:
            this.UI_TextBlock_FirstProject = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

