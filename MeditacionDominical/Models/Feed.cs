using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using HtmlAgilityPack;
using System.Net;

namespace MeditacionDominical.Models
{
    class Feed : NotificationEnabledObject
    {
        public String Id { get; set; }
        public String Titulo { get; set; }
        public String Link { get; set; }
        public String FechaPublicacion { get; set; }
        //public String Contenido { get; set; }
        public String ContenidoHtml { get; set; }

        public String ContenidoPlano 
        { 
            get{
                return ExtractText(ContenidoHtml);
            }
        }

        private string ExtractText(string html)
        {
            if (html == null)
            {
                throw new ArgumentNullException("html");
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var chunks = new List<string>();

            foreach (var item in doc.DocumentNode.DescendantNodesAndSelf())
            {
                if (item.NodeType == HtmlNodeType.Text)
                {
                    if (item.InnerText.Trim() != "")
                    {
                        chunks.Add(item.InnerText.Trim());
                    }
                }
            }
            return HttpUtility.HtmlDecode(String.Join(" ", chunks));
        }
    }
}
