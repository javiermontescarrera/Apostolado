using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
