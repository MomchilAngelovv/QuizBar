namespace QuizBar.Api.Responses.Sites;

using QuizBar.Shared;

public class GetSitesResponse : IPaginatedControllerResponse
{
    // IPaginatedControllerResponse  
    public int Page { get; set; }
    public int TotalItems { get; set; }

    public IEnumerable<GetQuizLocationsLocation> QuizLocations { get; set; }
}

public class GetQuizLocationsLocation
{
    public string Id { get; set; }
    public string Title { get; set; }
    public Statuses Status { get; set; }
}