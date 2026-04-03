using System.Net;

namespace Autorizador.Api.ServerCore.Cliente
{
    public class RespuestaRequest
    {
        public HttpStatusCode CodigoRespuesta { get; private set; }
        public object Contenido { get; private set; }

        public RespuestaRequest(HttpStatusCode codigoRespuesta, object contenido)
        {
            CodigoRespuesta = codigoRespuesta;
            Contenido = contenido;
        }
    }
}
