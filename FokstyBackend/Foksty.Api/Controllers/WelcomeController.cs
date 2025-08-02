using Foksty.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foksty.Api.Controllers;

[AllowAnonymous]
public class WelcomeController(
    IConfiguration configuration, 
    ApplicationDbContext db) : BaseController
{
    [HttpGet]
    public IActionResult Welcome()
    {
        
        return Ok(new
        {
            Text = $"{DateTime.UtcNow} - Foksty Api is working",
            EnvVar = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            //Conf = configuration.AsEnumerable().Select(kvp => $"{kvp.Key}:{kvp.Value}"),
            //Users = db.Users.ToList()
        });
    }
}