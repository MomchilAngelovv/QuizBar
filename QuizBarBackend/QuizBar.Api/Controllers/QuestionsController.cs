using Microsoft.AspNetCore.Mvc;
using QuizBar.Api.Requests.Questions;
using QuizBar.Api.Requests.Quizzes;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Questions;
using QuizBar.Api.Responses.Quizzes;
using QuizBar.Services;
using QuizBar.Services.Inputs.Questions;
using QuizBar.Services.Inputs.Quizzes;
using QuizBar.Services.Results;

namespace QuizBar.Api.Controllers;

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
            Text = request.Text,
            ResourceUrl = request.ResourceUrl,
            Type = request.Type,
            Priority = request.Priority,
            ChapterId = request.ChapterId,
            Answers = request.Answers.Select(answer => new CreateQuestionAnswerServiceInput
            {
                Text = answer.Text,
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
            Text = request.Text,
            ResourceUrl = request.ResourceUrl,
            Type = request.Type,
            Priority = request.Priority,
            ChapterId = request.ChapterId,
            Answers = request.Answers.Select(answer => new UpdateQuestionAnswerServiceInput
            {
                Text = answer.Text,
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