﻿#pragma checksum "D:\Javier\Proyectos\Apostolado\MeditacionDominical\Views\Lector.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DE410F86CB4913BBD1F2F84F3F6C2D76"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MeditacionDominical.Views {
    
    
    public partial class Lector : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock tblNombreAplicacion;
        
        internal System.Windows.Controls.TextBlock tblTitulo;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.ScrollViewer scrContenido;
        
        internal Microsoft.Phone.Controls.WebBrowser wbContenido;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MeditacionDominical;component/Views/Lector.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.tblNombreAplicacion = ((System.Windows.Controls.TextBlock)(this.FindName("tblNombreAplicacion")));
            this.tblTitulo = ((System.Windows.Controls.TextBlock)(this.FindName("tblTitulo")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.scrContenido = ((System.Windows.Controls.ScrollViewer)(this.FindName("scrContenido")));
            this.wbContenido = ((Microsoft.Phone.Controls.WebBrowser)(this.FindName("wbContenido")));
        }
    }
}

