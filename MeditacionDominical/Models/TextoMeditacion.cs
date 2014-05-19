using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeditacionDominical.Models
{
    class TextoMeditacion:NotificationEnabledObject
    {
        DateTime fechaLectura;
        public DateTime FechaLectura
        {
            get { return fechaLectura; }
            set
            {
                fechaLectura = value;
                OnPropertyChanged();
            }
        }

        string textoLectura;
        public string TextoLectura
        {
            get { return textoLectura; }
            set
            {
                textoLectura = value;
                OnPropertyChanged();
            }
        }
    }
}
