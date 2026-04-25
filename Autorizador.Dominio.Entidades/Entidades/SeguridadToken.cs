using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Dominio.Entidades.Entidades
{
    public class SeguridadToken
    {
        /// <summary>
        /// Identificador único del token de seguridad, utilizado para diferenciarlo de otros tokens en el sistema.
        /// </summary>
        public int IdentificadorToken { get;  private set; }
        /// <summary>
        /// Código del usuario asociado al token, utilizado para identificar al usuario al que pertenece el token en el sistema.
        /// </summary>
        public Guid CodigoUsuario { get; private set; }
        /// <summary>
        /// Obtiene el valor actual del token utilizado para la autenticación o autorización.
        /// </summary>
        public string ValorToken { get; private set; }
        /// <summary>
        /// Obtiene la fecha de expiración del token, utilizada para determinar cuándo el token ya no es válido y debe ser renovado o eliminado del sistema.
        /// </summary>
        public DateTime FechaExpiracion { get;  private set; }
        /// <summary>
        /// Indicador de estado del token, utilizado para representar el estado actual del token (por ejemplo, activo, inactivo, expirado) y controlar su validez en el sistema.
        /// </summary>
        public string IndicadorEstado { get; private set; }
        /// <summary>
        /// Obtiene la fecha y hora en que se registró la entidad.
        /// </summary>
        public DateTime FechaRegistro { get;  private set; }
        /// <summary>
        /// Crea una nueva instancia de la clase SeguridadToken con los valores especificados.
        /// </summary>
        /// <param name="codigoUsuario">El identificador único del usuario al que pertenece el token.</param>
        /// <param name="valorToken">El valor del token de seguridad que se asignará.</param>
        /// <param name="indicadorEstado">El estado actual del token, representado como una cadena. Por ejemplo, puede indicar si el token está activo
        /// o inactivo.</param>
        /// <param name="fechaExpiracion">La fecha y hora en que el token expirará.</param>
        /// <returns>Una nueva instancia de SeguridadToken inicializada con los valores proporcionados.</returns>
        public static SeguridadToken Crear(
            Guid codigoUsuario, 
            string valorToken, 
            string indicadorEstado,
            DateTime fechaExpiracion)
        {
            return new SeguridadToken
            {
                CodigoUsuario = codigoUsuario,
                ValorToken = valorToken,
                FechaExpiracion = fechaExpiracion,
                IndicadorEstado = indicadorEstado,
                FechaRegistro = DateTime.UtcNow
            };
        }
        /// <summary>
        /// Establece un nuevo valor para el estado actual.
        /// </summary>
        /// <param name="nuevoEstado">El valor que se asignará como nuevo estado. No puede ser null.</param>
        public void ActualizarEstado(string nuevoEstado)
        {
            IndicadorEstado = nuevoEstado;
        }
        /// <summary>
        /// Establece una nueva fecha de expiración para la entidad.
        /// </summary>
        /// <param name="nuevaFechaExpiracion">La nueva fecha y hora en la que la entidad expirará.</param>
        public void ActualizarFechaExpiracion(DateTime nuevaFechaExpiracion)
        {
            FechaExpiracion = nuevaFechaExpiracion;
        }

    }
}
