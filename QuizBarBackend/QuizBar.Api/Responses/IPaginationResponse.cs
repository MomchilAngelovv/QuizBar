namespace QuizBar.Api.Responses;

public interface IPaginationResponse
{
    public int Page { get; set; }
    public int TotalItems { get; set; }
}