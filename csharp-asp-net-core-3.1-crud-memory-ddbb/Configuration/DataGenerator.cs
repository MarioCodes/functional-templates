using crud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace crud.Configuration
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if(context.Users.Any())
                {
                    // there's already data. 
                    return; 
                }

                context.Users.AddRange(
                    new User
                    {
                        Id = 1,
                        Name = "Mario",
                        Email = "nothing@gmail.com"
                    },
                    new User
                    {
                        Id = 2,
                        Name = "Ramon",
                        Email = "something@gmail.com"
                    },
                    new User
                    {
                        Id = 3,
                        Name = "Antonia",
                        Email = "antonia@gmail.com"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
