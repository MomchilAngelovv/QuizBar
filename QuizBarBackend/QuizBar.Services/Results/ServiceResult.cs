namespace QuizBar.Services.Results;

public class ServiceResult<TServiceResult> where TServiceResult : class
{
    public ErrorReason Reason { get; set; }
    public TServiceResult? Result { get; set; }
}

public enum ErrorReason 
{
    None = 0,

    NotFound = 1000,
    QuestionNotFound = 1001,
    ChapterNotFound = 1002,
    QuizNotFound = 1003,
    QuizEventNotFound = 1004,

    AlreadyExists = 2000,
    QuestionAlreadyExists = 2001,
    ChapterAlreadyExists = 2002,
    QuizAlreadyExists = 2003,
    QuizEventAlreadyExists = 2004,

    IncompatibleData = 10000
}