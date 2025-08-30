using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using QuizBar.Data.Entities;

namespace QuizBar.Data;

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

    public DbSet<QuizEvent> QuizEvents { get; set; }
    public DbSet<QuizLocation> QuizLocations { get; set; }
    public DbSet<TeamQuizEvent> TeamsQuizEvents { get; set; }

    public DbSet<Team> Teams { get; set; }

    public DbSet<ExceptionLog> ExceptionLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        AddTableQuizzes(builder);
        AddTableChapters(builder);
        AddTableQuestions(builder);
        AddTablePossibleAnswer(builder);

        AddTableQuizEvents(builder);
        AddTableQuizLocations(builder);
        AddTableTeamsQuizEvents(builder);

        AddTableTeams(builder);

        AddTableExceptionLogs(builder);
    }

    private static void AddTableQuizzes(ModelBuilder builder)
    {
        // Main
        builder.Entity<Quiz>().ToTable("Quizzes").HasKey(p => p.Id);
        builder.Entity<Quiz>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<Quiz>().Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Entity<Quiz>().HasIndex(p => p.Title).IsUnique();
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
        builder.Entity<Chapter>().HasIndex(p => p.Title).IsUnique();
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
        builder.Entity<Question>().HasIndex(p => p.Text).IsUnique();
        builder.Entity<Question>().Property(p => p.Priority).IsRequired();

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
        builder.Entity<PossibleAnswer>().HasIndex(p => p.Text).IsUnique();
        builder.Entity<PossibleAnswer>().Property(p => p.IsCorrect).IsRequired();
        builder.Entity<PossibleAnswer>().Property(p => p.Priority).IsRequired();

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
    private static void AddTableQuizEvents(ModelBuilder builder)
    {
        // Main
        builder.Entity<QuizEvent>().ToTable("QuizEvents").HasKey(p => p.Id);
        builder.Entity<QuizEvent>().Property(p => p.Id).IsRequired().HasMaxLength(36);

        // Foreign keys
        builder.Entity<QuizEvent>().Property(p => p.QuizId).IsRequired().HasMaxLength(36);

        // IEntityMetaData
        builder.Entity<QuizEvent>().Property(p => p.Status).IsRequired();
        builder.Entity<QuizEvent>().Property(p => p.Description).IsRequired(false).HasMaxLength(200);

        builder.Entity<QuizEvent>().Property(p => p.CreatedOn).IsRequired();
        builder.Entity<QuizEvent>().Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Entity<QuizEvent>().Property(p => p.ModifiedOn).IsRequired(false);
        builder.Entity<QuizEvent>().Property(p => p.ModifiedBy).IsRequired(false).HasMaxLength(100);

        builder.Entity<QuizEvent>().Property(p => p.DeletedOn).IsRequired(false);
        builder.Entity<QuizEvent>().Property(p => p.DeletedBy).IsRequired(false).HasMaxLength(100);
    }
    private static void AddTableQuizLocations(ModelBuilder builder)
    {
        // Main
        builder.Entity<QuizLocation>().ToTable("QuizLocations").HasKey(p => p.Id);
        builder.Entity<QuizLocation>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<QuizLocation>().Property(p => p.Id).IsRequired().HasMaxLength(200);

        // Foreign keys

        // IEntityMetaData
        builder.Entity<QuizLocation>().Property(p => p.Status).IsRequired();
        builder.Entity<QuizLocation>().Property(p => p.Description).IsRequired(false).HasMaxLength(200);

        builder.Entity<QuizLocation>().Property(p => p.CreatedOn).IsRequired();
        builder.Entity<QuizLocation>().Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Entity<QuizLocation>().Property(p => p.ModifiedOn).IsRequired(false);
        builder.Entity<QuizLocation>().Property(p => p.ModifiedBy).IsRequired(false).HasMaxLength(100);

        builder.Entity<QuizLocation>().Property(p => p.DeletedOn).IsRequired(false);
        builder.Entity<QuizLocation>().Property(p => p.DeletedBy).IsRequired(false).HasMaxLength(100);
    }
    private static void AddTableTeamsQuizEvents(ModelBuilder builder)
    {
        // Main
        builder.Entity<TeamQuizEvent>().ToTable("TeamsQuizEvents").HasKey(op => op.Id);
        builder.Entity<TeamQuizEvent>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<TeamQuizEvent>().Property(p => p.Score).IsRequired();

        // Foreign keys
        builder.Entity<TeamQuizEvent>().Property(p => p.TeamId).IsRequired().HasMaxLength(36);
        builder.Entity<TeamQuizEvent>().Property(p => p.QuizEventId).IsRequired().HasMaxLength(36);

        // IEntityMetaData
        builder.Entity<TeamQuizEvent>().Property(p => p.Status).IsRequired();
        builder.Entity<TeamQuizEvent>().Property(p => p.Description).IsRequired(false).HasMaxLength(200);

        builder.Entity<TeamQuizEvent>().Property(p => p.CreatedOn).IsRequired();
        builder.Entity<TeamQuizEvent>().Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Entity<TeamQuizEvent>().Property(p => p.ModifiedOn).IsRequired(false);
        builder.Entity<TeamQuizEvent>().Property(p => p.ModifiedBy).IsRequired(false).HasMaxLength(100);

        builder.Entity<TeamQuizEvent>().Property(p => p.DeletedOn).IsRequired(false);
        builder.Entity<TeamQuizEvent>().Property(p => p.DeletedBy).IsRequired(false).HasMaxLength(100);
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
    private static void AddTableExceptionLogs(ModelBuilder builder)
    {
        // Main
        builder.Entity<ExceptionLog>().ToTable("ExceptionLogs").HasKey(p => p.Id);
        builder.Entity<ExceptionLog>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<ExceptionLog>().Property(p => p.Message).IsRequired();
        builder.Entity<ExceptionLog>().Property(p => p.StackTrace).IsRequired();
    }
}

public class ApplicationDbContextDesignFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Server=.;Database=QuizBar;Trusted_Connection=True;TrustServerCertificate=True");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}