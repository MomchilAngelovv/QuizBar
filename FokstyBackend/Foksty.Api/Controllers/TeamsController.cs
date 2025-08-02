using Microsoft.AspNetCore.Mvc;

namespace Foksty.Api.Controllers;

public class TeamsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<object>> CreateTeamAsync()
    {

    }

    [HttpGet]
    public async Task<ActionResult<object>> GetTeamsAsync()
    {

    }
}
