namespace QuizBar.Api.Requests;

using FluentValidation.Results;

public interface IValidatableRequest
{
    Task<ValidationResult> ValidateAsync();
}