using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Xml;
using System.Xml.Linq;
using MeditacionDominical.Models;
using MeditacionDominical.Resources;
using System.ServiceModel.Syndication;
using System.IO;
using MeditacionDominical.Models;
using System.Text;

namespace MeditacionDominical.Views
{
    public partial class Visor : PhoneApplicationPage
    {

        private Common common = new Common();
        //SyndicationItem feedItem = (App.Current.Resources["feedItem"] as SyndicationItem);
        Uri uriLector = new Uri("/Views/Lector.xaml", UriKind.Relative);

        public Visor()
        {
            InitializeComponent();

            try
            {
                BuildLocalizedApplicationBar();
                CargarFeeds();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void CargarFeeds()
        {
            WebClient RSSClient = new WebClient();
            RSSClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(RSSClient_DownloadStringCompleted);
            //RSSClient.DownloadStringAsync(new Uri("http://feeds.feedburner.com/MeditacionDominical"));
            RSSClient.DownloadStringAsync(new Uri("http://meditaciondominical.blogspot.com/feeds/posts/default?max-results=10"));
        }

        void RSSClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {           
            string xml = e.Result;
            XmlReader reader = XmlReader.Create(new StringReader(xml));
            try 
            {
                //var RSSData = from rss in XElement.Parse(xml).Descendants("entry")
                //              select new System.ServiceModel.Syndication.SyndicationItem
                //              {
                //                  Title = new TextSyndicationContent(rss.Element("title").Value),
                //                  PublishDate = DateTime.ParseExact(rss.Element("published").Value.ToString(), "yyyy-MM-ddTHH:mm:ss.ffK", null),
                //                  //Link = rss.Element("link/href").Value,
                //                  Content = new TextSyndicationContent(rss.Element("content").Value)
                //              };

                //RSSList.ItemsSource = RSSData;


                SyndicationFeed feed = SyndicationFeed.Load(reader);
                RSSList.ItemsSource = feed.Items.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarIconButton appBarIconButton = new ApplicationBarIconButton(new Uri("/Assets/refresh.png", UriKind.Relative));
            appBarIconButton.Text = AppResources.AppBarMenuItemText;
            appBarIconButton.Click += appBarIconButton_Click;
            ApplicationBar.Buttons.Add(appBarIconButton);
        }

        private void appBarIconButton_Click(object sender, EventArgs e)
        {
            try
            {
                //ViewModels.TraduccionVM ciudadViewModel = (App.Current.Resources["traduccionVM"] as ViewModels.TraduccionVM);

                //ciudadViewModel.TraducirCommand.Execute(null);

                CargarFeeds();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RSSList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SyndicationItem feedItem = (SyndicationItem) e.AddedItems[0];
            MemoryStream ms = new MemoryStream();
            XmlWriterSettings ws = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = " ",
                OmitXmlDeclaration = true,
                Encoding = new UTF8Encoding(false),
            };
            XmlWriter w = XmlWriter.Create(ms, ws);
            feedItem.Content.WriteTo(w,"Content", "");
            w.Flush();
            common.GuardarClave("feedItem", Encoding.UTF8.GetString(ms.ToArray(), 0, ms.ToArray().Length));
            NavigationService.Navigate(uriLector);
        }
    }
}