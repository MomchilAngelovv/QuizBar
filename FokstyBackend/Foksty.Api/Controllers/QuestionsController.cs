using Foksty.Api.Requests.Questions;
using Foksty.Api.Responses;
using Foksty.Api.Responses.Questions;
using Foksty.Services;
using Foksty.Services.Inputs.Questions;
using Microsoft.AspNetCore.Mvc;

namespace Foksty.Api.Controllers;

public class QuestionsController(
    QuestionsService questionsService) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<CreateQuestionResponse>))]
    public async Task<IActionResult> CreateQuestionAsync(
        CreateQuestionRequest request, [FromServices] CreateQuestionRequestValidator requestValidator)
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
            PossibleAnswers = request.PossibleAnswers.Select(pa => 
                new CreateQuestionPossibleAnswerServiceInput
                {
                    Text = pa.Text,
                    IsCorrect = pa.IsCorrect,
                    Priority = pa.Priority
                }),
            CreatedBy = UserId
        };

        var serviceResult = await questionsService.CreateQuestionAsync(serviceInput);
        if (!serviceResult.Success)
        {
            // Handle error
        }

        return ControllerResponse(new CreateQuestionResponse
        {
            Id = serviceResult.Data.Id
        });
    }
}