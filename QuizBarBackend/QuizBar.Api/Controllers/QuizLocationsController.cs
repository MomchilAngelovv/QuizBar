using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuizBar.Api.Requests.QuizLocations;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.QuizLocations;
using QuizBar.Api.Responses.Quizzes;
using QuizBar.Services;
using QuizBar.Services.Inputs.QuizLocations;
using QuizBar.Services.Results;

namespace QuizBar.Api.Controllers;

public class QuizLocationsController(
    SitesService sitesService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetQuizzesResponse>))]
    public async Task<IActionResult> GetQuizLocationsAsync(
        [FromQuery] int? page, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GetQuizLocationsRequest? request, [FromServices] GetQuizLocationsRequestValidator requestValidator)
    {
        request ??= new GetQuizLocationsRequest();
        request.Page = page ?? 1;
        
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new GetSitesServiceInput
        {
            Page = request.Page,
            Filter = new QuizLocationsFilter
            {
                UserId = UserId
            }
        };

        var servieResult = await sitesService.GetSitesAsync(serviceInput);

        return ControllerResponse(new GetQuizLocationsResponse
        {
            QuizLocations = servieResult.Result.QuizLocations.Select(quizLocation => new GetQuizLocationsLocation
            {
                Id = quizLocation.Id,
                Title = quizLocation.Title,
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

        var serviceInput = new CreateSiteServiceInput
        {
            Title = request.Title,
            UserId = UserId
        };

        var serviceResult = await sitesService.CreateSiteAsync(serviceInput);
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

        var serviceInput = new UpdateSiteServiceInput
        {
            QuizLocationId = request.QuizLocationId,
            Title = request.Title,
            UserId = UserId
        };

        var serviceResult = await sitesService.UpdateSiteAsync(serviceInput);
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