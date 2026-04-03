using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Autorizador.Aplicacion.Response
{
    public record AutenticacionRefreshResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("token_type")] string TokenType,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("refresh_token")] string RefreshToken,
    [property: JsonPropertyName("as:client_id")] string AsClientId,
    [property: JsonPropertyName("x:idClienteFinal")] string XIdClienteFinal,
    [property: JsonPropertyName("x:idVisual")] string XIdVisual,
    [property: JsonPropertyName("x:idSesion")] string XIdSesion,
    [property: JsonPropertyName(".issued")] string Issued,
    [property: JsonPropertyName(".expires")] string Expires,
    [property: JsonPropertyName(".refresh")] string Refresh = "True");
}
