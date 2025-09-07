namespace QuizBar.Services.Results;

public interface IPaginationServiceResponse
{
    public int Page { get; set; }
    public int TotalItems { get; set; }
}