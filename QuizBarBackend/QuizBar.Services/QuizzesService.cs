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

        var quiz = await db.Quizzes
                               .Include(q => q.Chapters)
                                   .ThenInclude(ch => ch.Questions)
                                       .ThenInclude(ch => ch.Answers)
                                           .FirstOrDefaultAsync(q => q.Id == filter.QuizId);
        if (quiz is null)
        {
            serviceResult.Reason = ErrorReason.NotFound;
            return serviceResult;
        }

        serviceResult.Result = new GetQuizServiceResult
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Scope = quiz.Scope,
            Chapters = quiz.Chapters.Select(ch => new GetQuizzesChapter
            {
                Id = ch.Id,
                Title = ch.Title,
                Status = ch.Status,
                Questions = ch.Questions.Select(q => new GetQuizzesChapterQuestion
                {
                    Id = q.Id,
                    Type = q.Type,
                    Text = q.Text,
                    Priority = q.Priority,
                    Status = q.Status,
                    Answers = q.Answers.Select(a => new GetQuizzesChapterQuestionAnswer
                    {
                        Id = a.Id,
                        Text = a.Text,
                        IsCorrect = a.IsCorrect,
                        Priority = a.Priority,
                        Status = a.Status,
                    })
                })
            })
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

        // TODO make the query not take everything, just the things we need
        var quizzes = await quizzesQuery.ToListAsync();

        serviceResult.Result = new GetQuizzesServiceResult
        {
            Quizzes = quizzes.Select(q => new GetQuizzesQuiz
            {
                Id = q.Id,
                Title = q.Title,
                Scope = q.Scope,
                Status = q.Status,
                
            })
        };

        return serviceResult;
    }
    public async Task<ServiceResult<CreateQuizServiceResult>> CreateQuizAsync(CreateQuizServiceInput input)
    {
        var serviceResult = new ServiceResult<CreateQuizServiceResult>();

        var quizExists = await db.Quizzes.FirstOrDefaultAsync(q => q.Title == input.Title && q.CreatedBy == input.UserId);
        if (quizExists is not null)
        {
            serviceResult.Reason = ErrorReason.QuizAlreadyExists;
            return serviceResult;
        }

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
            Title = ApplicationConstants.DefaultChapter,
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
    public async Task<ServiceResult<CreateQuizEventServiceResult>> CreateQuizEventAsync(CreateQuizEventServiceInput input)
    {
        var serviceResult = new ServiceResult<CreateQuizEventServiceResult>();

        var quiz = await db.Quizzes.FirstOrDefaultAsync(q => q.Id == input.QuizId);
        if (quiz is null)
        {
            serviceResult.Reason = ErrorReason.NotFound;
            return serviceResult;
        }

        var quizLocation = await db.QuizLocations.FirstOrDefaultAsync(ql => ql.Id == input.QuizLocationId);
        if (quizLocation is null)
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
            CreatedBy = input.UserId
        };

        var teams = await db.Teams.Where(t => input.TeamIds.Contains(t.Id)).ToListAsync();
        if (teams.Count > 0)
        {
            quizEvent.TeamsQuizEvents = [];

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
        }

        await db.QuizEvents.AddAsync(quizEvent);
        await db.SaveChangesAsync();

        serviceResult.Result = new CreateQuizEventServiceResult
        {
            Id = quizEvent.Id
        };
        
        return serviceResult;
    }
    public async Task<ServiceResult<UpdateQuizEventScoresServiceResult>> UpdateQuizEventScoresAsync(UpdateQuizEventScoresServiceInput input)
    {
        var serviceResult = new ServiceResult<UpdateQuizEventScoresServiceResult>();

        var quizEvent = await db.QuizEvents.FirstOrDefaultAsync(qe => qe.Id == input.QuizEventId);
        if (quizEvent is null)
        {
            serviceResult.Reason = ErrorReason.QuizEventNotFound;
            return serviceResult;
        }

        var teamsQuizEvents = await db.TeamsQuizEvents
            .Include(tqe => tqe.Team)
            .Where(tqe => tqe.QuizEventId == quizEvent.Id)
            .ToListAsync();
        
        var inputContainsCorrectIds = new HashSet<string>(input.TeamsScores.Select(ts => ts.TeamId)).SetEquals(new HashSet<string>(teamsQuizEvents.Select(tqe => tqe.TeamId)));
        if (!inputContainsCorrectIds)
        {
            serviceResult.Reason = ErrorReason.IncompatibleData;
            return serviceResult;
        }

        foreach (var teamsQuizEvent in teamsQuizEvents)
        {
            var teamScore = input.TeamsScores.First(ts => ts.TeamId == teamsQuizEvent.TeamId);
            teamsQuizEvent.Score += teamScore.Score;
        }

        db.TeamsQuizEvents.UpdateRange(teamsQuizEvents);
        await db.SaveChangesAsync();

        serviceResult.Result = new UpdateQuizEventScoresServiceResult
        {
            TeamsScores = teamsQuizEvents.Select(tqe => new Results.Quizzes.UpdateQuizEventResultsTeamResult 
            { 
                TeamId = tqe.TeamId,
                Score = tqe.Score
            })
        };

        return serviceResult;
    }
}