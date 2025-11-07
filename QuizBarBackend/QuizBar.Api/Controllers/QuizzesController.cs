namespace QuizBar.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuizBar.Api.Requests.Quizzes;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Quizzes;
using QuizBar.Services;
using QuizBar.Shared.ServiceModels.Inputs.Quizzes;
using QuizBar.Shared.ServiceModels.Results;

//TODO confirm int? for page
public class QuizzesController(
    QuizzesService quizzesService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetQuizzesResponse>))]
    public async Task<IActionResult> GetQuizzesAsync(int page = 1)
    {
        var request = new GetQuizzesRequest { Page = page };

        var validationResult = await request.ValidateAsync();
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new GetQuizzesServiceInput
        {
            UserId = UserId,
            Page = request.Page,
        };

        var serviceResult = await quizzesService.GetQuizzesAsync(serviceInput);

        return ControllerResponse(new GetQuizzesResponse
        {
            Quizzes = serviceResult.Result.Quizzes.Select(q => new GetQuizzesQuiz
            {
                Id = q.Id,
                Title = q.Title,
                Status = q.Status,
            }),
            Page = serviceResult.Result.Page,
            TotalItems = serviceResult.Result.TotalItems
        });
    }

    [HttpGet("{quizId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetQuizResponse>))]
    public async Task<IActionResult> GetQuizAsync(string quizId)
    {
        var request = new GetQuizRequest { QuizId = quizId };

        var validationResult = await request.ValidateAsync();
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
                    Resource = question.Resource,
                    Title = question.Title,
                    Priority = question.Priority,
                    Status = question.Status,
                    Answers = question.Answers.Select(answer => new GetQuizzesChapterQuestionAnswer
                    {
                        Id = answer.Id,
                        Title = answer.Title,
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
    public async Task<IActionResult> CreateQuizAsync(CreateQuizRequest request)
    {
        var validationResult = await request.ValidateAsync();
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