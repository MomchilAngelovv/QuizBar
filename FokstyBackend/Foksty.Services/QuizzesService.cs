using Foksty.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Foksty.Services;

public class QuizzesService(
    ApplicationDbContext db)
{
    public string CreateQuizAsync()
    {
        return null;
    }

    public string StartQuizAsync(string id)
    {
        return null;
    }
}
