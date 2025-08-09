using Foksty.Data;
using Foksty.Data.Entities;
using Foksty.Shared;

namespace Foksty.Services;

public class QuizzesService(
    ApplicationDbContext db,
    Statuses statuses)
{
    public async Task<string> CreateQuizAsync(string createdBy, string title, string scope = null)
    {
        var quiz = new Quiz
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = title,
            Scope = scope,
            Status = statuses.Active,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow,
        };

        var chapter = new Chapter
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = title,
            QuizId = quiz.Id,
            Status = statuses.Active,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow,
        };

        await db.Quizzes.AddAsync(quiz);
        await db.Chapters.AddAsync(chapter);
        await db.SaveChangesAsync();
        return quiz.Id;
    }

    public string StartQuizAsync(string id)
    {
        return null;
    }
}