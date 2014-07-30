using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace MeditacionDominical.Models
{
    public class RssServiceModel
    {
        private const string uriNoticias = "http://news.google.es/news?pz=1&cf=all&ned=es&hl=es&q={0}&output=rss";

        HttpClient client = new HttpClient();

        /*
        public async Task<IList<Noticia>> ObtenerNoticias(string ciudad)
        {
            string uriFormateada = string.Format(uriNoticias, ciudad.Replace(",", "+").Replace(" ", ""));

            var result = await client.GetStringAsync(uriFormateada);

            // Load the feed into a SyndicationFeed instance
            StringReader stringReader = new StringReader(result);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);


            IList<Noticia> noticas = new ObservableCollection<Noticia>();

            foreach (SyndicationItem s in feed.Items)
            {
                Noticia n = new Noticia()
                {
                    Title = s.Title,
                    Summary = s.Summary,
                    PublishDate = s.PublishDate
                };

                noticas.Add(n);
            }

            return noticas;
        }
        */
    }
}
