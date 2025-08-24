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
        var serviceResult = new ServiceResult<CreateTeamServiceResult>();

        var team = new Team
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = input.Title,
            Status = Status.Active,
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
        var sada = 0;
        var sadasd = 23 / sada;
        var serviceResult = new ServiceResult<UpdateTeamServiceResult>();

        var team = await db.Teams.FirstOrDefaultAsync(t => t.Id == input.TeamId);
        if (team is null)
        {
            serviceResult.Reason = ErrorReason.NotFound;
            return serviceResult;
        }

        if (!string.IsNullOrWhiteSpace(input.Title))
        {
            team.Title = input.Title;
        }


        db.Teams.Update(team);
        await db.SaveChangesAsync();

        return serviceResult;
    }
}