using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MeditacionDominical.Models
{
    public class RssTextTrimmer : IValueConverter
    {
        // Clean up text fields from each SyndicationItem. 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            int maxLength = 200;
            int strLength = 0;
            string fixedString = "";

            // Removemos tags HTML...
            fixedString = Regex.Replace(value.ToString(), "<[^>]+>", string.Empty);

            // Remove caracteres de nueva línea y retorno de carro...
            fixedString = fixedString.Replace("\r", "").Replace("\n", "");

            // Remove los caracteres codificados HTML...
            fixedString = HttpUtility.HtmlDecode(fixedString);

            strLength = fixedString.ToString().Length;

            // Si no hay texto en la noticia, entonces, retornamos nulo...
            if (strLength == 0)
            {
                return null;
            }

            // Limitamos el texto para que no sea demasiado largo
            else if (strLength >= maxLength)
            {
                fixedString = fixedString.Substring(0, maxLength);

                // Evitamos dejar alguna palabra mutilada...
                fixedString = fixedString.Substring(0, fixedString.LastIndexOf(" "));
            }

            // Le agregamos el indicador que que la noticia continúa...
            fixedString += "...";

            return fixedString;
        }

        // Este código no lo vamos a utilizar ya que no vamos a hacer TwoWay binding...  
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
