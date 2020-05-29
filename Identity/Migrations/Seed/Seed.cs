using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Migrations.Seed
{
    public class Seed
    {
        public static async Task SeedData(AppDbContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com"
                    },
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com"
                    },
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Arie22ee@");
                }

            }
        }
    }
}