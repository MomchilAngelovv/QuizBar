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
            CreatedBy = input.UserId
        };

        if (input.Type == QuestionType.SingleChoice || input.Type == QuestionType.MultiChoice)
        {
            question.Answers = [];

            foreach (var pa in input.PossibleAnswers)
            {
                var possibleAnswer = new Answer
                {
                    Id = Guid.CreateVersion7().ToString(),
                    Text = pa.Text,
                    IsCorrect = pa.IsCorrect,
                    Priority = pa.Priority,
                    QuestionId = question.Id,
                    Status = Statuses.Active,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = input.UserId
                };

                question.Answers.Add(possibleAnswer);
            }
        }
        if (input.Type == QuestionType.TrueFalse)
        {
            question.Answers = [];

            var truePossibleAnswer = new Answer
            {
                Id = Guid.CreateVersion7().ToString(),
                Text = "True",
                IsCorrect = input.IsTrue.Value == true,
                Priority = 1,
                QuestionId = question.Id,
                Status = Statuses.Active,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = input.UserId
            };

            var falsePossibleAnswer = new Answer
            {
                Id = Guid.CreateVersion7().ToString(),
                Text = "False",
                IsCorrect = input.IsTrue.Value == false,
                Priority = 2,
                QuestionId = question.Id,
                Status = Statuses.Active,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = input.UserId
            };

            question.Answers.Add(truePossibleAnswer);
            question.Answers.Add(falsePossibleAnswer);
        }
        if (input.Type == QuestionType.FreeText)
        {
            question.Answers = [];

            var correctAnswer = new Answer
            {
                Id = Guid.CreateVersion7().ToString(),
                Text = input.PossibleAnswers.First().Text,
                IsCorrect = true,
                Priority = 1,
                QuestionId = question.Id,
                Status = Statuses.Active,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = input.UserId
            };

            question.Answers.Add(correctAnswer);
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