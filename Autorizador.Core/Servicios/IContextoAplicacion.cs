using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Core.Servicios
{
    public interface IContextoAplicacion
    {
        /// <summary>
        /// ID de la sesión
        /// </summary>
        string IdSesion { get; }

        /// <summary>
        /// ID del login
        /// </summary>
        string IdLogin { get; }

        /// <summary>
        /// ID del terminal de origen
        /// </summary>
        string IdTerminalOrigen { get; }

        /// <summary>
        /// Indicador del Canal
        /// </summary>
        string IndicadorCanal { get; }

        /// <summary>
        /// Indicador de Sub Canal
        /// </summary>
        byte IndicadorSubCanal { get; }

        /// <summary>
        /// Codigo usuario de la session
        /// </summary>
        string CodigoUsuario { get; }

        /// <summary>
        /// Codigo de la agencia de la session
        /// </summary>
        string CodigoAgencia { get; }
        string IdServicio { get; }

        string ModeloDispositivo { get; }
        string DireccionIp { get; }
        string Navegador { get; }
        string SistemaOperativo { get; }
        DateTime FechaSistema { get; }
        DateTime FechaHoraServidor { get; }
        string IdAudiencia { get; set; }

        /// <summary>
        /// Token para operaciones
        /// </summary>
        string Token { get; }

        /// <summary>
        /// Procedimiento para actualizar los valores del contexto
        /// </summary>
        /// <param name="idSesion">ID de la sesión</param>
        /// <param name="idLogin">ID del login</param>
        /// <param name="idUsuarioAutenticado">ID del usuario autenticado</param>
        /// <param name="idTerminalUsuario">ID de terminal del usuario</param>
        /// <param name="idCanalOrigen">ID del canal de origen</param>
        /// <param name="codigoUsuario">Código del usuario</param>
        /// <param name="codigoAgencia">Código de la agencia</param>
        /// <returns>Contexto actualizado</returns>
        void ActualizarDatos(
        string idLogin,
        string codigoUsuario,
        string codigoAgencia,
        string canalOrigen,
        byte subCanalOrigen,
        string nombreEquipo,
        string modeloDispositivo,
        string direccionIp,
        string navegador,
        string sistemaOperativo,
        string token,
        string audiencia,
        string idUsuarioAutenticado,
        string identidadUsuario);

        void ActualizarDatos(
        string modeloDispositivo,
        string navegador,
        string sistemaOperativo,
        string indicadorCanal);
    }
}
