using testFeatures.Configuration;
using testFeatures.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.interfaces;

namespace testFeatures.Services
{
    // implement real database service. 
    public class UserService(IOptions<UserConfig> userConfig) : IUserService
    {

        private UserConfig _userConfig => userConfig.Value;

        public async Task<string> GetRegex()
        {
            return _userConfig.EmailRegex;
        }

        public async Task<string> CreateUser(RequestUserModel userModel)
        {
            return "user created";
        }
        private async Task<User> MapRequestUserToUser(RequestUserModel requestUser, int id)
        {
            return new User
            {
                Id = id,
                Name = requestUser.Name,
                Email = requestUser.Email
            };
        }

        public async Task DeleteUsers(List<RequestUserModel> userModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseUserModel> GetSpecificUser(RequestUserModel userModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseListUserModel> GetUsers()
        {

            return new ResponseListUserModel
            {
                Result = new List<ResponseUserModel>()
            };
        }

        private ResponseUserModel MapUserToResponseUser(User user)
        {
            var userModel = new ResponseUserModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
            return userModel;
        }

        public async Task<string> UpdateUser(RequestUserModel userModel)
        {
            return "user updated";
        }
    }
}
