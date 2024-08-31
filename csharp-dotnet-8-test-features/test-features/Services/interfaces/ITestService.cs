using System.Threading.Tasks;

namespace testFeatures.Services
{
    public interface ITestService
    {
        Task<string> AsyncTest();
    }
}
