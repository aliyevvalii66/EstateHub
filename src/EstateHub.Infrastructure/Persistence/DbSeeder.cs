using EstateHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EstateHub.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        string[] roles = { "SuperAdmin", "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        }

        var superAdminEmail = "superadmin@estatehub.az";
        var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);

        if (superAdmin == null)
        {
            superAdmin = new AppUser
            {
                FirstName = "Super",
                LastName = "Admin",
                Email = superAdminEmail,
                UserName = superAdminEmail,
                CreatedAt = DateTime.UtcNow
            };

            await userManager.CreateAsync(superAdmin, "Admin@123");
            await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
            await userManager.AddToRoleAsync(superAdmin, "Admin");
        }
    }
}