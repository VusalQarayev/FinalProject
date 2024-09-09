using Microsoft.AspNetCore.Identity;
using PcStoreBackend.Models;
using System.Threading.Tasks;

namespace PcStoreBackend.Data
{
    public static class SeedData
    {
        public static async Task SeedRolesAndAdminUser(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
           
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            
            var adminUser = new ApplicationUser
            {
                UserName = "admin@pcstore.com",
                Email = "admin@pcstore.com",
                EmailConfirmed = true
            };

            string adminPassword = "Admin@123";

            var user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
