using QuizBar.Shared;

namespace QuizBar.Data.Entities;

public class Question : IEntityMetaData
{
    // Main
    public required string Id { get; set; }
    public required QuestionType Type { get; set; }
    public required string Text { get; set; }
    public int Priority { get; set; }

    // Foreign keys
    public required string ChapterId { get; set; }

    // Navigation properties
    public virtual Chapter Chapter { get; set; }
    public virtual ICollection<PossibleAnswer> PossibleAnswers { get; set; }

    // IEntityMetaData
    public required Status Status { get; set; }
    public string? Description { get; set; }

    public required DateTime CreatedOn { get; set; }
    public required string CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }

    public DateTime? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
}