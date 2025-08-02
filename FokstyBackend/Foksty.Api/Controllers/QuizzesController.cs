using Microsoft.AspNetCore.Mvc;

namespace Foksty.Api.Controllers;

public class QuizzesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<object>> CreateQuizAsync()
    {
        return null;
    }

    [HttpGet]
    public async Task<ActionResult<object>> GetQuizzesAsync()
    {
        return null;
    }

    [HttpPost("{id}")]
    public async Task<ActionResult<object>> StartQuizAsync(string id)
    {
        return null;
    }

    // Create Quiz
    // Start Quiz = create quizREsult and start updagint it 
    // Get quizResults
}