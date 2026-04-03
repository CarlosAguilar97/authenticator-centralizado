using Autorizador.Aplicacion.Autenticacion;
using Autorizador.Aplicacion.Response;
using Autorizador.Core.Repositorios;
using Autorizador.Core.Utils;
using Autorizador.Dominio.Entidades.Entidades;
using Microsoft.Extensions.Options;
using System.Text.Json.Nodes;
namespace Autorizador.Aplicacion.Servicios;

public class ServicioAutenticacion : IServicioAutenticacion
{
    private readonly IRepositorioOperacionGeneral _repositorioOperacionGeneral;

    private readonly IOptions<Configuracion> _config;
    private readonly IJwtTokenGenerator _jwtGenerator;

    public ServicioAutenticacion(
        IRepositorioOperacionGeneral repositorioOperacionGeneral,
        IOptions<Configuracion> config,
        IJwtTokenGenerator jwtGenerator
        ) 
    {
        _repositorioOperacionGeneral = repositorioOperacionGeneral;
        _config = config;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<AutenticacionResponse> LoginAsync(AutenticacionRequest request)
    {
        var usuario = await _repositorioOperacionGeneral.ObtenerUnoONuloAsync<Seguridad>(
                        x => x.NombreUsuario == request.Usuario &&
                            x.IndicadorEstado == "1" &&
                            x.Clinica.IndicadorEstado == "1");

        // 2. Validación de seguridad
        if (usuario == null || usuario.Contraseña != request.Clave)
            return null;
        var fechaInicio = DateTime.UtcNow;
        var fechaFin = fechaInicio.AddMinutes(60);
        // 3. Generar el token con los datos obtenidos de la BD
        var token = _jwtGenerator.GenerarToken(
            usuario.CodigoUsuario,
            usuario.IdentificadorClinica,
            usuario.NombrePersona);

        return new AutenticacionResponse(
        AccessToken: token,
        TokenType: "Bearer",
        ExpiresIn: 3600,
        RefreshToken: Guid.NewGuid().ToString(),
        AsClientId: "AppClinicaCentral",
        XIdUsuario: usuario.CodigoUsuario.ToString(),
        XIdClinica: usuario.IdentificadorClinica.ToString(),
        Issued: fechaInicio.ToString("R"), // Formato RFC1123
        Expires: fechaFin.ToString("R")
        );
    }


}
