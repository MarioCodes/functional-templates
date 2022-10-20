using crud.Models;
using crudentityframework.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace crud.Configuration
{
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider, List<User> mockUsers)
        {
            using(var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if(context.Users.Any())
                {
                    // there's already data. 
                    return; 
                }
                context.Users.AddRange(mockUsers);
                context.SaveChanges();
            }
        }
    }
}
