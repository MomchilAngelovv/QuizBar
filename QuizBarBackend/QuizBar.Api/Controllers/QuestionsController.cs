namespace QuizBar.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizBar.Api.Requests.Questions;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Questions;
using QuizBar.Services;
using QuizBar.Services.ServiceModels.Inputs.Questions;
using QuizBar.Services.ServiceModels.Results;
using System.Runtime.InteropServices;

public class QuestionsController(
    QuestionsService questionsService) : BaseController
{
    // Post
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<CreateQuestionResponse>))]
    public async Task<IActionResult> CreateQuestionAsync(CreateQuestionRequest request)
    {
        var validationResult = await request.ValidateAsync();
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

    // Put
    [HttpPut("{questionId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<UpdateQuestionResponse>))]
    public async Task<IActionResult> UpdateQuestionAsync(UpdateQuestionRequest request, string questionId = default)
    {
        request.QuestionId = questionId;

        var validationResult = await request.ValidateAsync();
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