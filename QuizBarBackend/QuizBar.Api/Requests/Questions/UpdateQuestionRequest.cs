using FluentValidation;
using QuizBar.Shared;

namespace QuizBar.Api.Requests.Questions;

public class UpdateQuestionRequest 
{
    public string? QuestionId { get; set; }
    public string? Text { get; set; }
    public string? ResourceUrl { get; set; }
    public QuestionType? Type { get; set; }
    public string? ChapterId { get; set; }
    public int? Priority { get; set; }
    public IEnumerable<CreateQuestionAnswer>? Answers { get; set; }
}

public class UpdateQuestionRequestValidator : AbstractValidator<UpdateQuestionRequest>
{
    public UpdateQuestionRequestValidator()
    {
        When(question => question.Type != null, () =>
        {
            RuleFor(question => question.Answers).NotEmpty();

            RuleForEach(question => question.Answers).ChildRules(pa =>
            {
                pa.RuleFor(possibleAnswer => possibleAnswer.Text).NotEmpty();
            });

            When(question => question.Type == QuestionType.SingleChoice, () =>
            {
                RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.IsCorrect == true)).Equal(1);
            });

            When(question => question.Type == QuestionType.MultiChoice, () =>
            {
                RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.IsCorrect == true)).GreaterThan(1);
            });

            When(question => question.Type == QuestionType.TrueFalse, () =>
            {
                RuleFor(question => question.Answers.Count()).Equal(2);
                RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.Text == "True")).Equal(1);
                RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.Text == "False")).Equal(1);
                RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.IsCorrect == true)).Equal(1);
                RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.IsCorrect == false)).Equal(1);
            });

            When(question => question.Type == QuestionType.FreeText, () =>
            {
                RuleFor(question => question.Answers.Count()).Equal(1);
                RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.IsCorrect == true)).Equal(1);
            });

            When(question => question.Type == QuestionType.Video || question.Type == QuestionType.Sound, () =>
            {
                RuleFor(question => question.Answers.Count()).Equal(1);
                RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.IsCorrect == true)).Equal(1);
                RuleFor(question => question.ResourceUrl).NotEmpty();
            });
        });
    }
}