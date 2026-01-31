namespace QuizBar.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using QuizBar.Data.Entities;

public class ApplicationDbContext : IdentityDbContext<QuizBarUser, QuizBarRole, string>
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<TeamQuestionAnswer> TeamsAnswers { get; set; }
    public DbSet<Team> Teams { get; set; }

    public DbSet<Site> Sites { get; set; }

    public DbSet<ExceptionLog> ExceptionLogs { get; set; }
    
    private static void ConfigureQuizzes(ModelBuilder builder)
    {
        // Main
        builder.Entity<Quiz>().ToTable("Quizzes").HasKey(p => p.Id);
        builder.Entity<Quiz>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<Quiz>().Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Entity<Quiz>().Property(p => p.Scope).IsRequired(false).HasMaxLength(200);

        // Foreign keys

        ConfigureIEntityMetaData<Quiz>(builder);
    }
    private static void ConfigureChapters(ModelBuilder builder)
    {
        // Main
        builder.Entity<Chapter>().ToTable("Chapters").HasKey(p => p.Id);
        builder.Entity<Chapter>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<Chapter>().Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Entity<Chapter>().Property(p => p.Scope).IsRequired(false).HasMaxLength(200);
        builder.Entity<Chapter>().Property(p => p.Priority).IsRequired();

        // Foreign keys
        builder.Entity<Chapter>().Property(p => p.QuizId).IsRequired().HasMaxLength(36);

        ConfigureIEntityMetaData<Chapter>(builder);
    }
    private static void ConfigureQuestions(ModelBuilder builder)
    {
        // Main
        builder.Entity<Question>().ToTable("Questions").HasKey(p => p.Id);
        builder.Entity<Question>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<Question>().Property(p => p.Title).IsRequired().HasMaxLength(500);
        builder.Entity<Question>().Property(p => p.Resource).IsRequired(false);
        builder.Entity<Question>().Property(p => p.Priority).IsRequired();
        builder.Entity<Question>().Property(p => p.Score).IsRequired();

        // Foreign keys
        builder.Entity<Question>().Property(p => p.ChapterId).IsRequired().HasMaxLength(36);

        ConfigureIEntityMetaData<Question>(builder);
    }
    private static void ConfigureAnswers(ModelBuilder builder)
    {
        // Main
        builder.Entity<Answer>().ToTable("Answers").HasKey(p => p.Id);
        builder.Entity<Answer>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<Answer>().Property(p => p.Title).IsRequired().HasMaxLength(500);
        builder.Entity<Answer>().Property(p => p.Priority).IsRequired();

        // Foreign keys
        builder.Entity<Answer>().Property(p => p.QuestionId).IsRequired().HasMaxLength(36);

        ConfigureIEntityMetaData<Answer>(builder);
    }
    private static void ConfigureTeamsAnswers(ModelBuilder builder)
    {
        // Main
        builder.Entity<TeamQuestionAnswer>().ToTable("TeamsAnswers").HasKey(tr => tr.Id);
        builder.Entity<TeamQuestionAnswer>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<TeamQuestionAnswer>().Property(p => p.Title).IsRequired().HasMaxLength(200);

        // Foreign keys

        ConfigureIEntityMetaData<TeamQuestionAnswer>(builder);
    }
    private static void ConfigureTeams(ModelBuilder builder)
    {
        // Main
        builder.Entity<Team>().ToTable("Teams").HasKey(tr => tr.Id);
        builder.Entity<Team>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<Team>().Property(p => p.Title).IsRequired().HasMaxLength(200);

        // Foreign keys

        ConfigureIEntityMetaData<Team>(builder);
    }

    private static void ConfigureSites(ModelBuilder builder)
    {
        // Main
        builder.Entity<Site>().ToTable("Sites").HasKey(p => p.Id);
        builder.Entity<Site>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<Site>().Property(p => p.Id).IsRequired().HasMaxLength(200);

        // Foreign keys

        ConfigureIEntityMetaData<Site>(builder);
    }

    private static void ConfigureExceptionLogs(ModelBuilder builder)
    {
        // Main
        builder.Entity<ExceptionLog>().ToTable("ExceptionLogs").HasKey(p => p.Id);
        builder.Entity<ExceptionLog>().Property(p => p.Id).IsRequired().HasMaxLength(36);
        builder.Entity<ExceptionLog>().Property(p => p.Message).IsRequired();
        builder.Entity<ExceptionLog>().Property(p => p.StackTrace).IsRequired();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureQuizzes(builder);
        ConfigureChapters(builder);
        ConfigureQuestions(builder);
        ConfigureAnswers(builder);
        ConfigureTeamsAnswers(builder);
        ConfigureTeams(builder);

        ConfigureSites(builder);

        ConfigureExceptionLogs(builder);
    }
    private static void ConfigureIEntityMetaData<T>(ModelBuilder builder) where T : class, IEntityMetaData
    {
        builder.Entity<T>().Property(p => p.Status).IsRequired();
        builder.Entity<T>().Property(p => p.Description).IsRequired(false).HasMaxLength(200);

        builder.Entity<T>().Property(p => p.CreatedOn).IsRequired();
        builder.Entity<T>().Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Entity<T>().Property(p => p.ModifiedOn).IsRequired(false);
        builder.Entity<T>().Property(p => p.ModifiedBy).IsRequired(false).HasMaxLength(100);

        builder.Entity<T>().Property(p => p.DeletedOn).IsRequired(false);
        builder.Entity<T>().Property(p => p.DeletedBy).IsRequired(false).HasMaxLength(100);
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