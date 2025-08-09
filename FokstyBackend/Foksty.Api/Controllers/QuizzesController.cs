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
    [ProducesResponseType(StatusCodes.Status504GatewayTimeout, Type = typeof(ControllerResult<CreateQuizResponse>))]
    public async Task<ActionResult<ControllerResult<CreateQuizResponse>>> CreateQuizAsync(CreateQuizRequest request, [FromServices] CreateQuizRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var newQuiz = await quizzesService.CreateQuizAsync();

        // AI Create Quiz
        return new ControllerResult<CreateQuizResponse>
        {
            HasError = false,
            Message = "",
            Data = new CreateQuizResponse
            {

            },
        };
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