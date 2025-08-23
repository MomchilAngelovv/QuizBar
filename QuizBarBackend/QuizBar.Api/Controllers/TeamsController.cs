using Microsoft.AspNetCore.Mvc;
using QuizBar.Api.Requests.Teams;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Teams;
using QuizBar.Services;
using QuizBar.Services.Inputs.Teams;

namespace QuizBar.Api.Controllers;

public class TeamsController(
    TeamsService teamsService) : BaseController
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
        [FromBody] CreateTeamRequest request, [FromServices] CreateTeamRequestValidator requestValidator)
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