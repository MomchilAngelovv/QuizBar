namespace QuizBar.Services;

using Microsoft.EntityFrameworkCore;
using QuizBar.Data;
using QuizBar.Data.Entities;
using QuizBar.Services.ServiceModels.Inputs.Teams;
using QuizBar.Services.ServiceModels.Results;
using QuizBar.Services.ServiceModels.Results.Teams;
using QuizBar.Shared;

public class TeamsService(ApplicationDbContext db)
{
    public async Task<ServiceResult<GetTeamsServiceResult>> GetTeamsAsync(GetTeamsServiceInput input)
    {
        var serviceResult = new ServiceResult<GetTeamsServiceResult>();

        var teamsQuery = db.Teams
            .AsQueryable();

        if (input.Filter is not null)
        {
            if (!string.IsNullOrWhiteSpace(input.UserId))
            {
                teamsQuery = teamsQuery
                    .Where(team => team.CreatedBy == input.UserId);
            }
        }

        //TODO Make general pagination parameters and response
        var teams = await teamsQuery
            .OrderBy(q => q.Title)
            .Skip((input.Page - 1) * 10)
            .Take(10)
            .ToListAsync();

        var teamsTotalItems = await db.Teams
            .CountAsync();

        serviceResult.Result = new GetTeamsServiceResult
        {
            Teams = teams.Select(team => new GetTeamsTeam
            {
                Id = team.Id,
                Title = team.Title,
                Status = team.Status
            }),
            Page = input.Page,
            TotalItems = teamsTotalItems
        };

        return serviceResult;
    }
    public async Task<ServiceResult<GetTeamServiceResult>> GetTeamAsync(GetTeamServiceInput input)
    {
        var serviceResult = new ServiceResult<GetTeamServiceResult>();

        var team = await db.Teams
            .FirstOrDefaultAsync(t => t.Id == input.TeamId);
        if (team is null)
        {
            serviceResult.Reason = ErrorReason.TeamNotFound;
            return serviceResult;
        }

        serviceResult.Result = new GetTeamServiceResult
        {
            Id = team.Id,
            Title = team.Title,
            Status = team.Status
        };

        return serviceResult;
    }
    public async Task<ServiceResult<CreateTeamServiceResult>> CreateTeamAsync(CreateTeamServiceInput input)
    {
        var serviceResult = new ServiceResult<CreateTeamServiceResult>();

        var teamExists = await db.Teams
            .AnyAsync(team => team.Title == input.Title && team.CreatedBy == input.UserId);
        if (teamExists)
        {
            serviceResult.Reason = ErrorReason.TeamAlreadyExists;
            return serviceResult;
        }

        var team = new Team
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = input.Title,
            Status = Statuses.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = input.UserId,
        };
        
        await db.Teams.AddAsync(team);
        await db.SaveChangesAsync();

        serviceResult.Result = new CreateTeamServiceResult
        {
            Id = team.Id,
        };

        return serviceResult;
    }
    public async Task<ServiceResult<UpdateTeamServiceResult>> UpdateTeamAsync(UpdateTeamServiceInput input)
    {
        var serviceResult = new ServiceResult<UpdateTeamServiceResult>();

        var team = await db.Teams
            .FirstOrDefaultAsync(t => t.Id == input.TeamId);
        if (team is null)
        {
            serviceResult.Reason = ErrorReason.TeamNotFound;
            return serviceResult;
        }

        if (!string.IsNullOrWhiteSpace(input.Title))
        {
            team.Title = input.Title;
        }

        team.ModifiedOn = DateTime.UtcNow;
        team.ModifiedBy = input.UserId;

        db.Teams.Update(team);
        await db.SaveChangesAsync();

        serviceResult.Result = new UpdateTeamServiceResult
        {
            Id = team.Id
        };

        return serviceResult;
    }
}