namespace QuizBar.Services.ServiceModels.Results;

public interface IPaginatedServiceResult
{
    public int Page { get; set; }
    public int TotalItems { get; set; }
}