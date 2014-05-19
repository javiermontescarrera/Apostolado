using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeditacionDominical.Models
{
    class ContenidoLectura : NotificationEnabledObject
    {
        // Esta clase contendrá cada sub-texto sindicados en el índice.

        SortedDictionary<byte, ElementoIndice> elementos;
        public SortedDictionary<byte, ElementoIndice> Elementos
        {
            get { return elementos; }
            set
            {
                elementos = value;
                OnPropertyChanged();
            }
        }
    }
}
