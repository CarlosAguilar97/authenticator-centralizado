using Autorizador.Aplicacion.Autenticacion;
using Autorizador.Aplicacion.Response;
using Autorizador.Dominio.Entidades.Entidades;
using System.Text.Json.Nodes;

namespace Autorizador.Aplicacion.Servicios;

public interface IServicioAutenticacion
{
    Task<AutenticacionResponse> LoginAsync(AutenticacionRequest request);
    Task<AutenticacionResponse> RenovarTokenAsync(string refreshTokenActual);
    Task<string> GuardarRefreshTokenAsync(SeguridadToken token);
}
