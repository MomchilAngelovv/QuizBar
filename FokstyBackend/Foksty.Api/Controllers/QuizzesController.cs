using Foksty.Api.Requests.Quizzes;
using Foksty.Api.Responses;
using Foksty.Api.Responses.Quizzes;
using Foksty.Services;
using Foksty.Services.Inputs.Quizzes;
using Microsoft.AspNetCore.Mvc;

namespace Foksty.Api.Controllers;

public class QuizzesController(
    QuizzesService quizzesService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetQuizResponse>))]
    public async Task<IActionResult> GetQuizAsync(
        [FromQuery] GetQuizRequest request, [FromServices] GetQuizRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceResult = await quizzesService.GetQuizAsync();

        return ControllerResponse(new GetQuizResponse
        {
            Quiz = new GetQuizQuiz 
            { 
                Id = serviceResult.Data.Id,
                Title = serviceResult.Data.Title,
                Scope = serviceResult.Data.Scope,
            }
        });
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetQuizzesResponse>))]
    public async Task<IActionResult> GetQuizzesAsync(
        [FromQuery] GetQuizzesRequest request, [FromServices] GetQuizzesRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var quizzes = await quizzesService.GetQuizzesAsync();

        return ControllerResponse(new GetQuizzesResponse
        {
            Quizzes = quizzes,
            Page = 1,
            TotalItems = 50
        });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<CreateQuizResponse>))]
    public async Task<IActionResult> CreateQuizAsync(
        CreateQuizRequest request, [FromServices] CreateQuizRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new CreateQuizServiceInput
        {
            Title = request.Title,
            Scope = request.Scope,
            CreatedBy = UserId
        };

        var serviceResult = await quizzesService.CreateQuizAsync(serviceInput);

        return ControllerResponse(new CreateQuizResponse
        {
            Id = serviceResult.Data.Id
        });
    }

    [HttpPost("{id}")]
    public async Task<ActionResult<object>> StartQuizAsync(string id)
    {
        return null;
    }
}