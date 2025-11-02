namespace QuizBar.Services.Results.Quizzes;

public class UpdateQuizScoresServiceResult
{
    public IEnumerable<UpdateQuizScoreTeamTeamResult> TeamsScores { get; set; }
}

// TODO figure out naming. I dont like it at all
public class UpdateQuizScoreTeamTeamResult
{
    public required string TeamId { get; set; }
    public required double Score { get; set; }
}