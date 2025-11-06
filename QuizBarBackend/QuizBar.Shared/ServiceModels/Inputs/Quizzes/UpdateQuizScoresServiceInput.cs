namespace QuizBar.Shared.ServiceModels.Inputs.Quizzes;

public class UpdateQuizScoresServiceInput
{
    public required string QuizId { get; set; }
    public required IEnumerable<UpdateQuizEventResultsTeamResult> TeamsScores { get; set; }
}

public class UpdateQuizEventResultsTeamResult
{
    public required string TeamId { get; set; }
    public required double Score { get; set; }
}