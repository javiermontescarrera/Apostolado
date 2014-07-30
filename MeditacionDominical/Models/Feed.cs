using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeditacionDominical.Models
{
    class Feed : NotificationEnabledObject
    {
        public string Titulo { get; set; }
        public string Link { get; set; }
        public string FechaPublicacion { get; set; }
        public string Contenido { get; set; }
    }
}
