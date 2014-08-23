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
using System.IO.IsolatedStorage;
using Microsoft.Phone.Net.NetworkInformation;
using System.Collections.ObjectModel;

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
                if (verificarConexion())
                {
                    CargarFeeds();
                }
                else
                {
                    RSSList.ItemsSource = leerFeeds();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool verificarConexion()
        {
            bool Rpta = NetworkInterface.GetIsNetworkAvailable();

            return Rpta;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (RSSList.SelectedItems.Count > 0)
            //{
            //    RSSList.SelectedItems.Clear();
            //}

            //for (int i = 0; i < RSSList.Items.Count; i++)
            //{
            //    RSSList.Items[i] = null;
            //}
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
        }


        void CargarFeeds()
        {
            WebClient RSSClient = new WebClient();
            RSSClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(RSSClient_DownloadStringCompleted);
            //RSSClient.DownloadStringAsync(new Uri("http://feeds.feedburner.com/MeditacionDominical"));
            RSSClient.DownloadStringAsync(new Uri("http://meditaciondominical.blogspot.com/feeds/posts/default?max-results=4"));
        }

        void RSSClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                string xml = e.Result;
                XmlReader reader = XmlReader.Create(new StringReader(xml));
                try
                {
                    SyndicationFeed feed = SyndicationFeed.Load(reader);

                    List<SyndicationItem> listaFeeds = feed.Items.ToList();
                    ObservableCollection<Models.Feed> RSSData = new ObservableCollection<Models.Feed>();
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
                    grabarFeeds(RSSData);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (System.Reflection.TargetInvocationException tiEx)
            {
                RSSList.ItemsSource = leerFeeds();
            }
        }

        private void grabarFeeds(ObservableCollection<Models.Feed> RSSData)
        {
            
            for (int i = 1; i <= 10; i++)
            {
                String strArchivoId = "ArchivoId" + i.ToString();
                String strArchivoTitulo = "ArchivoTitulo" + i.ToString();
                String strArchivoFecha = "ArchivoFecha" + i.ToString();
                String strArchivoLink = "ArchivoLink" + i.ToString();
                String strArchivoContenidoHtml = "ArchivoContenidoHtml" + i.ToString();

                if (IsolatedStorageSettings.ApplicationSettings.Contains(strArchivoId))
                {
                    IsolatedStorageSettings.ApplicationSettings.Remove(strArchivoId);
                }

                if (IsolatedStorageSettings.ApplicationSettings.Contains(strArchivoTitulo))
                {
                    IsolatedStorageSettings.ApplicationSettings.Remove(strArchivoTitulo);
                }

                if (IsolatedStorageSettings.ApplicationSettings.Contains(strArchivoFecha))
                {
                    IsolatedStorageSettings.ApplicationSettings.Remove(strArchivoFecha);
                }

                if (IsolatedStorageSettings.ApplicationSettings.Contains(strArchivoLink))
                {
                    IsolatedStorageSettings.ApplicationSettings.Remove(strArchivoLink);
                }

                if (IsolatedStorageSettings.ApplicationSettings.Contains(strArchivoContenidoHtml))
                {
                    IsolatedStorageSettings.ApplicationSettings.Remove(strArchivoContenidoHtml);
                }
            }

            int j = 1;
            foreach (Models.Feed oFeed in RSSData)
            {
                String  strArchivoId = "ArchivoId" + j.ToString();
                String strArchivoTitulo = "ArchivoTitulo" + j.ToString();
                String strArchivoFecha = "ArchivoFecha" + j.ToString();
                String strArchivoLink = "ArchivoLink" + j.ToString();
                String strArchivoContenidoHtml = "ArchivoContenidoHtml" + j.ToString();

                IsolatedStorageSettings.ApplicationSettings[strArchivoId] = oFeed.Id;
                IsolatedStorageSettings.ApplicationSettings[strArchivoTitulo] = oFeed.Titulo;
                IsolatedStorageSettings.ApplicationSettings[strArchivoFecha] = oFeed.FechaPublicacion;
                IsolatedStorageSettings.ApplicationSettings[strArchivoLink] = oFeed.Link;
                IsolatedStorageSettings.ApplicationSettings[strArchivoContenidoHtml] = oFeed.ContenidoHtml;

                j++;
            }
        }



        private ObservableCollection<Models.Feed> leerFeeds()
        {
            ObservableCollection<Models.Feed> RSSData = new ObservableCollection<Models.Feed>();

            
            for (int i = 1;i<=10;i++)
            {
                String strArchivoId = "ArchivoId" + i.ToString();
                String strArchivoTitulo = "ArchivoTitulo" + i.ToString();
                String strArchivoFecha = "ArchivoFecha" + i.ToString();
                String strArchivoLink = "ArchivoLink" + i.ToString();
                String strArchivoContenidoHtml = "ArchivoContenidoHtml" + i.ToString();

                Models.Feed oFeed;

                if (IsolatedStorageSettings.ApplicationSettings.Contains(strArchivoId))
                {
                    oFeed = new Models.Feed();
                    oFeed.Id = IsolatedStorageSettings.ApplicationSettings[strArchivoId].ToString();
                }
                else
                {
                    break;
                }
                
                if (IsolatedStorageSettings.ApplicationSettings.Contains(strArchivoTitulo))
                {
                    oFeed.Titulo = IsolatedStorageSettings.ApplicationSettings[strArchivoTitulo].ToString();
                }

                if (IsolatedStorageSettings.ApplicationSettings.Contains(strArchivoFecha))
                {
                    oFeed.FechaPublicacion = IsolatedStorageSettings.ApplicationSettings[strArchivoFecha].ToString();
                }

                if (IsolatedStorageSettings.ApplicationSettings.Contains(strArchivoLink))
                {
                    oFeed.Link = IsolatedStorageSettings.ApplicationSettings[strArchivoLink].ToString();
                }

                if (IsolatedStorageSettings.ApplicationSettings.Contains(strArchivoContenidoHtml))
                {
                    oFeed.ContenidoHtml = IsolatedStorageSettings.ApplicationSettings[strArchivoContenidoHtml].ToString();
                }

                RSSData.Add(oFeed);
            }

            return RSSData;
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

        //private void RSSList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Models.Feed feedItem = (Models.Feed)e.AddedItems[0];
        //    leerFeedItem(feedItem);
        //}

        private void RSSList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (RSSList.SelectedItems.Count > 0)
            {
                Models.Feed feedItem = (Models.Feed)RSSList.SelectedItems[0];
                leerFeedItem(feedItem);
            }
        }

        private void leerFeedItem(Models.Feed feedItem)
        {
            common.GuardarClave("Titulo", feedItem.Titulo);
            common.GuardarClave("Contenido", feedItem.ContenidoHtml);
            common.GuardarClave("ContenidoPlano", feedItem.ContenidoPlano);
            common.GuardarClave("link", feedItem.Link.ToString());
            NavigationService.Navigate(uriLector);
        }
    }
}