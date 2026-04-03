using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Autorizador.Api.ServerCore.Atributos
{
    public class HttpsRequeridoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!String.Equals(context.HttpContext.Request.Scheme,
                "https", StringComparison.OrdinalIgnoreCase))
            {
                var resultado = new ContentResult();
                resultado.StatusCode = 403;
                resultado.Content = "HTTPS Required";

                context.Result = resultado;
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}
