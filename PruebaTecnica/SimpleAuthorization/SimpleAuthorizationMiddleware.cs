using PruebaTecnica.Services;
using System.Net.Http.Headers;
using System.Text;

namespace PruebaTecnica.SimpleAuthorization
{
    public class SimpleAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public SimpleAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter!);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                var username = credentials[0];
                var password = credentials[1];

                context.Items["User"] = userService.Authenticate(username, password);
            }
            catch
            {
                //No hay un header de Authorization válido o el parámetro esta seteado en NULL
            }

            await _next(context);
        }
    }
}
