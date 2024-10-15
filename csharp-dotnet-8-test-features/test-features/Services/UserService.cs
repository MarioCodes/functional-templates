using testFeatures.Configuration;
using testFeatures.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.interfaces;
using Microsoft.EntityFrameworkCore;

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
            var inputUsersEmail = userModel.Select(user => user.Email)
                .ToList();

            var onlyUsersToDelete = _dbContext.Users.Where(user => inputUsersEmail.Contains(user.Email))
                .ToList();

            var deletedUsers = onlyUsersToDelete.Select(user => user.Active = false).ToList();
            _dbContext.Users.UpdateRange(onlyUsersToDelete);
            _dbContext.SaveChanges();
        }

        public async Task<ResponseUserModel> GetSpecificUser(RequestUserModel userModel)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get all active <see cref="User"/>. There's a global filter set to EF Core inside <see cref="AppDbContext.OnModelCreating(ModelBuilder)"/>
        /// </summary>
        /// <returns>
        /// Returns <see cref="ResponseUserModel"/> which contains only active users
        /// </returns>
        public async Task<ResponseListUserModel> GetUsers()
        {
            List<ResponseUserModel> users = _dbContext.Users.Select(user => MapUserToResponseUser(user))
                .ToList();

            return new ResponseListUserModel
            {
                Result = users
            };
        }

        /// <summary>
        /// Get only (soft) deleted <see cref="User"/>. This invalids the global filter set to EF Core inside <see cref="AppDbContext.OnModelCreating(ModelBuilder)"/>
        /// </summary>
        /// <returns>
        /// Returns <see cref="ResponseListUserModel"/> which contains only (soft) deleted users
        /// </returns>
        public async Task<ResponseListUserModel> GetSoftDeletedUsers()
        {
            List<ResponseUserModel> users = _dbContext.Users.IgnoreQueryFilters()
                .Where(user => !user.Active)
                .Select(user => MapUserToResponseUser(user))
                .ToList();

            return new ResponseListUserModel
            {
                Result = users
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
