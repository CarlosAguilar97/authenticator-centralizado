using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Aplicacion.Autenticacion
{
    public record AutenticacionRequest(
    string Usuario,
    string Clave,
    int IdClinica
);
}
