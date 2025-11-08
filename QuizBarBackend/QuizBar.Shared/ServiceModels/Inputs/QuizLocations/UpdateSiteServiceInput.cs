namespace QuizBar.Shared.ServiceModels.Inputs.QuizLocations;

public class UpdateSiteServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public int SiteId { get; set; }
    public string Title { get; set; }
}