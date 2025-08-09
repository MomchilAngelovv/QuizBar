using Foksty.Data;

namespace Foksty.Services;

public class QuizzesService(
    ApplicationDbContext db)
{
    public async Task<string> CreateQuizAsync()
    {
        return null;
    }

    public string StartQuizAsync(string id)
    {
        return null;
    }
}
