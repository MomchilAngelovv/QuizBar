namespace QuizBar.Services.Results;

public interface IPaginatedServiceResult
{
    public int Page { get; set; }
    public int TotalItems { get; set; }
}