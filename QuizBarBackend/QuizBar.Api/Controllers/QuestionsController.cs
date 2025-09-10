using Microsoft.AspNetCore.Mvc;
using QuizBar.Api.Requests.Questions;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Questions;
using QuizBar.Services;
using QuizBar.Services.Inputs.Questions;
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
            Type = request.Type,
            Priority = request.Priority,
            ChapterId = request.ChapterId,
            IsTrue = request.IsTrue,
            PossibleAnswers = request.PossibleAnswers?.Select(possibleAnswer => new CreateQuestionPossibleAnswerServiceInput
            {
                Text = possibleAnswer.Text,
                IsCorrect = possibleAnswer.IsCorrect.GetValueOrDefault(),
                Priority = possibleAnswer.Priority
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
}