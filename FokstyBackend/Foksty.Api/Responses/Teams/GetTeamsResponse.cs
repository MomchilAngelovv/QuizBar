using Foksty.Data.Entities;

namespace Foksty.Api.Responses.Teams;

public class GetTeamsResponse : IPaginationResponse
{
    public IEnumerable<Team> Teams { get; set; }
    public int Page { get; set; }
    public int TotalItems { get; set; }
}