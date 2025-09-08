namespace QuizBar.Api.Responses.Quizzes;

public class UpdateQuizEventScoresResponse
{
    public required IEnumerable<UpdateQuizEventResultsTeamResult> TeamsScores { get; set; }
}

public class UpdateQuizEventResultsTeamResult
{
    public required string TeamId { get; set; }
    public required double Score { get; set; }
}