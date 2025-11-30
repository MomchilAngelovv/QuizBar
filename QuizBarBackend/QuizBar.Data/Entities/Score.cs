namespace QuizBar.Data.Entities;

using QuizBar.Shared;

public class Score : IEntityMetaData
{
    // Main
    public string Id { get; set; }

    // Foreign keys
    public string QuizId { get; set; }
    public string TeamId { get; set; }

    // Navigation properties
    public virtual Team Team { get; set; }
    public virtual Quiz Quiz { get; set; }
    public virtual ICollection<QuestionAnswer> Answers { get; set; }

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