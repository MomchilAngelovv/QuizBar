namespace QuizBar.Api.Responses;

public interface IPaginatedControllerResponse
{
    public int Page { get; set; }
    public int TotalItems { get; set; }
}