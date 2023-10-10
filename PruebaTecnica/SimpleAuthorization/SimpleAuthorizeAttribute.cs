using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PruebaTecnica.Entities;

namespace PruebaTecnica.SimpleAuthorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SimpleAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //No checkeamos por autorizacion si hay un SimpleAllowAnonymousAttribute en el endpoint
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<SimpleAllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            //Devolvemos un 401 si el usuario no está seteado
            var user = (User?)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new ObjectResult(null) { StatusCode = StatusCodes.Status401Unauthorized };
                context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
            }

        }
    }
}
