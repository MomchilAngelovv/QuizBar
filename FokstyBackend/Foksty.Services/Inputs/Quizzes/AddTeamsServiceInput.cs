namespace Foksty.Services.Inputs.Quizzes;

public class AddTeamsServiceInput : IServiceInput
{
    public string QuizId { get; set; }
    public IEnumerable<string> TeamIds { get; set; }

    //IServiceInput
    public required string CreatedBy { get; set; }
}