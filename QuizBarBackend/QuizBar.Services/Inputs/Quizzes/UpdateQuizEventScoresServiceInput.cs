namespace QuizBar.Services.Inputs.Quizzes;

public class UpdateQuizEventScoresServiceInput
{
    public required string QuizEventId { get; set; }
    public required IEnumerable<UpdateQuizEventResultsTeamResult> TeamsScores { get; set; }
}

public class UpdateQuizEventResultsTeamResult
{
    public required string TeamId { get; set; }
    public required double Score { get; set; }
}