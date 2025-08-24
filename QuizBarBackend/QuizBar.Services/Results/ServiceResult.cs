namespace QuizBar.Services.Results;

public class ServiceResult<TServiceResult> where TServiceResult : class
{
    public ErrorReason Reason { get; set; }
    public TServiceResult? Result { get; set; }
}

public enum ErrorReason 
{
    None = 0,
    NotFound = 1
}