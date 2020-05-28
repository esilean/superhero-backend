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
                var users = new List<AppUser>() {
                    new AppUser{
                        DisplayName = "Le Bevilaqua",
                        UserName = "lebevila",
                        Email = "le.bevilaqua@gmail.com"
                    },
                    new AppUser{
                        DisplayName = "Re Fag",
                        UserName = "refag",
                        Email = "refag@gmail.com"
                    },
                    new AppUser{
                        DisplayName = "Jane Doe",
                        UserName = "jane",
                        Email = "jane@gmail.com"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

            }
        }
    }
}