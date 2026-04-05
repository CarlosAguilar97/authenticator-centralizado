using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Dominio.Entidades.Entidades
{
    public class SeguridadToken
    {
        public int IdentificadorToken { get;  set; }
        public Guid CodigoUsuario { get;  set; }
        public string ValorToken { get;  set; }
        public DateTime FechaExpiracion { get;  set; }
        public string IndicadorEstado { get;  set; }
        public DateTime FechaRegistro { get;  set; }

    }
}
