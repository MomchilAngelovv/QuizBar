using Foksty.Data;
using Foksty.Data.Entities;
using Foksty.Services.Inputs.Questions;
using Foksty.Services.Results;
using Foksty.Services.Results.Questions;
using Foksty.Shared;

namespace Foksty.Services;

public class QuestionsService(
     ApplicationDbContext db)
{
    public async Task<ServiceResult<CreateQuestionServiceResult>> CreateQuestionAsync(CreateQuestionServiceInput input)
    {
        var question = new Question
        {
            Id = Guid.CreateVersion7().ToString(),
            Type = input.Type,
            Text = input.Text,
            Priority = input.Priority,
            ChapterId = input.ChapterId,
            Status = Status.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = input.CreatedBy
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
                CreatedBy = input.CreatedBy
            };

            question.PossibleAnswers.Add(possibleAnswer);
        }

        await db.Questions.AddAsync(question);
        await db.SaveChangesAsync();

        var serviceResult = new ServiceResult<CreateQuestionServiceResult>
        {
            Success = true,
            Data = new CreateQuestionServiceResult
            {
                Id = question.Id,
            }
        };

        return serviceResult;
    }
}