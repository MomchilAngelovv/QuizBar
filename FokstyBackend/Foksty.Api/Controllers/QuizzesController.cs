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

    [HttpGet("{id}")]
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
            return ControllerResponse(false, "Quiz not found", System.Net.HttpStatusCode.NotFound);
        }

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
            CreatedBy = UserId
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

    [HttpPost("{quizId:required}/teams")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<AddTeamsResponse>))]
    public async Task<IActionResult> AddTeamsAsync(
       [FromRoute] string quizId, [FromBody] AddTeamsRequest request, [FromServices] AddTeamsRequestValidator requestValidator)
    {
        request.QuizId = quizId;

        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new AddTeamsServiceInput
        {
            QuizId = quizId,
            TeamIds = request.TeamIds,
            CreatedBy = UserId
        };

        var serviceResult = await quizzesService.AddTeamsAsync(serviceInput);

        return ControllerResponse(new AddTeamsResponse
        {
            Ids = serviceResult.Data.TeamIds
        });
    }
}