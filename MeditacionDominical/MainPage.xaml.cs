using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MeditacionDominical.Resources;
using System.Xml.Linq;
using MeditacionDominical.Models;
using System.ServiceModel.Syndication;
using System.Xml;

namespace MeditacionDominical
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            WebClient RSSClient = new WebClient();
            RSSClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(RSSClient_DownloadStringCompleted);
            RSSClient.DownloadStringAsync(new Uri("http://feeds.feedburner.com/MeditacionDominical"));
        }

        void RSSClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var RSSData = from rss in XElement.Parse(e.Result).Descendants("item")
                          select new Feed
                          {
                              Titulo = rss.Element("title").Value,
                              FechaPublicacion = rss.Element("pubDate").Value,
                              Link = rss.Element("link").Value,
                              Contenido = rss.Element("description").Value
                          };
            RSSList.ItemsSource = RSSData;
        }
    }
}