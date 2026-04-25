
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Autorizador.Aplicacion.Servicios;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private const string SecretKey = "Tu_Llave_Secreta_Super_Segura_2026";

    public string GenerarToken(Guid codigoUsuario, int idClinica, string nombrePersona)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("CodUsuario", codigoUsuario.ToString()),
            new Claim("IdClinica", idClinica.ToString()),
            new Claim(ClaimTypes.Name, nombrePersona),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "AutorizadorCentral",
            audience: "ClinicasApp",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
