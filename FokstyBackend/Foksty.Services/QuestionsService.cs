using Foksty.Data;
using Foksty.Data.Entities;
using Foksty.Services.Inputs.Questions;
using Foksty.Services.Results;
using Foksty.Services.Results.Questions;
using Foksty.Shared;
using Npgsql.Internal;

namespace Foksty.Services;

public class QuestionsService(
     ApplicationDbContext db,
     Statuses statuses)
{
    public async Task<ServiceResult<CreateQuestionServiceResult>> CreateQuestionAsync(CreateQuestionServiceInput input)
    {
        var question = new Question
        {
            Id = Guid.CreateVersion7().ToString(),
            Type = input.Type,
            Text = input.Text,
            ShowOrder = input.ShowOrder,
            ChapterId = input.ChapterId,
            Status = statuses.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = input.CreatedBy
        };

        foreach (var possibleAnswer in possibleAnswers)
        {
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