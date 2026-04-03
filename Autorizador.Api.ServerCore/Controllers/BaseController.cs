using Autorizador.Api.ServerCore.Cliente;
using Autorizador.Api.ServerCore.Excepciones;
using Autorizador.Core.Excepciones;
using Autorizador.Core.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Autorizador.Api.ServerCore.Controllers
{
    public abstract class BaseController<T> : ControllerBase where T : class
    {
        public IContextoAplicacion _contextoAplicacion { get; }

        protected BaseController(IContextoAplicacion contextoAplicacion)
        {
            _contextoAplicacion = contextoAplicacion;
        }

        protected IActionResult RealizarOperacion(Func<RespuestaRequest> operacion)
        {
            RespuestaRequest resultado;
            try
            {
                resultado = operacion();
            }
            catch (ValidacionException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new MensajeError { Codigo = ex.CodigoError, Mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            return StatusCode(Convert.ToInt32(resultado.CodigoRespuesta), resultado.Contenido);
        }
    }
}
