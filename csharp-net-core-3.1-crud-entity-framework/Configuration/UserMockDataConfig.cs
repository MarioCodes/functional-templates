using crud.Models;
using System.Collections.Generic;

namespace crudentityframework.Configuration
{
    public class UserMockDataConfig
    {
        public const string Section = "UserMockDataConfig";

        public List<User> Users { get; set; }
    }
}
