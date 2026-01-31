using QuizBar.Services.ServiceModels.Inputs;

namespace QuizBar.Services.ServiceModels.Inputs.Sites;

public class CreateSiteServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public string Title { get; set; }
}