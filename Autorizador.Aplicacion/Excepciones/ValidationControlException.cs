using Autorizador.Core.Excepciones;
using FluentValidation.Results;

namespace Autorizador.Aplicacion.Excepciones;

public class ValidationControlException : BaseException
{
    /// <summary>
    /// Codigo del error fluent validation
    /// </summary>
    private const string CODIGO = "07";
    /// <summary>
    /// Array de errores
    /// </summary>
    public Dictionary<string, string[]> Errors { get; set; }

    public ValidationControlException()
        : base(CODIGO, "Se presentaron uno o mas errores de validacion")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationControlException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}
