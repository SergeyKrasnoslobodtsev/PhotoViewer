﻿#pragma checksum "..\..\..\..\Views\Photo\GroupsThumbnail.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "40E2AB23F980A8C73FB8A49A0D6A54CE1D26F993564168F77B8B1B5A59ED07E5"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ModernWpf;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using ModernWpf.DesignTime;
using ModernWpf.Markup;
using ModernWpf.Media.Animation;
using PhotoViewer.App.Views;
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
using System.Windows.Interactivity;
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


namespace PhotoViewer.App.Views {
    
    
    /// <summary>
    /// GroupsThumbnail
    /// </summary>
    public partial class GroupsThumbnail : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\Views\Photo\GroupsThumbnail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid stack;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Views\Photo\GroupsThumbnail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton tg_group;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Views\Photo\GroupsThumbnail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView list;
        
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
            System.Uri resourceLocater = new System.Uri("/PhotoViewer.App;component/views/photo/groupsthumbnail.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Photo\GroupsThumbnail.xaml"
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
            this.stack = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.tg_group = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 27 "..\..\..\..\Views\Photo\GroupsThumbnail.xaml"
            this.tg_group.Checked += new System.Windows.RoutedEventHandler(this.tg_group_Checked);
            
            #line default
            #line hidden
            
            #line 27 "..\..\..\..\Views\Photo\GroupsThumbnail.xaml"
            this.tg_group.Unchecked += new System.Windows.RoutedEventHandler(this.tg_group_Unchecked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.list = ((System.Windows.Controls.ListView)(target));
            
            #line 29 "..\..\..\..\Views\Photo\GroupsThumbnail.xaml"
            this.list.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.list_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

