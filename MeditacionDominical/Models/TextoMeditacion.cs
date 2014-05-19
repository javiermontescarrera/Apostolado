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


        // Por ejemplo: nombreMeditacion = "Domingo de la Semana 5ª de Pascua. Ciclo A"
        string nombreMeditacion;
        public string NombreMeditacion
        {
            get { return nombreMeditacion; }
            set
            {
                nombreMeditacion = value;
                OnPropertyChanged();
            }
        }

        ContenidoLectura contenido;
        public ContenidoLectura Contenido
        {
            get { return contenido; }
            set
            {
                contenido = value;
                OnPropertyChanged();
            }
        }
    }
}
