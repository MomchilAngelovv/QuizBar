using Foksty.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection.Emit;

namespace Foksty.Data;

public class ApplicationDbContext : IdentityDbContext<FokstyUser, FokstyRole, string>
{
    public ApplicationDbContext()
    {

    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<PossibleAnswer> PossibleAnswers { get; set; }

    public DbSet<QuizResult> QuizResults { get; set; }
    public DbSet<TeamResultQuizResult> TeamQuizResults { get; set; }
    public DbSet<TeamResult> TeamResults { get; set; }

    public DbSet<Team> Teams { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        AddTableQuizzes(builder);
        AddTableChapters(builder);
        AddTableQuestions(builder);
        AddTablePossibleAnswer(builder);

        AddTableQuizResults(builder);
        AddTableTeamResultsQuizResults(builder);
        AddTableTeamResults(builder);

        AddTableTeams(builder);
    }

    private static void AddTableQuizzes(ModelBuilder builder)
    {
        // Main
        builder.Entity<Quiz>().ToTable("Quizzes").HasKey(p => p.Id);
        builder.Entity<Quiz>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<Quiz>().Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Entity<Quiz>().Property(p => p.Scope).IsRequired(false).HasMaxLength(200);

        // IEntityMetaData
        builder.Entity<Quiz>().Property(p => p.Status).IsRequired();
        builder.Entity<Quiz>().Property(p => p.Description).IsRequired(false).HasMaxLength(200);

        builder.Entity<Quiz>().Property(p => p.CreatedOn).IsRequired();
        builder.Entity<Quiz>().Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Entity<Quiz>().Property(p => p.ModifiedOn).IsRequired(false);
        builder.Entity<Quiz>().Property(p => p.ModifiedBy).IsRequired(false).HasMaxLength(100);

        builder.Entity<Quiz>().Property(p => p.DeletedOn).IsRequired(false);
        builder.Entity<Quiz>().Property(p => p.DeletedBy).IsRequired(false).HasMaxLength(100);
    }
    private static void AddTableChapters(ModelBuilder builder)
    {
        // Main
        builder.Entity<Chapter>().ToTable("Chapters").HasKey(p => p.Id);
        builder.Entity<Chapter>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<Chapter>().Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Entity<Chapter>().Property(p => p.Scope).IsRequired(false).HasMaxLength(200);
        builder.Entity<Chapter>().Property(p => p.Priority).IsRequired();

        // Foreign keys
        builder.Entity<Chapter>().Property(p => p.QuizId).IsRequired().HasMaxLength(36);

        // IEntityMetaData
        builder.Entity<Chapter>().Property(p => p.Status).IsRequired();
        builder.Entity<Chapter>().Property(p => p.Description).IsRequired(false).HasMaxLength(200);

        builder.Entity<Chapter>().Property(p => p.CreatedOn).IsRequired();
        builder.Entity<Chapter>().Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Entity<Chapter>().Property(p => p.ModifiedOn).IsRequired(false);
        builder.Entity<Chapter>().Property(p => p.ModifiedBy).IsRequired(false).HasMaxLength(100);

        builder.Entity<Chapter>().Property(p => p.DeletedOn).IsRequired(false);
        builder.Entity<Chapter>().Property(p => p.DeletedBy).IsRequired(false).HasMaxLength(100);
    }
    private static void AddTableQuestions(ModelBuilder builder)
    {
        // Main
        builder.Entity<Question>().ToTable("Questions").HasKey(p => p.Id);
        builder.Entity<Question>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<Question>().Property(p => p.Text).IsRequired().HasMaxLength(500);
        builder.Entity<Question>().Property(p => p.ShowOrder).IsRequired();

        // Foreign keys
        builder.Entity<Question>().Property(p => p.ChapterId).IsRequired().HasMaxLength(36);

        // IEntityMetaData
        builder.Entity<Question>().Property(p => p.Status).IsRequired();
        builder.Entity<Question>().Property(p => p.Description).IsRequired(false).HasMaxLength(200);

        builder.Entity<Question>().Property(p => p.CreatedOn).IsRequired();
        builder.Entity<Question>().Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Entity<Question>().Property(p => p.ModifiedOn).IsRequired(false);
        builder.Entity<Question>().Property(p => p.ModifiedBy).IsRequired(false).HasMaxLength(100);

        builder.Entity<Question>().Property(p => p.DeletedOn).IsRequired(false);
        builder.Entity<Question>().Property(p => p.DeletedBy).IsRequired(false).HasMaxLength(100);
    }
    private static void AddTablePossibleAnswer(ModelBuilder builder)
    {
        // Main
        builder.Entity<PossibleAnswer>().ToTable("PossibleAnswers").HasKey(p => p.Id);
        builder.Entity<PossibleAnswer>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<PossibleAnswer>().Property(p => p.Text).IsRequired().HasMaxLength(500);
        builder.Entity<PossibleAnswer>().Property(p => p.IsCorrect).IsRequired();
        builder.Entity<PossibleAnswer>().Property(p => p.ShowOrder).IsRequired();

        // Foreign keys
        builder.Entity<PossibleAnswer>().Property(p => p.QuestionId).IsRequired().HasMaxLength(36);

        // IEntityMetaData
        builder.Entity<PossibleAnswer>().Property(p => p.Status).IsRequired();
        builder.Entity<PossibleAnswer>().Property(p => p.Description).IsRequired(false).HasMaxLength(200);

        builder.Entity<PossibleAnswer>().Property(p => p.CreatedOn).IsRequired();
        builder.Entity<PossibleAnswer>().Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Entity<PossibleAnswer>().Property(p => p.ModifiedOn).IsRequired(false);
        builder.Entity<PossibleAnswer>().Property(p => p.ModifiedBy).IsRequired(false).HasMaxLength(100);

        builder.Entity<PossibleAnswer>().Property(p => p.DeletedOn).IsRequired(false);
        builder.Entity<PossibleAnswer>().Property(p => p.DeletedBy).IsRequired(false).HasMaxLength(100);
    }
    private static void AddTableQuizResults(ModelBuilder builder)
    {
        // Main
        builder.Entity<QuizResult>().ToTable("QuizResults").HasKey(p => p.Id);
        builder.Entity<QuizResult>().Property(p => p.Id).IsRequired().HasMaxLength(36);

        // Foreign keys
        builder.Entity<QuizResult>().Property(p => p.QuizId).IsRequired().HasMaxLength(36);

        // IEntityMetaData
        builder.Entity<QuizResult>().Property(p => p.Status).IsRequired();
        builder.Entity<QuizResult>().Property(p => p.Description).IsRequired(false).HasMaxLength(200);

        builder.Entity<QuizResult>().Property(p => p.CreatedOn).IsRequired();
        builder.Entity<QuizResult>().Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Entity<QuizResult>().Property(p => p.ModifiedOn).IsRequired(false);
        builder.Entity<QuizResult>().Property(p => p.ModifiedBy).IsRequired(false).HasMaxLength(100);

        builder.Entity<QuizResult>().Property(p => p.DeletedOn).IsRequired(false);
        builder.Entity<QuizResult>().Property(p => p.DeletedBy).IsRequired(false).HasMaxLength(100);
    }
    private static void AddTableTeamResultsQuizResults(ModelBuilder builder)
    {
        // Main
        builder.Entity<TeamResultQuizResult>().ToTable("TeamResultsQuizResults").HasKey(op => new { op.TeamResultId, op.QuizResultId });

        // Foreign keys
        builder.Entity<TeamResultQuizResult>().Property(p => p.TeamResultId).IsRequired().HasMaxLength(36);
        builder.Entity<TeamResultQuizResult>().Property(p => p.QuizResultId).IsRequired().HasMaxLength(36);

        // IEntityMetaData
        builder.Entity<TeamResultQuizResult>().Property(p => p.Status).IsRequired();
        builder.Entity<TeamResultQuizResult>().Property(p => p.Description).IsRequired(false).HasMaxLength(200);

        builder.Entity<TeamResultQuizResult>().Property(p => p.CreatedOn).IsRequired();
        builder.Entity<TeamResultQuizResult>().Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Entity<TeamResultQuizResult>().Property(p => p.ModifiedOn).IsRequired(false);
        builder.Entity<TeamResultQuizResult>().Property(p => p.ModifiedBy).IsRequired(false).HasMaxLength(100);

        builder.Entity<TeamResultQuizResult>().Property(p => p.DeletedOn).IsRequired(false);
        builder.Entity<TeamResultQuizResult>().Property(p => p.DeletedBy).IsRequired(false).HasMaxLength(100);
    }
    private static void AddTableTeamResults(ModelBuilder builder)
    {
        // Main
        builder.Entity<TeamResult>().ToTable("TeamResults").HasKey(tr => tr.Id);
        builder.Entity<TeamResult>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<TeamResult>().Property(p => p.Score).IsRequired();

        // Foreign keys
        builder.Entity<TeamResult>().Property(p => p.TeamId).IsRequired().HasMaxLength(36);

        // IEntityMetaData
        builder.Entity<TeamResult>().Property(p => p.Status).IsRequired();
        builder.Entity<TeamResult>().Property(p => p.Description).IsRequired(false).HasMaxLength(200);

        builder.Entity<TeamResult>().Property(p => p.CreatedOn).IsRequired();
        builder.Entity<TeamResult>().Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Entity<TeamResult>().Property(p => p.ModifiedOn).IsRequired(false);
        builder.Entity<TeamResult>().Property(p => p.ModifiedBy).IsRequired(false).HasMaxLength(100);

        builder.Entity<TeamResult>().Property(p => p.DeletedOn).IsRequired(false);
        builder.Entity<TeamResult>().Property(p => p.DeletedBy).IsRequired(false).HasMaxLength(100);
    }
    private static void AddTableTeams(ModelBuilder builder)
    {
        // Main
        builder.Entity<Team>().ToTable("Teams").HasKey(tr => tr.Id);
        builder.Entity<Team>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<Team>().Property(p => p.Title).IsRequired().HasMaxLength(200);

        // Foreign keys
       
        // IEntityMetaData
        builder.Entity<Team>().Property(p => p.Status).IsRequired();
        builder.Entity<Team>().Property(p => p.Description).IsRequired(false).HasMaxLength(200);

        builder.Entity<Team>().Property(p => p.CreatedOn).IsRequired();
        builder.Entity<Team>().Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Entity<Team>().Property(p => p.ModifiedOn).IsRequired(false);
        builder.Entity<Team>().Property(p => p.ModifiedBy).IsRequired(false).HasMaxLength(100);

        builder.Entity<Team>().Property(p => p.DeletedOn).IsRequired(false);
        builder.Entity<Team>().Property(p => p.DeletedBy).IsRequired(false).HasMaxLength(100);
    }
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