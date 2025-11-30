namespace QuizBar.Data.Entities;

using QuizBar.Shared;

public class Chapter : IEntityMetaData
{
    // Main
    public string Id { get; set; }
    public string Title { get; set; }
    public string Scope { get; set; }
    public int Priority { get; set; }

    // Foreign keys
    public string QuizId { get; set; }

    // Navigation properties
    public virtual Quiz Quiz { get; set; }
    public virtual ICollection<Question> Questions { get; set; }

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