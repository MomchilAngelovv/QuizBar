using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizBar.Data;
using QuizBar.Data.Entities;
using QuizBar.Shared;

namespace QuizBar.Services;

public class SeederService(
    ApplicationDbContext db,
    UserManager<FokstyUser> userManager,
    RoleManager<FokstyRole> roleManager)
{
    public async Task SeedAdministratorsAsync()
    {
        var hasUsers = await db.Users.AnyAsync();
        if (hasUsers)
        {
            return;
        }

        var user = new FokstyUser
        {
            Id = Guid.CreateVersion7().ToString(),
            Email = "momchilangelov0040@gmail.com",
            UserName = "momchilangelov0040@gmail.com",
            CreatedBy = "momchilangelov0040@gmail.com",
            Status = Status.Active,
            CreatedOn = DateTime.UtcNow,
        };

        await userManager.CreateAsync(user, "123456");
    }
    public async Task SeedRolesAsync()
    {
        var hasRoles = await db.Roles.AnyAsync();
        if (hasRoles)
        {
            return;
        }

        var roles = new List<Role> { Role.Default, Role.Administrator };
        foreach (var r in roles)
        {
            var role = new FokstyRole
            {
                Id = Guid.CreateVersion7().ToString(),
                Name = r.ToString(),
                Description = r.ToString(),
                Status = Status.Active,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "momchilangelov0040@gmail.com",
            };

            await roleManager.CreateAsync(role);
        }
    }
    public async Task SeedUserRolesAsync()
    {
        var adminUser = await userManager.FindByEmailAsync("momchilangelov0040@gmail.com");
        if (adminUser is null)
        {
            return;
        }

        var isInDefaultRole = await userManager.IsInRoleAsync(adminUser, Role.Administrator.ToString());
        if (!isInDefaultRole)
        {
            await userManager.AddToRolesAsync(adminUser, [Role.Default.ToString(), Role.Default.ToString()]);
        }

        var isInAdministratorRole = await userManager.IsInRoleAsync(adminUser, Role.Administrator.ToString());
        if (!isInAdministratorRole)
        {
            await userManager.AddToRolesAsync(adminUser, [Role.Default.ToString(), Role.Default.ToString()]);
        }
    }
}