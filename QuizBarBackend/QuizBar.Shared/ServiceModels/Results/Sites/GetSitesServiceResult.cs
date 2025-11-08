namespace QuizBar.Shared.ServiceModels.Results.Sites;

using QuizBar.Shared;
using QuizBar.Shared.ServiceModels.Results;

public class GetSitesServiceResult : IPaginatedServiceResult
{
    // IPaginatedServiceResult
    public int Page { get; set; }
    public int TotalItems { get; set; }

    public IEnumerable<Site> Sites { get; set; }
}

public class Site
{
    public string Id { get; set; }
    public string Title { get; set; }
    public Statuses Status { get; set; }
}