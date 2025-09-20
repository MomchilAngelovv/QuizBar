using Microsoft.EntityFrameworkCore;
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

        var questionExists = await db.Questions.FirstOrDefaultAsync(q => q.Text == input.Text && q.CreatedBy == input.UserId);
        if (questionExists is not null)
        {
            serviceResult.Reason = ErrorReason.QuestionAlreadyExists;
            return serviceResult;
        }

        var chapter = await db.Chapters.FirstOrDefaultAsync(ch => ch.Id == input.ChapterId && ch.CreatedBy == input.UserId);
        if (chapter is null)
        {
            serviceResult.Reason = ErrorReason.ChapterNotFound;
            return serviceResult;
        }

        var question = new Question
        {
            Id = Guid.CreateVersion7().ToString(),
            Type = input.Type,
            Text = input.Text,
            Priority = input.Priority,
            ChapterId = input.ChapterId,
            Status = Statuses.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = input.UserId,
            Answers = []
        };

        foreach (var answer in input.Answers)
        {
            question.Answers.Add(new Answer
            {
                Id = Guid.CreateVersion7().ToString(),
                Text = answer.Text,
                IsCorrect = answer.IsCorrect,
                Priority = answer.Priority,
                QuestionId = question.Id,
                Status = Statuses.Active,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = input.UserId
            });
        }

        if (input.Type == QuestionType.Video || 
            input.Type == QuestionType.Sound)
        {
            question.ResourceUrl = input.ResourceUrl;
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