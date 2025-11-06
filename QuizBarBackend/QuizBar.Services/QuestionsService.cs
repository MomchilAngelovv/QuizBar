namespace QuizBar.Services;

using Microsoft.EntityFrameworkCore;
using QuizBar.Data;
using QuizBar.Data.Entities;
using QuizBar.Shared;
using QuizBar.Shared.ServiceModels.Inputs.Questions;
using QuizBar.Shared.ServiceModels.Results;
using QuizBar.Shared.ServiceModels.Results.Questions;


public class QuestionsService(
     ApplicationDbContext db)
{
    public async Task<ServiceResult<CreateQuestionServiceResult>> CreateQuestionAsync(CreateQuestionServiceInput input)
    {
        var serviceResult = new ServiceResult<CreateQuestionServiceResult>();

        var questionExists = await db.Questions
            .FirstOrDefaultAsync(q => q.Title == input.Title && q.CreatedBy == input.UserId);
        if (questionExists is not null)
        {
            serviceResult.Reason = ErrorReason.QuestionAlreadyExists;
            return serviceResult;
        }

        var chapter = await db.Chapters
            .FirstOrDefaultAsync(ch => ch.Id == input.ChapterId && ch.CreatedBy == input.UserId);
        if (chapter is null)
        {
            serviceResult.Reason = ErrorReason.ChapterNotFound;
            return serviceResult;
        }

        var question = new Question
        {
            Id = Guid.CreateVersion7().ToString(),
            Type = input.Type,
            Title = input.Title,
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
                Title = answer.Title,
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
            question.Resource = input.Resource;
        }

        await db.Questions.AddAsync(question);
        await db.SaveChangesAsync();

        serviceResult.Result = new CreateQuestionServiceResult
        {
            Id = question.Id,
        };

        return serviceResult;
    }
    public async Task<ServiceResult<UpdateQuestionServiceResult>> UpdateQuestionAsync(UpdateQuestionServiceInput input)
    {
        var serviceResult = new ServiceResult<UpdateQuestionServiceResult>();

        var question = await db.Questions
            .FirstOrDefaultAsync(question => question.Id == input.QuestionId);
        if (question is not null)
        {
            serviceResult.Reason = ErrorReason.QuestionNotFound;
            return serviceResult;
        }

        if (input.ChapterId is not null)
        {
            var chapter = await db.Chapters
                .FirstOrDefaultAsync(ch => ch.Id == input.ChapterId && ch.CreatedBy == input.UserId);
            if (chapter is null)
            {
                serviceResult.Reason = ErrorReason.ChapterNotFound;
                return serviceResult;
            }
        }

        if (!string.IsNullOrWhiteSpace(input.Title))
        {
            question.Title = input.Title;
        }

        if (!string.IsNullOrWhiteSpace(input.Resource))
        {
            question.Resource = input.Resource;
        }

        if (input.Type != QuestionType.None)
        {
            question.Type = input.Type;
        }

        if (input.Priority is not null)
        {
            question.Priority = input.Priority.Value;
        }

        if (input.Answers is not null && input.Answers.Any())
        {
            foreach (var answer in input.Answers)
            {
                question.Answers.Add(new Answer
                {
                    Id = Guid.CreateVersion7().ToString(),
                    Title = answer.Title,
                    IsCorrect = answer.IsCorrect,
                    Priority = answer.Priority,
                    QuestionId = question.Id,
                    Status = Statuses.Active,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = input.UserId
                });
            }
        }

        db.Questions.Update(question);
        await db.SaveChangesAsync();

        serviceResult.Result = new UpdateQuestionServiceResult
        {
             Test = "TestDontForgetThis" 
        };

        return serviceResult;
    }
}