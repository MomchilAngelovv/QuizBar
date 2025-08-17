using Foksty.Data;
using Foksty.Data.Entities;
using Foksty.Services.Inputs.Teams;
using Foksty.Services.Results;
using Foksty.Services.Results.Quizzes;
using Foksty.Services.Results.Teams;
using Foksty.Shared;
using Microsoft.EntityFrameworkCore;

namespace Foksty.Services;

public class TeamsService(
    ApplicationDbContext db)
{
    public async Task<IEnumerable<Team>> GetTeamsAsync()
    {
        var teams = await db.Teams.ToListAsync();
        return teams;
    }

    public async Task<ServiceResult<CreateTeamServiceResult>> CreateTeamAsync(CreateTeamServiceInput input)
    {
        var team = new Team
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = input.Title,
            Status = Status.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = input.CreatedBy,
        };
        
        await db.Teams.AddAsync(team);
        await db.SaveChangesAsync();

        var serviceResult = new ServiceResult<CreateTeamServiceResult>
        {
            Success = true,
            Data = new CreateTeamServiceResult
            {
                Id = team.Id,
            }
        };

        return serviceResult;
    }
}