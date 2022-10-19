using crud.Configuration;
using crud.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud.Services
{
    public class UserService : IUserService
    {
        private AppDbContext _dbContext;
        private UserConfig _userConfig;

        public UserService(AppDbContext dbContext,
            IOptions<UserConfig> userConfig)
        {
            _dbContext = dbContext;
            _userConfig = userConfig.Value;
        }

        public async Task<bool> ValidateUserId(string userId)
        {
            return userId != "" 
                && userId.All(char.IsDigit);
        }

        public async Task<bool> UserExists(RequestUserModel userModel)
        {
            ResponseUserModel response = await GetSpecificUserByEmail(userModel.Email);
            return response != null;
        }

        public async Task<string> GetRegex()
        {
            return _userConfig.EmailRegex;
        }

        public async Task<string> InsertUser(RequestUserModel userModel)
        {
            var newId = _dbContext.Users
                .Select(x => x.Id).Max()+1;
            User user = await MapRequestUserToUser(userModel, newId);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return "user created";
        }

        public async Task<string> UpdateUser(RequestUserModel userModel)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Email == userModel.Email);
            user.Name = userModel.Name;
            _dbContext.SaveChanges();
            return "user updated";
        }

        private async Task<User> MapRequestUserToUser(RequestUserModel requestUser, int id)
        {
            return new User
            {
                Id = id,
                Email = requestUser.Email,
                Name = requestUser.Name
            };
        }

        public async Task DeleteUsers(List<RequestUserModel> userModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseUserModel> GetSpecificUser(int userId)
        {
            var user = _dbContext.Users
                .Find(userId);
            return user != null ? MapUserToResponseUser(user) : null;
        }

        public async Task<ResponseUserModel> GetSpecificUserByEmail(string email)
        {
            var user = _dbContext.Users.Where(user => user.Email == email)
                .FirstOrDefault();
            return user != null ? MapUserToResponseUser(user) : null;
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

    }
}
