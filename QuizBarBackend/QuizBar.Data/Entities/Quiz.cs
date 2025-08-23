using QuizBar.Shared;

namespace QuizBar.Data.Entities;

public class Quiz : IEntityMetaData
{
    // Main
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string Scope { get; set; }

    // Navigation properties
    public virtual ICollection<Chapter> Chapters { get; set; }
    public virtual ICollection<QuizEvent> Events { get; set; }

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