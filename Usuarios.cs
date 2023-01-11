using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudCoppel
{
    internal class Usuarios
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string domicilio { get; set; }
        public string telefono { get; set; }

        public string estado { get; set; }
    }
}
