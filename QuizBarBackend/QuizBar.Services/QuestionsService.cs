using QuizBar.Data;
using QuizBar.Data.Entities;
using QuizBar.Services.Inputs.Questions;
using QuizBar.Services.Results;
using QuizBar.Services.Results.Questions;
using QuizBar.Shared;

namespace QuizBar.Services;

public class QuestionsService(
     ApplicationDbContext db)
{
    public async Task<ServiceResult<CreateQuestionServiceResult>> CreateQuestionAsync(CreateQuestionServiceInput input)
    {
        var serviceResult = new ServiceResult<CreateQuestionServiceResult>();

        var question = new Question
        {
            Id = Guid.CreateVersion7().ToString(),
            Type = input.Type,
            Text = input.Text,
            Priority = input.Priority,
            ChapterId = input.ChapterId,
            Status = Status.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = input.UserId,
            PossibleAnswers = []
        };

        foreach (var pa in input.PossibleAnswers)
        {
            var possibleAnswer = new PossibleAnswer
            {
                Id = Guid.CreateVersion7().ToString(),
                Text = pa.Text,
                IsCorrect = pa.IsCorrect,
                Priority = pa.Priority,
                QuestionId = question.Id,
                Status = Status.Active,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = input.UserId
            };

            question.PossibleAnswers.Add(possibleAnswer);
        }

        await db.Questions.AddAsync(question);
        await db.SaveChangesAsync();

        serviceResult.Result = new CreateQuestionServiceResult
        {
            Id = question.Id,
        };

        return serviceResult;
    }
}