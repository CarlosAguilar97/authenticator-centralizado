using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Core.Dtos
{

    /// <summary>
    /// Dto para generar token de refresco
    /// </summary>
    /// <param name="IdAudiencia">Id de la audiencia</param>
    /// <param name="IdClienteApi">Id del api cliente</param>
    /// <param name="IdVisual">Id visual</param>
    /// <param name="TokenAcceso">Token de acceso</param>
    /// <param name="IndicadorCanal">Indicador de canal</param>
    /// <param name="TipoAutenticacion">Tipo de autenticación</param>
    public record GenerarTokenRefrescoDto
    (
        string IdAudiencia,
        string IdVisual,
        string TokenAcceso,
        string IndicadorCanal,
        byte TipoAutenticacion
    );

}
