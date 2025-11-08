namespace QuizBar.Shared.ServiceModels.Inputs.Sites;

public class UpdateSiteServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public int SiteId { get; set; }
    public string Title { get; set; }
}