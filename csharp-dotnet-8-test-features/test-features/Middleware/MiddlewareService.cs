using System;
using System.Threading.Tasks;
using testFeatures.Middleware.interfaces;

namespace testFeatures.Middleware
{
    public class MiddlewareService : IMiddlewareService
    {
        public async Task ProcessRequest(string request)
        {
            // do something with the request
            Console.WriteLine(request);
        }

    }
}
