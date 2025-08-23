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
    public async Task<ServiceResult<GetQuizServiceResult>> GetQuizAsync(string id)
    {
        var quiz = await db.Quizzes.FirstOrDefaultAsync(q => q.Id == id);

        var serviceResult = new ServiceResult<GetQuizServiceResult>
        {
            Success = true
        };

        if (quiz is not null)
        {
            serviceResult.Data = new GetQuizServiceResult
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Scope = quiz.Scope
            };
        }

        return serviceResult;
    }

    public async Task<IEnumerable<Quiz>> GetQuizzesAsync()
    {
        var quizzes = await db.Quizzes.ToListAsync();
        return quizzes;
    }

    public async Task<ServiceResult<CreateQuizServiceResult>> CreateQuizAsync(CreateQuizServiceInput input)
    {
        var quiz = new Quiz
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = input.Title,
            Scope = input.Scope,
            Status = Status.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = input.CreatedBy
        };

        var chapter = new Chapter
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = input.Title,
            QuizId = quiz.Id,
            Status = Status.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = input.CreatedBy
        };

        await db.Quizzes.AddAsync(quiz);
        await db.Chapters.AddAsync(chapter);
        await db.SaveChangesAsync();

        var serviceResult = new ServiceResult<CreateQuizServiceResult>
        {
            Success = true,
            Data = new CreateQuizServiceResult
            {
                Id = quiz.Id,
            }
        };

        return serviceResult;
    }

    public string StartQuizAsync(string id)
    {
        return null;
    }

    public async Task<ServiceResult<CreateQuizEventServiceResult>> CreateQuizEventAsync(CreateQuizEventServiceInput input)
    {
        var quiz = await db.Quizzes.FirstOrDefaultAsync(q => q.Id == input.QuizId);
        if (quiz is null)
        {
            return new ServiceResult<CreateQuizEventServiceResult>
            {
                Success = false,
                Message = "Quiz not found"
            };
        }

        var teams = await db.Teams.Where(t => input.TeamIds.Contains(t.Id)).ToListAsync();
        foreach (var team in teams)
        {
            var teamQuizResult = new TeamResult
            {
                Id = Guid.CreateVersion7().ToString(),
                Score = 0,
                TeamId = team.Id,
                QuizId = quiz.Id,
                Status = Status.Active,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = input.CreatedBy
            };

            await db.TeamQuizResults.AddAsync(teamQuizResult);
        }

        await db.SaveChangesAsync();

        var serviceResult = new ServiceResult<CreateQuizEventServiceResult>
        {
            Success = true,
            Data = new CreateQuizEventServiceResult
            {
                TeamIds = db.TeamQuizResults.Local.Select(tqr => tqr.Id)
            }
        };

        return serviceResult;
    }
}