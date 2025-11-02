namespace QuizBar.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using QuizBar.Api.Requests.Questions;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Questions;
using QuizBar.Services;
using QuizBar.Services.Inputs.Questions;
using QuizBar.Services.Results;

public class QuestionsController(
    QuestionsService questionsService) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<CreateQuestionResponse>))]
    public async Task<IActionResult> CreateQuestionAsync(
        [FromBody] CreateQuestionRequest request, [FromServices] CreateQuestionRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new CreateQuestionServiceInput
        {
            Title = request.Title,
            Resource = request.Resource,
            Type = request.Type,
            Priority = request.Priority,
            ChapterId = request.ChapterId,
            Answers = request.Answers.Select(answer => new CreateQuestionAnswerServiceInput
            {
                Title = answer.Title,
                IsCorrect = answer.IsCorrect,
                Priority = answer.Priority
            }),
            UserId = UserId
        };

        var serviceResult = await questionsService.CreateQuestionAsync(serviceInput);
        if (serviceResult.Reason != ErrorReason.None)
        {
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new CreateQuestionResponse
        {
            Id = serviceResult.Result.Id
        });
    }

    [HttpPut("{questionId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<UpdateQuestionResponse>))]
    public async Task<IActionResult> UpdateQuestionAsync(
        [FromRoute] string questionId, [FromBody] UpdateQuestionRequest request, [FromServices] UpdateQuestionRequestValidator requestValidator)
    {
        request.QuestionId = questionId;

        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new UpdateQuestionServiceInput
        {
            QuestionId = request.QuestionId,
            Title = request.Title,
            Resource = request.Resource,
            Type = request.Type,
            Priority = request.Priority,
            ChapterId = request.ChapterId,
            Answers = request.Answers.Select(answer => new UpdateQuestionAnswerServiceInput
            {
                Title = answer.Title,
                IsCorrect = answer.IsCorrect,
                Priority = answer.Priority
            }),
            UserId = UserId
        };

        var serviceResult = await questionsService.UpdateQuestionAsync(serviceInput);
        if (serviceResult.Reason != ErrorReason.None)
        {
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new UpdateQuestionResponse
        {
            Id  = "sdds"
        });
    }
}