using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Core.Utils
{
    public class Configuracion
    {
        /// <summary>
        /// Servidor de base de datos SAF
        /// </summary>
        public string? BD_SERVIDOR { get; set; }

        /// <summary>
        /// Nombre de base de datos SAF
        /// </summary>
        public string? BD_CATALOGO { get; set; }

        /// <summary>
        /// Usuario de base de datos SAF
        /// </summary>
        public string? BD_USUARIO { get; set; }
        /// <summary>
        /// Contraseña de base de datos SAF
        /// </summary>
        public string? BD_CLAVE { get; set; }

        /// <summary>
        /// Indicador para seguridad integrada
        /// </summary>
        public bool USAR_SEGURIDAD_INTEGRADA_BD { get; set; }
    }
}
