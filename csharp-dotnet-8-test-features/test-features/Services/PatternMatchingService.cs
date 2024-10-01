using System.Threading.Tasks;
using testFeatures.Models;
using testFeatures.Services.interfaces;

namespace testFeatures.Services
{
    public class PatternMatchingService : IPatternMatchingService
    {
        public async Task ProcessUser(User user)
        {
            if(user is null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(user.Username))
            {

            }
        }
    }
}
