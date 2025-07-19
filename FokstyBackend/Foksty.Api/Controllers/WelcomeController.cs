using Microsoft.AspNetCore.Mvc;

namespace Foksty.Api.Controllers;

public class WelcomeController : BaseController
{
    [HttpGet]
    public IActionResult Welcome()
    {
        return Ok(new
        {
            Text = $"{DateTime.UtcNow} - Foksty Api is working",
            Env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
        });
    }
}