using crud.Configuration;
using crud.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud.Services
{
    // implement real database service. 
    public class UserService : IUserService
    {
        private AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateUser(RequestUserModel userModel)
        {
            var newId = _context.Users.Select(x => x.Id).Max()+1;
            User user = await mapRequestUserToUser(userModel, newId);
            _context.Users.Add(user);
            _context.SaveChanges();
            return "user created";
        }
        private async Task<User> mapRequestUserToUser(RequestUserModel requestUser, int id)
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
            var users = _context.Users.ToList();
            List<ResponseUserModel> responseUsers = await mapUserToResponseUser(users);

            return new ResponseListUserModel
            {
                Result = responseUsers
            };
        }

        private async Task<List<ResponseUserModel>> mapUserToResponseUser(List<User> users)
        {
            var outUsers = new List<ResponseUserModel>();
            foreach (var tmpUser in users)
            {
                var userModel = new ResponseUserModel
                {
                    Id = tmpUser.Id,
                    Name = tmpUser.Name,
                    Email = tmpUser.Email
                };
                outUsers.Add(userModel);
            }
            return outUsers;
        }

        public async Task<string> UpdateUser(RequestUserModel userModel)
        {
            return "user updated";
        }
    }
}
