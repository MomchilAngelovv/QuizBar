using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuizBar.Api.Requests.QuizLocations;
using QuizBar.Api.Requests.Quizzes;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.QuizLocations;
using QuizBar.Api.Responses.Quizzes;
using QuizBar.Services;
using QuizBar.Services.Inputs.QuizLocations;
using QuizBar.Services.Inputs.Quizzes;
using QuizBar.Services.Results;

namespace QuizBar.Api.Controllers;

public class QuizLocationsController(
    QuizLocationsService quizLocationsService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetQuizzesResponse>))]
    public async Task<IActionResult> GetQuizLocationsAsync(
        [FromQuery] int? page, [FromBody] GetQuizLocationsRequest? request, [FromServices] GetQuizLocationsRequestValidator requestValidator)
    {
        request ??= new GetQuizLocationsRequest();
        request.Page = page ?? 1;
        
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new GetQuizLocationsServiceInput
        {
            Page = request.Page,
            Filter = new QuizLocationsFilter
            {
                UserId = UserId
            }
        };

        var servieResult = await quizLocationsService.GetQuizLocationsAsync(serviceInput);

        return ControllerResponse(new GetQuizLocationsResponse
        {
            QuizLocations = servieResult.Result.QuizLocations.Select(quizLocation => new GetQuizLocationsLocation
            {
                Id = quizLocation.Id,
                Site = quizLocation.Site,
                Status = quizLocation.Status
            }),
            Page = servieResult.Result.Page,
            TotalItems = servieResult.Result.TotalItems
        });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<CreateQuizLocationResponse>))]
    public async Task<IActionResult> CreateQuizLocationAsync(
        [FromBody] CreateQuizLocationRequest request, [FromServices] CreateQuizLocationRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new CreateQuizLocationServiceInput
        {
            Site = request.Site,
            UserId = UserId
        };

        var serviceResult = await quizLocationsService.CreateQuizLocationsAsync(serviceInput);
        if (serviceResult.Reason != ErrorReason.None)
        {
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new CreateQuizLocationResponse
        {
            Id = serviceResult.Result.Id
        });
    }

    [HttpPut("{quizId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<UpdateQuizLocationResponse>))]
    public async Task<IActionResult> UpdateQuizLocationAsync(
        [FromRoute] int quizLocationId, [FromBody] UpdateQuizLocationRequest request, [FromServices] UpdateQuizLocationRequestValidator requestValidator)
    {
        request.QuizLocationId = quizLocationId;

        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new UpdateQuizLocationServiceInput
        {
            QuizLocationId = request.QuizLocationId,
            Site = request.Site,
            UserId = UserId
        };

        var serviceResult = await quizLocationsService.UpdateQuizLocationsAsync(serviceInput);
        if (serviceResult.Reason != ErrorReason.None)
        {
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new UpdateQuizLocationResponse
        {
            Id = serviceResult.Result.Id
        });
    }
}