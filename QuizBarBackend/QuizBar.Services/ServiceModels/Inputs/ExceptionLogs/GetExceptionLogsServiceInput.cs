using QuizBar.Services.ServiceModels.Inputs;

namespace QuizBar.Services.ServiceModels.Inputs.ExceptionLogs;

public class GetExceptionLogsServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public int Page { get; set; }
}