using Microsoft.AspNetCore.Mvc;
using QuizBar.Api.Requests.Teams;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Teams;
using QuizBar.Services;
using QuizBar.Services.Inputs.Teams;
using QuizBar.Services.Results;
using QuizBar.Shared;
using System.Net;

namespace QuizBar.Api.Controllers;

public class TeamsController(
    TeamsService teamsService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetTeamsResponse>))]
    public async Task<IActionResult> GetTeamsAsync(
        [FromQuery] GetTeamsRequest request, [FromServices] GetTeamsRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var teams = await teamsService.GetTeamsAsync();

        return ControllerResponse(new GetTeamsResponse
        {
            Teams = teams.Select(t => new GetTeamsTeam
            {
                Id = t.Id,
                Title = t.Title,
            }),
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
            UserId = UserId
        };

        var serviceResult = await teamsService.CreateTeamAsync(serviceInput);

        return ControllerResponse(new CreateTeamResponse
        {
            Id = serviceResult.Result.Id,
        });
    }

    [HttpPut("{teamId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<UpdateTeamResponse>))]
    public async Task<IActionResult> UpdateTeamAsync(
        [FromRoute] string teamId, [FromBody] UpdateTeamRequest request, [FromServices] UpdateTeamRequestValidator requestValidator)
    {
        request.TeamId = teamId;

        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new UpdateTeamServiceInput
        {
            TeamId = request.TeamId,
            Title = request.Title,
            UserId = UserId
        };

        var serviceResult = await teamsService.UpdateTeamAsync(serviceInput);
        if (serviceResult.Reason != ErrorReason.None)
        {
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new UpdateTeamResponse
        {
            Id = serviceResult.Result.Id,
        });
    }
}