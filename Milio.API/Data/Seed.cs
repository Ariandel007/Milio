using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Milio.API.Models;
using Newtonsoft.Json;

namespace Milio.API.Data
{
    public class Seed
    {
        public static void SeedUsers(UserManager<User> userManager,
                                    RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                //create some role
                var roles = new List<Role>
                {
                    new Role{Name = "Client"},
                    new Role{Name = "Admin"},
                    new Role{Name = "Babysitter"},
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }

                foreach (var user in users)
                {

                    userManager.CreateAsync(user, "password").Wait();
                    userManager.AddToRoleAsync(user, "Client").Wait();
                }

                // crear admin user
                var adminUser = new User{
                    UserName = "Admin"
                };

                var result = userManager.CreateAsync(adminUser, "password").Result;

                if (result.Succeeded)
                {
                    var admin = userManager.FindByNameAsync("Admin").Result;
                    userManager.AddToRolesAsync(admin, new [] {"Admin"});
                }

            }
        }
    }
}