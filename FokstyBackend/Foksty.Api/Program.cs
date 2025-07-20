using Foksty.Data;
using Foksty.Data.Entities;
using Foksty.Services;
using Foksty.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

try
{
    // Database
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString"]));

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
    builder.Services.AddIdentity<FokstyUser, FokstyRole>(options =>
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
        // options.Filters.Add<EndpointLoggerActionFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

    // OpenAI
    builder.Services.AddOpenApi();

    // Health checks
    builder.Services.AddHealthChecks();

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

    // Custom services
    builder.Services.AddSingleton<ApplicationConstants>();
    builder.Services.AddSingleton<Statuses>();
    builder.Services.AddSingleton<Roles>();

    builder.Services.AddTransient<SeederService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseDeveloperExceptionPage();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapHealthChecks("/health/ready");

    //Executed once just before application starts
    using var scope = app.Services.CreateScope();
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();

        var seederService = scope.ServiceProvider.GetRequiredService<SeederService>();
        await seederService.InitialSeedAdministratorAndRolesAsync();
    }

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    if (ex.InnerException is not null)
    {
        Console.WriteLine(ex.InnerException.Message);
    }

    Console.ReadLine();
}