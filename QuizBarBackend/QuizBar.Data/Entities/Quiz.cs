namespace QuizBar.Data.Entities;

using QuizBar.Shared;

public class Quiz : IEntityMetaData
{
    // Main
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string Scope { get; set; }

    // Foreign keys

    // Navigation properties
    public virtual ICollection<Chapter> Chapters { get; set; }

    // IEntityMetaData
    public required Statuses Status { get; set; }
    public string? Description { get; set; }

    public required DateTime CreatedOn { get; set; }
    public required string CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }

    public DateTime? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
}