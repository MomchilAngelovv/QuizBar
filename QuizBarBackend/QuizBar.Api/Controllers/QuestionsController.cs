using Microsoft.AspNetCore.Mvc;
using QuizBar.Api.Requests.Questions;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Questions;
using QuizBar.Services;
using QuizBar.Services.Inputs.Questions;
using QuizBar.Services.Results;
using QuizBar.Shared;
using System.Net;

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
            PossibleAnswers = request.PossibleAnswers?.Select(pa => new CreateQuestionPossibleAnswerServiceInput
            {
                Text = pa.Text,
                IsCorrect = pa.IsCorrect.GetValueOrDefault(),
                Priority = pa.Priority
            }),
            UserId = UserId
        };

        var serviceResult = await questionsService.CreateQuestionAsync(serviceInput);
        if (serviceResult.Reason != ErrorReason.None)
        {
            // TODO: This can be done with 1 geneic method and code sohuld look like Return COntrollerRespnse (serviceResult.Reason)
            if (serviceResult.Reason == ErrorReason.QuestionAlreadyExists)
            {
                return ControllerResponse(ValidationErrors.AlreadyExists("Question"), HttpStatusCode.Conflict);
            }

            if (serviceResult.Reason == ErrorReason.ChapterNotFound)
            {
                return ControllerResponse(ValidationErrors.NotFound("Chapter"), HttpStatusCode.NotFound);
            }
        }

        return ControllerResponse(new CreateQuestionResponse
        {
            Id = serviceResult.Result.Id
        });
    }
}