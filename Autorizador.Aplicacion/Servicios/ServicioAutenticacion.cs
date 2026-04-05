using Autorizador.Aplicacion.Autenticacion;
using Autorizador.Aplicacion.Response;
using Autorizador.Core.Repositorios;
using Autorizador.Core.Utils;
using Autorizador.Dominio.Entidades.Entidades;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Text;
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
        var fechaFin = fechaInicio.AddSeconds(3600);
        string tokenJwt = _jwtGenerator.GenerarToken(usuario.CodigoUsuario, usuario.IdentificadorClinica, usuario.NombrePersona);

        // GENERACIÓN DEL REFRESH TOKEN
        string refreshTokenGenerado = Guid.NewGuid().ToString();

        // CREACIÓN DE LA ENTIDAD SEGURIDADTOKEN
        var seguridadToken = new SeguridadToken
        {
            CodigoUsuario = usuario.CodigoUsuario,
            ValorToken = refreshTokenGenerado,
            FechaExpiracion = DateTime.Now.AddDays(7), // Duración de 7 días
            IndicadorEstado = "1",
            FechaRegistro = DateTime.Now
        };

        // LLAMADA AL MÉTODO DE GUARDADO
        string guardadoExitoso = await GuardarRefreshTokenAsync(seguridadToken);

        if (guardadoExitoso!="Okey")
        {
            throw new Exception("No se pudo persistir el token de sesión.");
        }
        await _repositorioOperacionGeneral.GuardarCambiosAsync();
        // RETORNO DE LA RESPUESTA
        return new AutenticacionResponse(
            AccessToken: tokenJwt,
            TokenType: "Bearer",
            ExpiresIn: 3600,
            RefreshToken: refreshTokenGenerado,
            AsClientId: "AppClinicaCentral",
            XIdUsuario: usuario.CodigoUsuario.ToString(),
            XIdClinica: usuario.IdentificadorClinica.ToString(),
            Issued: fechaInicio.ToString("R"),
            Expires: fechaFin.ToString("R")
        );

    }
    public async Task<AutenticacionResponse> RenovarTokenAsync(string refreshTokenActual)
    {
        // 1. Buscar el token en la BD
        var tokenBd = await _repositorioOperacionGeneral.ObtenerUnoONuloAsync<SeguridadToken>(t =>
            t.ValorToken == refreshTokenActual &&
            t.IndicadorEstado == "1" &&
            t.FechaExpiracion > DateTime.Now);

        if (tokenBd == null) throw new UnauthorizedAccessException("Token inválido o expirado");

        // 2. Obtener los datos del usuario
        var usuario = await _repositorioOperacionGeneral.ObtenerUnoONuloAsync<Seguridad>(u => u.CodigoUsuario == tokenBd.CodigoUsuario);

        // 3. Invalidar el token anterior (Rotación)
        tokenBd.IndicadorEstado ="0";
        _repositorioOperacionGeneral.Modificar(tokenBd);

        // 4. Generar nuevo par de tokens
        string nuevoAccess = _jwtGenerator.GenerarToken(usuario.CodigoUsuario,usuario.IdentificadorClinica,usuario.NombrePersona);
        string nuevoRefresh = GenerarCadenaAleatoria();
        var fechaInicio = DateTime.UtcNow;
        var fechaFin = fechaInicio.AddSeconds(3600);
        // 5. Guardar el nuevo refresh token
        await _repositorioOperacionGeneral.AdicionarAsync(new SeguridadToken
        {
            CodigoUsuario = usuario.CodigoUsuario,
            ValorToken = nuevoRefresh,
            FechaExpiracion = DateTime.Now.AddDays(7),
            IndicadorEstado = "1"
        });

        await _repositorioOperacionGeneral.GuardarCambiosAsync();

        return new AutenticacionResponse(
         AccessToken: nuevoAccess,
         TokenType: "Bearer",
         ExpiresIn: 3600,
         RefreshToken: nuevoRefresh.ToString(),
         AsClientId: "AppClinicaCentral",
         XIdUsuario: usuario.CodigoUsuario.ToString(),
         XIdClinica: usuario.IdentificadorClinica.ToString(),
         Issued: fechaInicio.ToString("R"), // Formato RFC1123
         Expires: fechaFin.ToString("R")
         );
    }
    private string GenerarCadenaAleatoria()
    {
        var buffer = new byte[32];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(buffer);
        return Convert.ToBase64String(buffer);
    }
    public async Task<string> GuardarRefreshTokenAsync(SeguridadToken token)
    {
        try
        {
            // 1. Opcional: Inactivar tokens anteriores del mismo usuario
            var tokensPrevios = await _repositorioOperacionGeneral.ObtenerPorExpresionConLimiteAsync<SeguridadToken>(t =>
                t.CodigoUsuario == token.CodigoUsuario && t.IndicadorEstado == "1");

            foreach (var t in tokensPrevios)
            {
                t.IndicadorEstado = "0"; // Inactivamos los viejos
                _repositorioOperacionGeneral.Modificar(t);
            }

            // 2. Insertar el nuevo token
            await _repositorioOperacionGeneral.AdicionarAsync(token);
           
            return  "Okey";
            }
        catch (Exception)
        {
            return "Error";
        }
    }
}
