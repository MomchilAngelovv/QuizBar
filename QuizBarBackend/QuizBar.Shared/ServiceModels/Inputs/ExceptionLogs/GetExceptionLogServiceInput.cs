namespace QuizBar.Shared.ServiceModels.Inputs.ExceptionLogs;

public class GetExceptionLogServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public string ExceptionLogId { get; set; }
}