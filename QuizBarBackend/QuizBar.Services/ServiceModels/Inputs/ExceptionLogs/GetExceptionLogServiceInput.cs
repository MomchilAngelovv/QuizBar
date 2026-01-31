using QuizBar.Services.ServiceModels.Inputs;

namespace QuizBar.Services.ServiceModels.Inputs.ExceptionLogs;

public class GetExceptionLogServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public string ExceptionLogId { get; set; }
}