using Microsoft.AspNetCore.Mvc;

namespace Foksty.Api.Controllers;

public class QuizzesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<object>> CreateQuizAsync()
    {

    }

    [HttpGet]
    public async Task<ActionResult<object>> GetQuizzesAsync()
    {

    }

    [HttpPost("{id}")]
    public async Task<ActionResult<object>> StartQuizAsync(string id)
    {

    }

    // Create Quiz
    // Start Quiz = create quizREsult and start updagint it 
    // Get quizResults
}