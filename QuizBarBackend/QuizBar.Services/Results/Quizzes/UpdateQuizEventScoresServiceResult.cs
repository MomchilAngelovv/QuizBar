namespace QuizBar.Services.Results.Quizzes;

public class UpdateQuizEventScoresServiceResult
{
    public IEnumerable<UpdateQuizEventResultsTeamResult> TeamsScores { get; set; }
}

public class UpdateQuizEventResultsTeamResult
{
    public required string TeamId { get; set; }
    public required double Score { get; set; }
}