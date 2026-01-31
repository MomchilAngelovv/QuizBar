namespace QuizBar.Services;

using Microsoft.EntityFrameworkCore;
using QuizBar.Data;
using QuizBar.Data.Entities;
using QuizBar.Services.ServiceModels.Inputs.Quizzes;
using QuizBar.Services.ServiceModels.Results;
using QuizBar.Services.ServiceModels.Results.Quizzes;
using QuizBar.Shared;

public class QuizzesService(ApplicationDbContext db)
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
            Chapters = quiz.Chapters.Select(chapter => new GetQuizzesChapter
            {
                Id = chapter.Id,
                Title = chapter.Title,
                Status = chapter.Status,
                Questions = chapter.Questions.Select(question => new GetQuizzesChapterQuestion
                {
                    Id = question.Id,
                    Type = question.Type,
                    Title = question.Title,
                    Resource = question.Resource,
                    Priority = question.Priority,
                    Status = question.Status,
                    Answers = question.Answers.Select(answer => new GetQuizzesChapterQuestionAnswer
                    {
                        Id = answer.Id,
                        Title = answer.Title,
                        IsCorrect = answer.IsCorrect,
                        Priority = answer.Priority,
                        Status = answer.Status,
                    })
                })
            })
        };

        return serviceResult;
    }
    public async Task<ServiceResult<GetQuizzesServiceResult>> GetQuizzesAsync(GetQuizzesServiceInput input)
    {
        var serviceResult = new ServiceResult<GetQuizzesServiceResult>();

        var quizzesQuery = db.Quizzes.AsQueryable();

        if (input.Filter is not null)
        {
            if (!string.IsNullOrWhiteSpace(input.UserId))
            {
                quizzesQuery = quizzesQuery
                    .Where(team => team.CreatedBy == input.UserId);
            }
        }

        var quizzes = await quizzesQuery
            .OrderBy(q => q.Title)
            .Skip((input.Page - 1) * 10)
            .Take(10)
            .ToListAsync();

        var quizzesTotalItems = await db.Quizzes
            .CountAsync();

        serviceResult.Result = new GetQuizzesServiceResult
        {
            Quizzes = quizzes.Select(q => new GetQuizzesQuiz
            {
                Id = q.Id,
                Title = q.Title,
                Scope = q.Scope,
                Status = q.Status,
                
            }),
            Page = input.Page,
            TotalItems = quizzesTotalItems
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
}