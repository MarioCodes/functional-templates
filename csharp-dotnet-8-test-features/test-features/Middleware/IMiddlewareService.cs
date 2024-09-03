using System.Threading.Tasks;

namespace testFeatures.Middleware
{
    public interface IMiddlewareService
    {
        Task ProcessRequest(string request);
    }
}
