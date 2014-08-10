using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.ServiceModel.Syndication;
using HtmlAgilityPack;
using System.Net;

namespace MeditacionDominical.Models
{
    public class Common
    {
        public void GuardarClave(string clave, string valor)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(clave))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(clave);
            }

            IsolatedStorageSettings.ApplicationSettings[clave] = valor;
        }

        public string LeerClave(string clave)
        {
            string valor = string.Empty;

            if (IsolatedStorageSettings.ApplicationSettings.Contains(clave))
            {
                valor = IsolatedStorageSettings.ApplicationSettings[clave].ToString();
            }

            return valor;
        }

        public string RetornarContenidoFeed(SyndicationContent feedItemContent)
        {
            string rpta = string.Empty;

            try
            {
                MemoryStream ms = new MemoryStream();
                XmlWriterSettings ws = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = " ",
                    OmitXmlDeclaration = true,
                    Encoding = new UTF8Encoding(false),
                };
                XmlWriter w = XmlWriter.Create(ms, ws);
                feedItemContent.WriteTo(w, "Content", "");
                w.Flush();
                String texto = Encoding.UTF8.GetString(ms.ToArray(), 0, ms.ToArray().Length);

                texto = texto.Replace("<br />", "\n");
                //texto = HttpUtility.HtmlDecode(texto);
                //rpta = HttpUtility.HtmlDecode(ExtractText(texto));
                rpta = HttpUtility.HtmlDecode(texto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rpta;
        }

        //public static string ExtractText(string html)
        //{
        //    if (html == null)
        //    {
        //        throw new ArgumentNullException("html");
        //    }

        //    HtmlDocument doc = new HtmlDocument();
        //    doc.LoadHtml(html);

        //    var chunks = new List<string>();

        //    foreach (var item in doc.DocumentNode.DescendantNodesAndSelf())
        //    {
        //        if (item.NodeType == HtmlNodeType.Text)
        //        {
        //            if (item.InnerText.Trim() != "")
        //            {
        //                chunks.Add(item.InnerText.Trim());
        //            }
        //        }
        //    }
        //    return String.Join(" ", chunks);
        //}

    }
}
