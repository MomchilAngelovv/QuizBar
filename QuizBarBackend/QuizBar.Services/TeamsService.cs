using Microsoft.EntityFrameworkCore;
using QuizBar.Data;
using QuizBar.Data.Entities;
using QuizBar.Services.Inputs.Teams;
using QuizBar.Services.Results;
using QuizBar.Services.Results.Teams;
using QuizBar.Shared;

namespace QuizBar.Services;

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