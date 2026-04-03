using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Infraestructura.Datos.Interfaces
{
    public interface IContextoBase
    {
        /// <summary>
        /// Obtiene el conjunto de datos de una entidad.
        /// </summary>
        /// <typeparam name="T">Tipo de conjunto.</typeparam>
        /// <returns>Conjunto de datos de una entidad</returns>
        DbSet<T> Establecer<T>() where T : class;

        /// <summary>
        /// Almacena los datos en el contexto.
        /// </summary>
        void GuardarCambios();
    }
}
