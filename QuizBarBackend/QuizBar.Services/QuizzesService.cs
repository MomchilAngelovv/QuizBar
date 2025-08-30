using Microsoft.EntityFrameworkCore;
using QuizBar.Data;
using QuizBar.Data.Entities;
using QuizBar.Services.Inputs.Quizzes;
using QuizBar.Services.Results;
using QuizBar.Services.Results.Quizzes;
using QuizBar.Shared;

namespace QuizBar.Services;

public class QuizzesService(
    ApplicationDbContext db)
{
    public async Task<ServiceResult<GetQuizServiceResult>> GetQuizAsync(QuizzesFilter filter)
    {
        var serviceResult = new ServiceResult<GetQuizServiceResult>();

        var quiz = await db.Quizzes.FirstOrDefaultAsync(q => q.Id == filter.QuizId);
        if (quiz is null)
        {
            serviceResult.Reason = ErrorReason.NotFound;
            return serviceResult;
        }

        serviceResult.Result = new GetQuizServiceResult
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Scope = quiz.Scope
        };

        return serviceResult;
    }

    public async Task<ServiceResult<GetQuizzesServiceResult>> GetQuizzesAsync(QuizzesFilter filter = null)
    {
        var serviceResult = new ServiceResult<GetQuizzesServiceResult>();

        var quizzesQuery = db.Quizzes.AsQueryable();

        if (filter is not null)
        {
            if (string.IsNullOrWhiteSpace(filter.UserId))
            {
                quizzesQuery = quizzesQuery.Where(q => q.CreatedBy == filter.UserId);
            }
        }

        var quizzes = await quizzesQuery.ToListAsync();

        serviceResult.Result = new GetQuizzesServiceResult
        {
            Quizzes = quizzes.Select(q => new GetQuizzesQuiz
            {
                Id = q.Id,
                Title = q.Title,
                Scope = q.Scope,
                Status = q.Status
            })
        };

        return serviceResult;
    }

    public async Task<ServiceResult<CreateQuizServiceResult>> CreateQuizAsync(CreateQuizServiceInput input)
    {
        var serviceResult = new ServiceResult<CreateQuizServiceResult>();

        var quiz = new Quiz
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = input.Title,
            Scope = input.Scope,
            Status = Statuses.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = input.UserId
        };

        var chapter = new Chapter
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = input.Title,
            QuizId = quiz.Id,
            Status = Statuses.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = input.UserId
        };

        await db.Quizzes.AddAsync(quiz);
        await db.Chapters.AddAsync(chapter);
        await db.SaveChangesAsync();

        serviceResult.Result = new CreateQuizServiceResult
        {
            Id = quiz.Id,
        };

        return serviceResult;
    }

    public string StartQuizAsync(string id)
    {
        return null;
    }

    public async Task<ServiceResult<CreateQuizEventServiceResult>> CreateQuizEventAsync(CreateQuizEventServiceInput input)
    {
        var serviceResult = new ServiceResult<CreateQuizEventServiceResult>();

        var quiz = await db.Quizzes.FirstOrDefaultAsync(q => q.Id == input.QuizId);
        if (quiz is null)
        {
            serviceResult.Reason = ErrorReason.NotFound;
            return serviceResult;
        }
        
        var quizEvent = new QuizEvent
        {
            Id = Guid.CreateVersion7().ToString(),
            ScheduledOn = input.ScheduledOn,
            QuizLocationId = input.QuizLocationId,
            QuizId = quiz.Id,
            Status = Statuses.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = input.UserId,
            TeamsQuizEvents = []
        };

        var teams = await db.Teams.Where(t => input.TeamIds.Contains(t.Id)).ToListAsync();
        foreach (var team in teams)
        {
            var teamQuizEvent = new TeamQuizEvent
            {
                Id = Guid.CreateVersion7().ToString(),
                Score = 0,
                TeamId = team.Id,
                QuizEventId = quizEvent.Id,
                Status = Statuses.Active,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = input.UserId
            };

            await db.TeamsQuizEvents.AddAsync(teamQuizEvent);
        }

        await db.QuizEvents.AddAsync(quizEvent);
        await db.SaveChangesAsync();

        serviceResult.Result = new CreateQuizEventServiceResult
        {
            Id = quizEvent.Id
        };
        
        return serviceResult;
    }
}