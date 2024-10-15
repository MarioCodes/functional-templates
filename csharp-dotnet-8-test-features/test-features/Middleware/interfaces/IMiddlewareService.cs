using System.Threading.Tasks;

namespace testFeatures.Middleware.interfaces
{
    public interface IMiddlewareService
    {
        Task ProcessRequest(string request);
    }
}
