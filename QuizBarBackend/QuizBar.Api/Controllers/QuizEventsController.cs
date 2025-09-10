using Microsoft.AspNetCore.Mvc;
using QuizBar.Api.Requests.Quizzes;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Quizzes;
using QuizBar.Services;
using QuizBar.Services.Inputs.Quizzes;
using QuizBar.Services.Results;

namespace QuizBar.Api.Controllers;

public class QuizEventsController(
    QuizzesService quizzesService) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<CreateQuizEventResponse>))]
    public async Task<IActionResult> CreateQuizEventAsync(
        [FromRoute] string quizId, [FromBody] CreateQuizEventRequest request, [FromServices] CreateQuizEventRequestValidator requestValidator)
    {
        request.QuizId = quizId;

        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new CreateQuizEventServiceInput
        {
            QuizId = quizId,
            ScheduledOn = request.ScheduledOn,
            QuizLocationId = request.QuizLocationId,
            TeamIds = request.TeamIds,
            UserId = UserId
        };

        var serviceResult = await quizzesService.CreateQuizEventAsync(serviceInput);

        return ControllerResponse(new CreateQuizEventResponse
        {
            Id = serviceResult.Result.Id
        });
    }

    [HttpPut("{quizEventId:required}/teamscores")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<UpdateQuizEventScoresResponse>))]
    public async Task<IActionResult> UpdateQuizEventResultsAsync(
        [FromRoute] string quizEventId, [FromBody] UpdateQuizEventScoresRequest request, [FromServices] UpdateQuizEventResultsRequestValidator requestValidator)
    {
        request.QuizEventId = quizEventId;

        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new UpdateQuizEventScoresServiceInput
        {
            QuizEventId = request.QuizEventId,
            TeamsScores = request.TeamsScores.Select(teamScore => new Services.Inputs.Quizzes.UpdateQuizEventResultsTeamResult
            {
                TeamId = teamScore.TeamId,
                Score = teamScore.Score
            })
        };

        var serviceResult = await quizzesService.UpdateQuizEventScoresAsync(serviceInput);
        if (serviceResult.Reason != ErrorReason.None)
        {
            return ControllerResponse(serviceResult.Reason);
        }

        return ControllerResponse(new UpdateQuizEventScoresResponse
        {
            TeamsScores = serviceResult.Result.TeamsScores.Select(teamScore => new Responses.Quizzes.UpdateQuizEventResultsTeamResult 
            {
                TeamId = teamScore.TeamId,
                Score = teamScore.Score
            })
        });
    }
}