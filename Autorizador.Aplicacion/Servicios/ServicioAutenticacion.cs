using Autorizador.Aplicacion.Autenticacion;
using Autorizador.Aplicacion.Response;
using Autorizador.Core.Constantes;
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
                            x.IndicadorEstado == ConstantesGlobales.INDICADOR_ACTIVO &&
                            x.Clinica.IndicadorEstado == ConstantesGlobales.INDICADOR_ACTIVO);

        if (usuario == null || usuario.Contraseña != request.Clave)
            return null;

        var fechaInicio = DateTime.UtcNow;
        var fechaFin = fechaInicio.AddSeconds(3600);

        usuario.Clinica.NombreClinica = usuario.Clinica.NombreClinica.Trim();

        string tokenJwt = _jwtGenerator.GenerarToken(usuario.CodigoUsuario, usuario.IdentificadorClinica, usuario.NombrePersona);

        string refreshTokenGenerado = Guid.NewGuid().ToString();

        var nuevoSeguridadToken = SeguridadToken.Crear(
            codigoUsuario: usuario.CodigoUsuario,
            valorToken: refreshTokenGenerado,
            indicadorEstado: ConstantesGlobales.INDICADOR_ACTIVO,
            fechaExpiracion: DateTime.Now.AddDays(7) 
        );

        string guardadoExitoso = await GuardarRefreshTokenAsync(nuevoSeguridadToken);

        if (guardadoExitoso!="Okey")
        {
            throw new Exception("No se pudo persistir el token de sesión.");
        }

        await _repositorioOperacionGeneral.GuardarCambiosAsync();

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

        var unTokenSeguridad = await _repositorioOperacionGeneral.ObtenerUnoONuloAsync<SeguridadToken>(t =>
            t.ValorToken == refreshTokenActual &&
            t.IndicadorEstado == ConstantesGlobales.INDICADOR_ACTIVO &&
            t.FechaExpiracion > DateTime.Now);

        if (unTokenSeguridad == null) throw new UnauthorizedAccessException("Token inválido o expirado");

        var usuario = await _repositorioOperacionGeneral.ObtenerUnoONuloAsync<Seguridad>(u => u.CodigoUsuario == unTokenSeguridad.CodigoUsuario);

        unTokenSeguridad.ActualizarEstado(ConstantesGlobales.INDICADOR_INACTIVO);

        _repositorioOperacionGeneral.Modificar(unTokenSeguridad);

        string nuevoAccess = _jwtGenerator.GenerarToken(usuario.CodigoUsuario,usuario.IdentificadorClinica,usuario.NombrePersona);
        string nuevoRefresh = GenerarCadenaAleatoria();

        var fechaInicio = DateTime.UtcNow;
        var fechaFin = fechaInicio.AddSeconds(3600);

        var nuevoSeguridadToken = SeguridadToken.Crear(
            codigoUsuario: usuario.CodigoUsuario,
            valorToken: nuevoRefresh,
            indicadorEstado: ConstantesGlobales.INDICADOR_ACTIVO,
            fechaExpiracion: DateTime.Now.AddDays(7)
        );

        await _repositorioOperacionGeneral.AdicionarAsync(nuevoSeguridadToken);

        await _repositorioOperacionGeneral.GuardarCambiosAsync();

        return new AutenticacionResponse(
         AccessToken: nuevoAccess,
         TokenType: "Bearer",
         ExpiresIn: 3600,
         RefreshToken: nuevoRefresh.ToString(),
         AsClientId: "AppClinicaCentral",
         XIdUsuario: usuario.CodigoUsuario.ToString(),
         XIdClinica: usuario.IdentificadorClinica.ToString(),
         Issued: fechaInicio.ToString("R"), 
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
            var listaTokensPrevios = await _repositorioOperacionGeneral.ObtenerPorExpresionConLimiteAsync<SeguridadToken>(t =>
                t.CodigoUsuario == token.CodigoUsuario && t.IndicadorEstado == ConstantesGlobales.INDICADOR_ACTIVO);

            foreach (var tokenEncontrado in listaTokensPrevios)
            {
                tokenEncontrado.ActualizarEstado(ConstantesGlobales.INDICADOR_INACTIVO);
                _repositorioOperacionGeneral.Modificar(tokenEncontrado);
            }

            await _repositorioOperacionGeneral.AdicionarAsync(token);
           
            return  "Okey";
            }
        catch (Exception)
        {
            return "Error";
        }
    }
}
