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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (RSSList.SelectedItems.Count > 0)
            {
                //RSSList.SelectedItems.Clear();
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
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                
                List<SyndicationItem> listaFeeds = feed.Items.ToList();
                List<Models.Feed> RSSData = new List<Models.Feed>();
                foreach (SyndicationItem si in listaFeeds)
                {
                    Models.Feed oFeed = new Models.Feed();
                    oFeed.Id = si.Id;
                    oFeed.Titulo = si.Title.Text;
                    oFeed.FechaPublicacion = si.PublishDate.ToString("dd/MM/yyyy");
                    oFeed.ContenidoHtml = "<html><body>" + common.RetornarContenidoFeed(si.Content) + "</body></html>";
                    
                    foreach (SyndicationLink sl in feed.Items.ToList()[0].Links)
                    {
                        if (sl.RelationshipType == "alternate")
                        {
                            oFeed.Link = sl.Uri.ToString();
                        }
                    }

                    RSSData.Add(oFeed);
                }

                RSSList.ItemsSource = RSSData;

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
            Models.Feed feedItem = (Models.Feed)e.AddedItems[0];
            common.GuardarClave("Titulo", feedItem.Titulo);
            common.GuardarClave("Contenido", feedItem.ContenidoHtml);
            common.GuardarClave("ContenidoPlano", feedItem.ContenidoPlano);
            common.GuardarClave("link", feedItem.Link.ToString());
            NavigationService.Navigate(uriLector);
        }

    }
}