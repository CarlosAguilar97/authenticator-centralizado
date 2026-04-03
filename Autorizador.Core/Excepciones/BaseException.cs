using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Core.Excepciones
{
    /// <summary>
    /// Representa una excepcion controlada por el sistema.
    /// </summary>
    public class BaseException : Exception
    {
        /// <summary>
        /// Codigo del error.
        /// </summary>
        public string CodigoError { get; }

        /// <summary>
        /// Representa una excepcion controlada por el sistema.
        /// </summary>
        /// <param name="codigo">Codigo del error.</param>
        /// <param name="message">Descripcion del error.</param>
        protected BaseException(string codigo, string message)
            : base(message)
        {
            CodigoError = codigo;
        }

        /// <summary>
        /// Representa una excepcion controlada por el sistema.
        /// </summary>
        /// <param name="codigo">Codigo del error.</param>
        /// <param name="message">Descripcion del error.</param>
        /// <param name="innerException">Excepcion interna.</param>
        protected BaseException(string codigo, string message, Exception innerException)
            : base(message, innerException)
        {
            CodigoError = codigo;
        }

        /// <summary>
        /// Representa una excepcion controlada por el sistema.
        /// </summary>
        /// <param name="info">Informacion de serializacion.</param>
        /// <param name="context">Contexto de la ejecucion.</param>
        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
