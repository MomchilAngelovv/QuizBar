namespace QuizBar.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using QuizBar.Api.Requests.Teams;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Teams;
using QuizBar.Services;
using QuizBar.Services.ServiceModels.Inputs.Teams;
using QuizBar.Services.ServiceModels.Results;

public class TeamsController(
    TeamsService teamsService) : BaseController
{   
    // Get
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetTeamsResponse>))]
    public async Task<IActionResult> GetTeamsAsync(int page = 1)
    {
        var request = new GetTeamsRequest { Page = page };

        var validationResult = await request.ValidateAsync();
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new GetTeamsServiceInput
        {
            UserId = UserId,
            Page = request.Page,
        };

        var serviceResult = await teamsService.GetTeamsAsync(serviceInput);
        if (serviceResult.Reason != ErrorReason.None)
        {
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new GetTeamsResponse
        {
            Page = serviceResult.Result.Page,
            TotalItems = serviceResult.Result.TotalItems,
            Teams = serviceResult.Result.Teams.Select(team => new Team
            {
                Id = team.Id,
                Title = team.Title,
            })
        });
    }

    [HttpGet("{teamId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetTeamResponse>))]
    public async Task<IActionResult> GetTeamAsync(string teamId = default)
    {
        var request = new GetTeamRequest { TeamId = teamId };

        var validationResult = await request.ValidateAsync();
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new GetTeamServiceInput
        {
            UserId = UserId,
            TeamId = request.TeamId
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
            Status = serviceResult.Result.Status
        });
    }

    // Post
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

    // Put
    [HttpPut("{teamId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<UpdateTeamResponse>))]
    public async Task<IActionResult> UpdateTeamAsync(UpdateTeamRequest request, string teamId = default)
    {
        request.TeamId = teamId;

        var validationResult = await request.ValidateAsync();
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new UpdateTeamServiceInput
        {
            UserId = UserId,
            TeamId = request.TeamId,
            Title = request.Title
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

    // Delete
    [HttpDelete("{teamId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<DeleteTeamResponse>))]
    public async Task<IActionResult> DeleteTeamAsync(string teamId)
    {
        var request = new DeleteTeamRequest { TeamId = teamId };

        var validationResult = await request.ValidateAsync();
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new DeleteTeamServiceInput
        {
            UserId = UserId,
            TeamId = request.TeamId
        };

        var serviceResult = await teamsService.DeleteTeamAsync(serviceInput);
        if (serviceResult.Reason != ErrorReason.None)
        {
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new DeleteTeamResponse
        {
            Id = serviceResult.Result.Id,
        });
    }
}