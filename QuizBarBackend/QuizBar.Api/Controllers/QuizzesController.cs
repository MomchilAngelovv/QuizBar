using Microsoft.AspNetCore.Mvc;
using QuizBar.Api.Requests.Quizzes;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Quizzes;
using QuizBar.Services;
using QuizBar.Services.Inputs.Quizzes;
using QuizBar.Services.Results;
using QuizBar.Shared;
using System.Net;

namespace QuizBar.Api.Controllers;

public class QuizzesController(
    QuizzesService quizzesService) : BaseController
{
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

        var filter = new QuizzesFilter
        {
            UserId = UserId
        };

        var servieResult = await quizzesService.GetQuizzesAsync(filter);

        return ControllerResponse(new GetQuizzesResponse
        {
            Quizzes = servieResult.Result.Quizzes.Select(q => new GetQuizzesQuiz
            {
                Id = q.Id,
                Title = q.Title,
                Status = q.Status
            }),
            Page = 1,
            TotalItems = 50
        });
    }

    [HttpGet("{quizId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetQuizResponse>))]
    public async Task<IActionResult> GetQuizAsync(
        [FromRoute] string quizId, [FromBody] GetQuizRequest request, [FromServices] GetQuizRequestValidator requestValidator)
    {
        request.QuizId = quizId;

        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var filter = new QuizzesFilter
        {
            QuizId = request.QuizId
        };

        var serviceResult = await quizzesService.GetQuizAsync(filter);
        if (serviceResult.Reason != ErrorReason.None)
        {
            if (serviceResult.Reason == ErrorReason.NotFound)
            {
                return ControllerResponse(ValidationErrors.NotFound("Quiz"), HttpStatusCode.NotFound);
            }
        }

        return ControllerResponse(new GetQuizResponse
        {
            Id = serviceResult.Result.Id,
            Title = serviceResult.Result.Title,
            Scope = serviceResult.Result.Scope
        });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<CreateQuizResponse>))]
    public async Task<IActionResult> CreateQuizAsync(
        [FromBody] CreateQuizRequest request, [FromServices] CreateQuizRequestValidator requestValidator)
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
            UserId = UserId
        };

        var serviceResult = await quizzesService.CreateQuizAsync(serviceInput);

        return ControllerResponse(new CreateQuizResponse
        {
            Id = serviceResult.Result.Id
        });
    }

    [HttpPost("{quizId:required}/events")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<CreateQuizEventResponse>))]
    public async Task<IActionResult> CreateQuizEventAsync(
        [FromRoute] string quizId, [FromBody] CreateQuizEventRequest request, [FromServices] CreateQuizEventRequestValidator requestValidator)
    {
        request.QuizId = quizId;

        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new CreateQuizEventServiceInput
        {
            QuizId = quizId,
            ScheduledOn = request.ScheduledOn,
            QuizLocationId = request.QuizLocationId,
            TeamIds = request.TeamIds,
            UserId = UserId
        };

        var serviceResult = await quizzesService.CreateQuizEventAsync(serviceInput);

        return ControllerResponse(new CreateQuizEventResponse
        {
            Id = serviceResult.Result.Id
        });
    }

    [HttpPost("{quizId:required}/events/{eventId}/start")]
    public async Task<ActionResult<object>> StartQuizEventAsync(string id)
    {
        return null;
    }
}