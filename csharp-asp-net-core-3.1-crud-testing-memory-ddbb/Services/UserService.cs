using crud.Configuration;
using crud.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud.Services
{
    // implement real database service. 
    public class UserService : IUserService
    {
        private ApplicationDbContext _dbContext;
        private UserConfig _userConfig;

        public UserService(ApplicationDbContext dbContext,
            IOptions<UserConfig> userConfig)
        {
            _dbContext = dbContext;
            _userConfig = userConfig.Value;
        }

        public async Task<string> GetRegex()
        {
            return _userConfig.EmailRegex;
        }

        public async Task<string> CreateUser(RequestUserModel userModel)
        {
            var newId = _dbContext.Users
                .Select(x => x.Id).Max()+1;
            User user = await MapRequestUserToUser(userModel, newId);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
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
            var users = _dbContext.Users.ToList();
            List<ResponseUserModel> responseUsers = users.Select(user => MapUserToResponseUser(user))
                .ToList();

            return new ResponseListUserModel
            {
                Result = responseUsers
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
