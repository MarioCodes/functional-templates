using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using testFeatures.Middleware.interfaces;

namespace testFeatures.Middleware
{
    public class CustomMiddleware(IMiddlewareService _service) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // custom logic to be executed before next middleware
            await _service.ProcessRequest("middleware processing request");
            await next(context);
            // custom logic to be executed after next middleware
        }
    }
}
