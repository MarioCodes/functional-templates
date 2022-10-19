using crud.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crud.Services
{
    public interface IUserService
    {
        public Task<bool> ValidateUserId(string userId);
        public Task<bool> UserExists(RequestUserModel userModel);
        public Task<ResponseListUserModel> GetUsers();

        public Task<ResponseUserModel> GetSpecificUser(int userId);

        public Task<string> InsertUser(RequestUserModel userModel);

        public Task<string> UpdateUser(RequestUserModel userModel);

        public Task DeleteUsers(List<RequestUserModel> userModel);

        public Task<string> GetRegex();
    }
}
