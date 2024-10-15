using testFeatures.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.interfaces
{
    public interface IUserService
    {
        public Task<ResponseListUserModel> GetUsers();
        public Task<ResponseListUserModel> GetSoftDeletedUsers();
        public Task<ResponseUserModel> GetSpecificUser(RequestUserModel userModel);
        public Task<string> CreateUser(RequestUserModel userModel);
        public Task<string> UpdateUser(RequestUserModel userModel);
        public Task DeleteUsers(List<RequestUserModel> userModel);
        public Task<string> GetRegex();
    }
}
