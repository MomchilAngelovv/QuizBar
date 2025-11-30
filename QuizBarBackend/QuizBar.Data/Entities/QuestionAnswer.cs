namespace QuizBar.Data.Entities;

using QuizBar.Shared;

public class QuestionAnswer : IEntityMetaData
{
    // Main
    public string Id { get; set; }

    // Foreign keys
    public string ScoreId { get; set; }
    public string QuestionId { get; set; }
    public string AnswerId { get; set; }

    // Navigation properties
    public virtual Score Score { get; set; }
    public virtual Question Question { get; set; }
    public virtual Answer Answer { get; set; }

    // IEntityMetaData
    public Statuses Status { get; set; }
    public string? Description { get; set; }

    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }

    public DateTime? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
}