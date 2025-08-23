using QuizBar.Api.Responses;
using QuizBar.Data.Entities;

namespace QuizBar.Api.Responses.Teams;

public class GetTeamsResponse : IPaginationResponse
{
    public IEnumerable<Team> Teams { get; set; }
    public int Page { get; set; }
    public int TotalItems { get; set; }
}