using Foksty.Data;
using Foksty.Data.Entities;
using Foksty.Services.Inputs.Questions;
using Foksty.Services.Results;
using Foksty.Services.Results.Questions;
using Foksty.Shared;

namespace Foksty.Services;

public class QuestionsService(
     ApplicationDbContext db,
     Status statuses)
{
    public async Task<ServiceResult<CreateQuestionServiceResult>> CreateQuestionAsync(CreateQuestionServiceInput input)
    {
        var question = new Question
        {
            Id = Guid.CreateVersion7().ToString(),
            Type = input.Type,
            Text = input.Text,
            ShowPriority = input.ShowOrder,
            ChapterId = input.ChapterId,
            Status = statuses.Active,
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
                ShowOrder = pa.ShowOrder,
                QuestionId = question.Id,
                Status = statuses.Active,
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