namespace QuizBar.Shared.ServiceModels.Results.Quizzes;

public class UpdateQuizScoresServiceResult
{
    public IEnumerable<UpdateQuizScoreTeamTeamResult> TeamsScores { get; set; }
}

// TODO figure out naming. I dont like it at all
public class UpdateQuizScoreTeamTeamResult
{
    public string TeamId { get; set; }
    public double Score { get; set; }
}