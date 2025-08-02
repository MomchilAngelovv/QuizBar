using Foksty.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Foksty.Data;

public class ApplicationDbContext : IdentityDbContext<FokstyUser, FokstyRole, string>
{
    public ApplicationDbContext()
    {

    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<TeamResultQuizResult>()
            .HasKey(op => new { op.TeamResultId, op.QuizResultId });

        base .OnModelCreating(builder);
    }

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Question> Questions { get; set; }

    public DbSet<QuizResult> QuizResults { get; set; }
    public DbSet<TeamResultQuizResult> TeamQuizResults { get; set; }
    public DbSet<TeamResult> TeamResults { get; set; }

    public DbSet<Team> Teams { get; set; }

    //TODO make private methods with explicit definition of entityes and validatins
}

public class ApplicationDbContextDesignFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Server=.;Database=FokstyDb;Trusted_Connection=True;TrustServerCertificate=True");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}