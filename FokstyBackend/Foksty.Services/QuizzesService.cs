using Foksty.Data;
using Foksty.Data.Entities;
using Foksty.Services.Inputs.Quizzes;
using Foksty.Services.Results;
using Foksty.Services.Results.Quizzes;
using Foksty.Shared;
using Microsoft.EntityFrameworkCore;

namespace Foksty.Services;

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
}