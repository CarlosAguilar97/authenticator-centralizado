using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Dominio.Entidades.Entidades
{
    public class Clinica
    {
        /// <summary>
        /// Identificador único de la clínica, utilizado para diferenciarla de otras clínicas en el sistema.
        /// </summary>
        public int IdentificadorClinica { get; set; }
        /// <summary>
        /// Obtiene o establece el nombre de la clínica.
        /// </summary>
        public string NombreClinica { get; set; }
        /// <summary>
        /// Obtiene o establece el indicador de estado asociado a la entidad.
        /// </summary>
        public string IndicadorEstado { get; set; }
    }
}
