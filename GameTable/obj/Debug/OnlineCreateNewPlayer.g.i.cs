﻿#pragma checksum "..\..\OnlineCreateNewPlayer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7E7DB001CE2A54C4CC10296C04315251"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
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


namespace GameTable {
    
    
    /// <summary>
    /// OnlineCreateNewPlayer
    /// </summary>
    public partial class OnlineCreateNewPlayer : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\OnlineCreateNewPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxName;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\OnlineCreateNewPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxMyPort;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\OnlineCreateNewPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxRemotePort;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\OnlineCreateNewPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonConnect;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\OnlineCreateNewPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxRemoteIPAddress;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\OnlineCreateNewPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonDisconnect;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\OnlineCreateNewPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockInGame;
        
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
            System.Uri resourceLocater = new System.Uri("/GameTable;component/onlinecreatenewplayer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\OnlineCreateNewPlayer.xaml"
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
            this.TextBoxName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            
            #line 8 "..\..\OnlineCreateNewPlayer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TextBoxMyPort = ((System.Windows.Controls.TextBox)(target));
            
            #line 13 "..\..\OnlineCreateNewPlayer.xaml"
            this.TextBoxMyPort.Loaded += new System.Windows.RoutedEventHandler(this.TextBoxMyPort_Loaded);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TextBoxRemotePort = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.ButtonConnect = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\OnlineCreateNewPlayer.xaml"
            this.ButtonConnect.Click += new System.Windows.RoutedEventHandler(this.btnConnect_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TextBoxRemoteIPAddress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.ButtonDisconnect = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\OnlineCreateNewPlayer.xaml"
            this.ButtonDisconnect.Click += new System.Windows.RoutedEventHandler(this.ButtonDisconnect_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.TextBlockInGame = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
