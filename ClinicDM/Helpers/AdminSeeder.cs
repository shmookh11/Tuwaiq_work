using ClinicDM.Models;
using Microsoft.AspNetCore.Identity;

namespace ClinicDM.Helpers
{
    public static class AdminSeeder
    {

        public static async Task CreateAdminUser(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var adminEmail = "admin@clinic.com";
            var adminPassword = "Admin@123456";

            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, adminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                result = await userManager.AddToRoleAsync(admin, AppRoles.Admin.ToString());
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to add admin user to Admin role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}