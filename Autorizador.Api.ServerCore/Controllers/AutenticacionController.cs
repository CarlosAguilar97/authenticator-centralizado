using Autorizador.Aplicacion.Autenticacion;
using Autorizador.Aplicacion.Response;
using Autorizador.Aplicacion.Servicios;
using Autorizador.Core.Servicios;
using Autorizador.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json.Nodes;

namespace Autorizador.Api.ServerCore.Controllers
{

    [ApiController]
    [Route("oauth2/[controller]")]
    public class AutenticacionController : BaseController<AutenticacionController>
    {
        private readonly IContextoAplicacion _contextoAplicacion;
        private readonly IServicioAutenticacion _servicioAutenticacion;

        public AutenticacionController(
           IContextoAplicacion contextoAplicacion,
           IOptions<Configuracion> config,
           IServicioAutenticacion servicioAutenticacion)
           : base(contextoAplicacion)
        {
            _contextoAplicacion = contextoAplicacion;
            _servicioAutenticacion = servicioAutenticacion;
        }
        [HttpPost("acceso")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(typeof(JsonObject), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AutenticacionResponse>> Autenticar([FromForm] AutenticacionRequest request)
        {
            var respuesta = await _servicioAutenticacion.LoginAsync(request);

            if (respuesta == null)
                return Unauthorized(new { error = "Acceso no autorizado" });

            return Ok(respuesta);
        }
    }
}
