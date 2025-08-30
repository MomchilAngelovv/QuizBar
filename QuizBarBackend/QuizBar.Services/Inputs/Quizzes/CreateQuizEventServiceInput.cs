namespace QuizBar.Services.Inputs.Quizzes;

public class CreateQuizEventServiceInput : IServiceInput
{
    public required string QuizId { get; set; }
    public required DateTime ScheduledOn { get; set; }
    public required string QuizLocationId { get; set; }
    public IEnumerable<string>? TeamIds { get; set; }

    //IServiceInput
    public required string UserId { get; set; }
}