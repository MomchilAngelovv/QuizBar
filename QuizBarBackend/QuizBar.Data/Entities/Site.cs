namespace QuizBar.Data.Entities;

using QuizBar.Shared;

public class Site : IEntityMetaData
{
    // Main
    public required string Id { get; set; }
    public required string Title { get; set; }

    // Foreign keys

    // Navigation properties
    public virtual ICollection<Quiz> Quizzes { get; set; }

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