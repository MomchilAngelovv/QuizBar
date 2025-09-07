using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuizBar.Api.Requests.QuizLocations;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.QuizLocations;
using QuizBar.Api.Responses.Quizzes;
using QuizBar.Services;
using QuizBar.Services.Inputs.QuizLocations;

namespace QuizBar.Api.Controllers;

public class QuizLocationsController(
    QuizLocationsService quizLocationsService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetQuizzesResponse>))]
    public async Task<IActionResult> GetQuizLocationsAsync(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GetQuizLocationsRequest? request, [FromServices] GetQuizLocationsRequestValidator requestValidator)
    {
        request ??= new GetQuizLocationsRequest();

        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var filter = new QuizLocationsFilter
        {
            UserId = UserId
        };

        var servieResult = await quizLocationsService.GetQuizLocationsAsync(filter);

        return ControllerResponse(new GetQuizLocationsResponse
        {
            QuizLocations = servieResult.Result.QuizLocations.Select(ql => new GetQuizLocationsLocation
            {
                Id = ql.Id,
                Site = ql.Site,
                Status = ql.Status
            }),
            Page = 1,
            TotalItems = 50
        });
    }
}