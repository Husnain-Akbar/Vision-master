using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Models;
using Vision.Utility;

namespace Vision
{
    public class SeedData
    {
        public static void Seed(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            var users = userManager.GetUsersInRoleAsync(SD.Role_Employee).Result;

            if (userManager.FindByNameAsync("MODsolution@localhost.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "MODsolution@localhost.com",
                    Email = "MODsolution@localhost.com"
                };
                var result = userManager.CreateAsync(user, "P@ssword1").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, SD.Role_Admin).Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(SD.Role_Admin).Result)
            {
                var role = new IdentityRole
                {
                    Name =SD.Role_Admin
                };
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(SD.Role_Employee).Result)
            {
                var role = new IdentityRole
                {
                    Name = SD.Role_Employee
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync(SD.Role_User_Indi).Result)
            {
                var role = new IdentityRole
                {
                    Name = SD.Role_User_Indi
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }

    }
}
