using Foksty.Api.Requests.Quizzes;
using Foksty.Services;
using Microsoft.AspNetCore.Mvc;

namespace Foksty.Api.Controllers;

public class QuizzesController(
    QuizzesService quizzesService) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<object>> CreateQuizAsync(CreateQuizRequest request)
    {
        // AI Create Quiz
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
}