using System.Text;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using FluentValidation;
using Scalar.AspNetCore;

using QuizBar.Api.Utilities;
using QuizBar.Data;
using QuizBar.Data.Entities;
using QuizBar.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});

// Identity
builder.Services.AddIdentity<QuizBarUser, QuizBarRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// OpenAI
builder.Services.AddOpenApi();

// Health checks
builder.Services.AddHealthChecks();

builder.Services.AddHttpClient("test", client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/v1/");
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["OpenAI:ApiKey"]}");
}).AddResilienceHandler("resiliencePolicy", pipeLine => 
{
    pipeLine.AddRetry(new Polly.Retry.RetryStrategyOptions<HttpResponseMessage>
    {
        MaxRetryAttempts = 3,
        BackoffType = DelayBackoffType.Exponential,
        Delay = TimeSpan.FromSeconds(2),
        //OnRetry = (outcome) =>
        //{
        //    var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
        //    logger.LogWarning($"Retrying due to: {outcome.Exception?.Message ?? outcome.Result.ReasonPhrase}. Attempt {retryAttempt}.");
        //}
    });
});

// Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(options =>
    {
        options.AllowAnyOrigin();
        options.AllowAnyMethod();
        options.AllowAnyHeader();
    });
});

// Fluent Validation validators
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Custom services
builder.Services.AddTransient<SeederService>();
builder.Services.AddTransient<QuizzesService>();
builder.Services.AddTransient<TeamsService>();
builder.Services.AddTransient<QuestionsService>();
builder.Services.AddTransient<SitesService>();
builder.Services.AddTransient<ExceptionLogsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("api/health/ready");
app.MapOpenApi("/api/openapi.json");
app.MapScalarApiReference("/api/scalar", options =>
{
    options.OpenApiRoutePattern = "/api/openapi.json";
});
app.MapControllers();

// Executed once just before application starts
using var scope = app.Services.CreateScope();
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();

    var seederService = scope.ServiceProvider.GetRequiredService<SeederService>();
    await seederService.SeedAdministratorsAsync();
    await seederService.SeedRolesAsync();
    await seederService.SeedUserRolesAsync();
    await seederService.SeedSitesAsync();
}

app.Run();