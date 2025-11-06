namespace QuizBar.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuizBar.Api.Requests.Teams;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Teams;
using QuizBar.Services;
using QuizBar.Shared.ServiceModels.Inputs.Teams;
using QuizBar.Shared.ServiceModels.Results;

public class TeamsController(
    TeamsService teamsService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetTeamsResponse>))]
    public async Task<IActionResult> GetTeamsAsync(
        [FromQuery] int? page, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GetTeamsRequest? request)
    {
        request ??= new GetTeamsRequest();
        request.Page = page ?? 1;

        var validationResult = await request.ValidateAsync();
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new GetTeamsServiceInput
        {
            Page = request.Page,
            UserId = UserId
        };

        var serviceResult = await teamsService.GetTeamsAsync(serviceInput);
        if (serviceResult.Reason != ErrorReason.None)
        {
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new GetTeamsResponse
        {
            Teams = serviceResult.Result.Teams.Select(t => new GetTeamsTeam
            {
                Id = t.Id,
                Title = t.Title,
            }),
            Page = serviceResult.Result.Page,
            TotalItems = serviceResult.Result.TotalItems
        });
    }

    [HttpGet("{teamId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetTeamResponse>))]
    public async Task<IActionResult> GetTeamAsync(string teamId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GetTeamRequest? request)
    {
        request ??= new GetTeamRequest();
        request.TeamId = teamId;

        var validationResult = await request.ValidateAsync();
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new GetTeamServiceInput
        {
            TeamId = request.TeamId,
            UserId = UserId
        };

        var serviceResult = await teamsService.GetTeamAsync(serviceInput);
        if (serviceResult.Reason != ErrorReason.None)
        {
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new GetTeamResponse
        {
            Id = serviceResult.Result.Id,
            Title = serviceResult.Result.Title,
            Status = serviceResult.Result.Status,
            Results = serviceResult.Result.Results.Select(r => new GetTeamResultServiceResult
            {
                Score = r.Score,
                TeamId = r.TeamId
            })
        });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<CreateTeamResponse>))]
    public async Task<IActionResult> CreateTeamAsync(CreateTeamRequest request)
    {
        var validationResult = await request.ValidateAsync();
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
        if (serviceResult.Reason != ErrorReason.None)
        {
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new CreateTeamResponse
        {
            Id = serviceResult.Result.Id,
        });
    }

    [HttpPut("{teamId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<UpdateTeamResponse>))]
    public async Task<IActionResult> UpdateTeamAsync(string teamId, UpdateTeamRequest request)
    {
        request.TeamId = teamId;

        var validationResult = await request.ValidateAsync();
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