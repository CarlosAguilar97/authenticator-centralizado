using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Dominio.Entidades.Entidades
{
    public class Seguridad
    {
        /// <summary>
        /// Obtiene el identificador único del usuario asociado.
        /// </summary>
        public Guid CodigoUsuario { get; private set; }
        /// <summary>
        /// Obtiene el identificador único de la clínica asociada al usuario.
        /// </summary>
        public int IdentificadorClinica { get; private set; }
        /// <summary>
        /// Obtiene el nombre de usuario asociado a la instancia.
        /// </summary>
        public string NombreUsuario { get; private set; }
        /// <summary>
        /// Obitene el nombre de la persona asociada al usuario, utilizado para identificar al usuario de manera más amigable en el sistema.
        /// </summary>
        public string NombrePersona { get; private set; }
        /// <summary>
        /// Contraseña de seguridad del usuario
        /// </summary>
        public string Contraseña { get; private set; }
        /// <summary>
        /// Obtiene el estado actual representado por esta instancia.
        /// </summary>
        public string IndicadorEstado { get; private set; }
        /// <summary>
        /// Fecha de Registro de la instancia, utilizada para llevar un control temporal de cuándo se creó el registro en el sistema.
        /// </summary>
        public DateTime FechaRegistro { get; private set; }
        // Relación: Permite acceder a los datos de la clínica desde el usuario
        public virtual Clinica Clinica { get; private set; }
    }
}
