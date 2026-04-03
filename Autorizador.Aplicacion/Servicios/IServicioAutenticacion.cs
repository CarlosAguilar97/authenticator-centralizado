using Autorizador.Aplicacion.Autenticacion;
using Autorizador.Aplicacion.Response;
using System.Text.Json.Nodes;

namespace Autorizador.Aplicacion.Servicios;

public interface IServicioAutenticacion
{
    Task<AutenticacionResponse> LoginAsync(AutenticacionRequest request);
}
