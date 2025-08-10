using Foksty.Data;
using Foksty.Data.Entities;
using Foksty.Shared;
using Microsoft.EntityFrameworkCore;

namespace Foksty.Services;

public class TeamsService(
    ApplicationDbContext db,
    Statuses statuses)
{
    public async Task<IEnumerable<Team>> GetTeamsAsync()
    {
        var teams = await db.Teams.ToListAsync();
        return teams;
    }

    public async Task<Team> CreateTeamAsync(string createdBy, string title)
    {
        var team = new Team
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = title,
            Status = statuses.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = createdBy,
        };

        await db.Teams.AddAsync(team);
        await db.SaveChangesAsync();
        return team;
    }
}