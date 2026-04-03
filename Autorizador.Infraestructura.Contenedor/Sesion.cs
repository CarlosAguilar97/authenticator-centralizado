using Autorizador.Core.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Infraestructura.Contenedor
{
    public class Sesion : IContextoAplicacion
    {
        #region Implementación de IContextoBitacora.

        public string IdLogin { get; private set; }
        public string IdSesion { get; private set; }
        public string CodigoUsuario { get; private set; }
        public string CodigoAgencia { get; private set; }
        public string IndicadorCanal { get; private set; }
        public byte IndicadorSubCanal { get; set; }
        public string IdTerminalOrigen { get; private set; }
        public string IdTerminalLogin { get; set; }
        public string IdServicio => "AutorizadorDominio";
        public string IdAudiencia { get; set; }
        public string ModeloDispositivo { get; set; }
        public string DireccionIp { get; set; }
        public string Navegador { get; set; }
        public string SistemaOperativo { get; set; }
        public DateTime FechaSistema { get; set; }
        public DateTime FechaHoraServidor => DateTime.Now;
        public string Token { get; set; }
        public string IdCanalOrigen { get; set; }
        public string IdentidadUsuario { get; set; }

        #endregion Implementación de IContextoBitacora.

        public Sesion()
        {
            IdSesion = Guid.NewGuid().ToString();
            var identityActual = Environment.UserName;
            CodigoUsuario =
                identityActual.Substring(identityActual.IndexOf("\\", StringComparison.Ordinal) + 1);
            CodigoAgencia = "01";
            IndicadorCanal = "";
            IndicadorSubCanal = 0;
            IdTerminalOrigen = "";
            IdTerminalLogin = "";
            Token = "";
        }

        public Sesion(
        string idSesion,
        string idTerminalCliente,
        string idTerminalLogin,
        string modeloDispositivo,
        string direccionIp,
        string navegador,
        string sistemaOperativo,
        string token,
        string indicadorCanal,
        string audiencia,
        string idUsuarioAutenticado)
        {
            IdSesion = idSesion;
            var identityActual = Environment.UserName;
            CodigoUsuario =
                identityActual.Substring(identityActual.IndexOf("\\", StringComparison.Ordinal) + 1);
            CodigoAgencia = "01";
            IndicadorCanal = indicadorCanal;
            IndicadorSubCanal = 0;
            IdTerminalOrigen = idTerminalCliente;
            IdTerminalLogin = idTerminalLogin == null ? "" : idTerminalLogin;
            ModeloDispositivo = modeloDispositivo;
            DireccionIp = direccionIp;
            Navegador = navegador;
            SistemaOperativo = sistemaOperativo;
            Token = token;
            IdAudiencia = audiencia;
            IdUsuarioAutenticado = idUsuarioAutenticado;
        }

        #region Implementación IContextoApi

        public string IdUsuarioAutenticado { get; private set; }
        public string IdTerminal { get; private set; }

        public void ActualizarDatos(
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
        string identidadUsuario)
        {
            IdLogin = idLogin;
            CodigoUsuario = codigoUsuario;
            CodigoAgencia = codigoAgencia;
            IdCanalOrigen = canalOrigen;
            IndicadorCanal = canalOrigen;
            IndicadorSubCanal = subCanalOrigen;
            IdTerminal = nombreEquipo;
            IdTerminalOrigen = nombreEquipo;
            IdTerminalLogin = nombreEquipo;
            ModeloDispositivo = modeloDispositivo;
            DireccionIp = direccionIp;
            Navegador = navegador;
            SistemaOperativo = sistemaOperativo;
            Token = token;
            FechaSistema = DateTime.Now;
            IdAudiencia = audiencia;
            IdUsuarioAutenticado = idUsuarioAutenticado;
            IdentidadUsuario = identidadUsuario;
        }

        public void ActualizarDatos(
        string modeloDispositivo,
        string navegador,
        string sistemaOperativo,
        string indicadorCanal)
        {
            ModeloDispositivo = modeloDispositivo;
            Navegador = navegador;
            SistemaOperativo = sistemaOperativo;
            IndicadorCanal = indicadorCanal;
        }

        #endregion Implementación IContextoApi
    }
}
