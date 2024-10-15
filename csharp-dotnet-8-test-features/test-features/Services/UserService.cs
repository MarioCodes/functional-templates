using testFeatures.Configuration;
using testFeatures.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.interfaces;

namespace testFeatures.Services
{
    // implement real database service.
    public class UserService(AppDbContext _dbContext, IOptions<UserConfig> userConfig) : IUserService
    {

        private UserConfig _userConfig => userConfig.Value;

        public async Task<string> GetRegex()
        {
            return _userConfig.EmailRegex;
        }

        public async Task<string> CreateUser(RequestUserModel userModel)
        {
            var newId = _dbContext.Users.Select(user => user.Id)
                .Max()+1;
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
                Email = requestUser.Email,
                Active = true
            };
        }

        public async Task DeleteUsers(List<RequestUserModel> userModel)
        {
            var usersEmail = userModel.Select(user => user.Email)
                .ToList();

            var usersToDelete = _dbContext.Users.Where(user => usersEmail.Contains(user.Email))
                .ToList();

            var deletedUsers = usersToDelete.Select(user => user.Active = false).ToList();
            _dbContext.Users.UpdateRange(usersToDelete);
            _dbContext.SaveChanges();
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
                Email = user.Email,
                Active = user.Active
            };
            return userModel;
        }

        public async Task<string> UpdateUser(RequestUserModel userModel)
        {
            return "user updated";
        }
    }
}
