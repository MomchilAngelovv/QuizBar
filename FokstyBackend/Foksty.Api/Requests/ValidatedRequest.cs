namespace Foksty.Api.Requests;

public class ValidatedRequest<TRequest> where TRequest : class
{
    public bool IsValid { get; set; }
    public required TRequest ValidatedRequest { get; set; }
}