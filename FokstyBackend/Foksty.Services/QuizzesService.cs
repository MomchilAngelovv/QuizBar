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
        var newQuiz = new Quiz
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = title,
            Scope = scope,
            CreatedBy = createdBy,
            Status = statuses.Active
        };

        var newChapter = new Chapter
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = title,
            CreatedBy = createdBy,
            Status = statuses.Active,
            QuizId = newQuiz.Id
        };

        await db.Quizzes.AddAsync(newQuiz);
        await db.Chapters.AddAsync(newChapter);
        await db.SaveChangesAsync();
        return newQuiz.Id;
    }

    public string StartQuizAsync(string id)
    {
        return null;
    }
}
