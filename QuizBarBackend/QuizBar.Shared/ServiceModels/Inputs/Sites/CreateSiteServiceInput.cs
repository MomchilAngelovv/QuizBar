namespace QuizBar.Shared.ServiceModels.Inputs.Sites;

public class CreateSiteServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public string Title { get; set; }
}