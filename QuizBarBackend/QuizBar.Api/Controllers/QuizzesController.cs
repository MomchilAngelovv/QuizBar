using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        var serviceResult = await quizzesService.GetQuizzesAsync(filter);

        return ControllerResponse(new GetQuizzesResponse
        {
            Quizzes = serviceResult.Result.Quizzes.Select(q => new GetQuizzesQuiz
            {
                Id = q.Id,
                Title = q.Title,
                Status = q.Status,
            }),
            Page = 1,
            TotalItems = 50
        });
    }

    [HttpGet("{quizId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetQuizResponse>))]
    public async Task<IActionResult> GetQuizAsync(
        [FromRoute] string quizId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GetQuizRequest? request, [FromServices] GetQuizRequestValidator requestValidator)
    {
        request ??= new GetQuizRequest();
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
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new GetQuizResponse
        {
            Id = serviceResult.Result.Id,
            Title = serviceResult.Result.Title,
            Scope = serviceResult.Result.Scope,
            Chapters = serviceResult.Result.Chapters.Select(chapter => new GetQuizzesChapter
            {
                Id = chapter.Id,
                Title = chapter.Title,
                Status = chapter.Status,
                Questions = chapter.Questions.Select(question => new GetQuizzesChapterQuestion
                {
                    Id = question.Id,
                    Type = question.Type,
                    Text = question.Text,
                    Priority = question.Priority,
                    Status = question.Status,
                    Answers = question.Answers.Select(answer => new GetQuizzesChapterQuestionAnswer
                    {
                        Id = answer.Id,
                        Text = answer.Text,
                        IsCorrect = answer.IsCorrect,
                        Priority = answer.Priority,
                        Status = answer.Status,
                    })
                })
            })
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
        if (serviceResult.Reason != ErrorReason.None)
        {
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new CreateQuizResponse
        {
            Id = serviceResult.Result.Id
        });
    }
}