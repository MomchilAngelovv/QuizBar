using Foksty.Api.Requests.Teams;
using Foksty.Api.Responses;
using Foksty.Api.Responses.Teams;
using Foksty.Services;
using Foksty.Services.Inputs.Teams;
using Microsoft.AspNetCore.Mvc;

namespace Foksty.Api.Controllers;

public class TeamsController
    (TeamsService teamsService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetTeamsResponse>))]
    public async Task<IActionResult> GetTeamsAsync([FromQuery] GetTeamsRequest request, [FromServices] GetTeamsRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var teams = await teamsService.GetTeamsAsync();

        return ControllerResponse(new GetTeamsResponse
        {
            Teams = teams,
            Page = 1,
            TotalItems = 50
        });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<CreateTeamResponse>))]
    public async Task<IActionResult> CreateTeamAsync(
        CreateTeamRequest request, [FromServices] CreateTeamRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new CreateTeamServiceInput
        {
            Title = request.Title,
            CreatedBy = UserId
        };

        var team = await teamsService.CreateTeamAsync(serviceInput);

        return ControllerResponse(new CreateTeamResponse
        {
            Id = team.Data.Id,
        });
    }
}