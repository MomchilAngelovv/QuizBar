using Microsoft.AspNetCore.Mvc;
using QuizBar.Api.Requests.Quizzes;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Quizzes;
using QuizBar.Services;
using QuizBar.Services.Inputs.Quizzes;
using QuizBar.Shared;

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

        var quizzes = await quizzesService.GetQuizzesAsync();

        return ControllerResponse(new GetQuizzesResponse
        {
            Quizzes = quizzes.Select(q => new GetQuizzesQuiz
            {
                Id = q.Id,
                Title = q.Title,
                Status = q.Status
            }),
            Page = 1,
            TotalItems = 50
        });
    }

    [HttpGet("{id:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetQuizResponse>))]
    public async Task<IActionResult> GetQuizAsync(
        [FromRoute] GetQuizRequest request, [FromServices] GetQuizRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceResult = await quizzesService.GetQuizAsync(request.Id);
        if (serviceResult.Data is null)
        {
            return ControllerResponse(ValidationErrors.NotFound("Quiz"), System.Net.HttpStatusCode.NotFound);
        }

        return ControllerResponse(new GetQuizResponse
        {
            Quiz = new GetQuizQuiz 
            { 
                Id = serviceResult.Data.Id,
                Title = serviceResult.Data.Title,
                Scope = serviceResult.Data.Scope,
                Teams = []
            }
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
            Id = serviceResult.Data.Id
        });
    }

    [HttpPost("{id}/start")]
    public async Task<ActionResult<object>> StartQuizAsync(string id)
    {
        return null;
    }

    [HttpPost("{id:required}/events")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<CreateQuizEventResponse>))]
    public async Task<IActionResult> AddTeamsToQuizAsync(
       [FromRoute] string id, [FromBody] CreateQuizEventRequest request, [FromServices] CreateQuizEventRequestValidator requestValidator)
    {
        request.QuizId = id;

        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new CreateQuizEventServiceInput
        {
            QuizId = id,
            ScheduledOn = request.ScheduledOn,
            TeamIds = request.TeamIds,
            UserId = UserId
        };

        var serviceResult = await quizzesService.CreateQuizEventAsync(serviceInput);

        return ControllerResponse(new CreateQuizEventResponse
        {
            Id = serviceResult.Data.Id
        });
    }
}