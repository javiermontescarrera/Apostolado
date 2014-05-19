using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeditacionDominical.Models
{
    class ElementoIndice : NotificationEnabledObject
    {
        ElementoIndice elementoPadre;
        public ElementoIndice ElementoPadre
        {
            get { return elementoPadre; }
            set
            {
                elementoPadre = value;
                OnPropertyChanged();
            }
        }

        SortedDictionary<byte, ElementoIndice> subElementos;
        public SortedDictionary<byte, ElementoIndice> SubElementos
        {
            get { return subElementos; }
            set
            {
                subElementos = value;
                OnPropertyChanged();
            }
        }

        string texto = string.Empty;
        public string Texto
        {
            get { return texto; }
            set
            {
                texto = value;
                OnPropertyChanged();
            }
        }

    }
}
