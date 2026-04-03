using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Aplicacion.Response
{
    public record UsuarioResponse(
     Guid CodigoUsuario,
     string NombrePersona,
     string NombreClinica
    );

}
