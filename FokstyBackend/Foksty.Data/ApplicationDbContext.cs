using Foksty.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Foksty.Data;

public class ApplicationDbContext : IdentityDbContext<FokstyUser, FokstyRole, string>
{
    public ApplicationDbContext()
    {

    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //TODO This should not be like this but currently throwing some strange error
        optionsBuilder.UseSqlServer("Server=.;Database=FokstyDb;Trusted_Connection=True;");
    }
}