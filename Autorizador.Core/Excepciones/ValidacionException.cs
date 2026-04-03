using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Core.Excepciones
{
    public class ValidacionException : BaseException
    {
        /// <summary>
        /// Codigo del error.
        /// </summary>
        private const string CODIGO = "06";

        /// <summary>
        /// Representa una excepcion cuando no se cumple con una validacion.
        /// </summary>
        /// <param name="mensaje">Descripcion del error.</param>
        public ValidacionException(string mensaje) : base(CODIGO, mensaje)
        {
        }

        /// <summary>
        /// Representa una excepcion cuando no se cumple con una validacion.
        /// </summary>
        /// <param name="mensaje">Descripcion del error.</param>
        /// <param name="innerException">Excepcion interna.</param>
        public ValidacionException(string mensaje, Exception innerException)
            : base(CODIGO, mensaje, innerException)
        {
        }
    }
}
