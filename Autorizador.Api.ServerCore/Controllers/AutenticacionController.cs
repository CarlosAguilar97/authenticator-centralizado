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
        /// <summary>
        /// Endpoint para renovar el Access Token usando el Refresh Token.
        /// Permite al usuario mantener la sesión activa sin volver a pedir contraseña.
        /// </summary>
        [HttpPost("refresh")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Refresh([FromForm] string refresh_token)
        {
            if (string.IsNullOrEmpty(refresh_token))
            {
                return BadRequest(new { mensaje = "El refresh_token es requerido" });
            }

            try
            {
                var resultado = await _servicioAutenticacion.RenovarTokenAsync(refresh_token);
                return Ok(resultado);
            }
            catch (UnauthorizedAccessException)
            {
                // 401 si el refresh token expiró en la base de datos o ya fue usado (rotación)
                return Unauthorized(new { mensaje = "Sesión expirada o token de renovación inválido" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al procesar la renovación", detalle = ex.Message });
            }
        }
    }
}
