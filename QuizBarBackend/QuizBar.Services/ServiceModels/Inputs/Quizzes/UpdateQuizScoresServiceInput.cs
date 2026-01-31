using QuizBar.Services.ServiceModels.Inputs;

namespace QuizBar.Services.ServiceModels.Inputs.Quizzes;

public class UpdateQuizScoresServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public string QuizId { get; set; }
    public IEnumerable<UpdateQuizEventResultsTeamResult> TeamsScores { get; set; }
}

public class UpdateQuizEventResultsTeamResult
{
    public string TeamId { get; set; }
    public double Score { get; set; }
}