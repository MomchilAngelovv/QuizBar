namespace QuizBar.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using QuizBar.Api.Requests.Sites;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Quizzes;
using QuizBar.Api.Responses.Sites;
using QuizBar.Services;
using QuizBar.Shared.ServiceModels.Inputs.QuizLocations;
using QuizBar.Shared.ServiceModels.Results;

public class SitesController(
    SitesService sitesService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetSitesResponse>))]
    public async Task<IActionResult> GetSitesAsync(int page = 1)
    {
        var request = new GetSitesRequest { Page = page };
        
        var validationResult = await request.ValidateAsync();
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new GetSitesServiceInput
        {
            Page = request.Page,
            UserId = UserId,
        };

        var servieResult = await sitesService.GetSitesAsync(serviceInput);

        return ControllerResponse(new GetSitesResponse
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<CreateSiteResponse>))]
    public async Task<IActionResult> CreateQuizLocationAsync(CreateSiteRequest request)
    {
        var validationResult = await request.ValidateAsync();
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

        return ControllerResponse(new CreateSiteResponse
        {
            Id = serviceResult.Result.Id
        });
    }

    [HttpPut("{siteId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<UpdateSiteResponse>))]
    public async Task<IActionResult> UpdateQuizLocationAsync(int siteId, UpdateSiteRequest request)
    {
        request.SiteId = siteId;

        var validationResult = await request.ValidateAsync();
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new UpdateSiteServiceInput
        {
            SiteId = request.SiteId,
            Title = request.Title,
            UserId = UserId
        };

        var serviceResult = await sitesService.UpdateSiteAsync(serviceInput);
        if (serviceResult.Reason != ErrorReason.None)
        {
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new UpdateSiteResponse
        {
            Id = serviceResult.Result.Id
        });
    }
}