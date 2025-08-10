using Foksty.Api.Requests.Quizzes;
using Foksty.Api.Responses;
using Foksty.Api.Responses.Quizzes;
using Foksty.Services;
using Microsoft.AspNetCore.Mvc;

namespace Foksty.Api.Controllers;

public class QuizzesController(
    QuizzesService quizzesService) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<CreateQuizResponse>))]
    public async Task<IActionResult> CreateQuizAsync(CreateQuizRequest request, [FromServices] CreateQuizRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var quiz = await quizzesService.CreateQuizAsync(LoggedInUserEmail, request.Title, request.Scope);

        return ControllerResponse(new CreateQuizResponse
        {
            Id = quiz.Id
        });
    }

    [HttpGet]
    public async Task<ActionResult<object>> GetQuizzesAsync()
    {
        return null;
    }

    [HttpPost("{id}")]
    public async Task<ActionResult<object>> StartQuizAsync(string id)
    {
        return null;
    }
}