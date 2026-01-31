using QuizBar.Services.ServiceModels.Inputs;

namespace QuizBar.Services.ServiceModels.Inputs.Sites;

public class UpdateSiteServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public int SiteId { get; set; }
    public string Title { get; set; }
}