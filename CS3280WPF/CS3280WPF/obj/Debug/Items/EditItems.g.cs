﻿#pragma checksum "..\..\..\Items\EditItems.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "93A3F0B3DF300927CC181818F38DA475"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CS3280WPF.Items;
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


namespace CS3280WPF.Items {
    
    
    /// <summary>
    /// EditItems
    /// </summary>
    public partial class EditItems : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Items\EditItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid itemDataGrid;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Items\EditItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox itemCodeTextBox;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Items\EditItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox itemDescTextBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Items\EditItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox itemPriceTextBox;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Items\EditItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addItemButton;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Items\EditItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button editItemButton;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Items\EditItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button removeItemButton;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Items\EditItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button clearSelectionButton;
        
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
            System.Uri resourceLocater = new System.Uri("/CS3280WPF;component/items/edititems.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Items\EditItems.xaml"
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
            this.itemDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 13 "..\..\..\Items\EditItems.xaml"
            this.itemDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.itemDataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.itemCodeTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.itemDescTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.itemPriceTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.addItemButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Items\EditItems.xaml"
            this.addItemButton.Click += new System.Windows.RoutedEventHandler(this.addItemButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.editItemButton = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\Items\EditItems.xaml"
            this.editItemButton.Click += new System.Windows.RoutedEventHandler(this.editItemButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.removeItemButton = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\Items\EditItems.xaml"
            this.removeItemButton.Click += new System.Windows.RoutedEventHandler(this.removeItemButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.clearSelectionButton = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\Items\EditItems.xaml"
            this.clearSelectionButton.Click += new System.Windows.RoutedEventHandler(this.addItemButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
