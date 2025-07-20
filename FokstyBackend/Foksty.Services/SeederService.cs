using Foksty.Data;
using Foksty.Data.Entities;
using Foksty.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Foksty.Services;

public class SeederService(
    Roles roles,
    ApplicationDbContext db,
    UserManager<FokstyUser> userManager,
    RoleManager<FokstyRole> roleManager)
{
    //public async Task<bool> IsDatabaseEmptyAsync()
    //{
    //    //var isDbEmpty = await db.Users.AnyAsync() && await db.Roles.AnyAsync();
    //    //return !isDbEmpty;
    //}

    public async Task SeedAdministratorAndRolesAsync()
    {
        var roleNames = new List<string> { roles.Administrator, roles.Default };
        foreach (var roleName in roleNames)
        {
            var role = new FokstyRole
            {
                Name = roleName,
                Description = $"{roleName}_Description",
                CreatedBy = "Seed",
                CreatedOn = DateTime.UtcNow,
                Status = "Active"
            };

            await roleManager.CreateAsync(role);
        }

        var administrator = new FokstyUser()
        {
            Email = "MomchilAngelov0040@gmail.com",
            UserName = "MomchilAngelov0040@gmail.com",
            CreatedBy = "Seed",
            CreatedOn = DateTime.UtcNow,
            Status = "Active"
        };

        await userManager.CreateAsync(administrator, "1234567890");
        await userManager.AddToRolesAsync(administrator, roleNames);
    }
}