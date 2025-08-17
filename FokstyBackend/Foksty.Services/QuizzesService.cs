using Foksty.Data;
using Foksty.Data.Entities;
using Foksty.Shared;
using Microsoft.EntityFrameworkCore;

namespace Foksty.Services;

public class QuizzesService(
    ApplicationDbContext db,
    Statuses statuses)
{
    public async Task<IEnumerable<Quiz>> GetQuizzesAsync()
    {
        var quizzes = await db.Quizzes.ToListAsync();
        return quizzes;
    }

    public async Task<Quiz> CreateQuizAsync(string createdBy, string title, string scope = null)
    {
        var quiz = new Quiz
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = title,
            Scope = scope,
            Status = statuses.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = createdBy
        };

        var chapter = new Chapter
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = title,
            ShowOrder = 1,
            QuizId = quiz.Id,
            Status = statuses.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = createdBy
        };

        await db.Quizzes.AddAsync(quiz);
        await db.Chapters.AddAsync(chapter);
        await db.SaveChangesAsync();
        return quiz;
    }

    public string StartQuizAsync(string id)
    {
        return null;
    }
}