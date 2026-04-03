using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Aplicacion.Servicios;

public interface IJwtTokenGenerator
{
    string GenerarToken(Guid codigoUsuario, int idClinica, string nombrePersona);
}
