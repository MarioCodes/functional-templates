using System.Threading.Tasks;

namespace testFeatures.Services
{
    // implement real database service. 
    public class TestService() : ITestService
    {

        public async Task<string> AsyncTest()
        {
            return "";
        }

    }
}
