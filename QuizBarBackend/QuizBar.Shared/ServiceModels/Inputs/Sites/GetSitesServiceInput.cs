namespace QuizBar.Shared.ServiceModels.Inputs.Sites;

public class GetSitesServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public int Page { get; set; }
    public SitesFilter Filter { get; set; }
}