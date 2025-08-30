using QuizBar.Shared;

namespace QuizBar.Data.Entities;

public class QuizLocation : IEntityMetaData
{
    // Main
    public required string Id { get; set; }
    public required string Site { get; set; }

    // Foreign keys

    // Navigation properties
    public virtual ICollection<QuizEvent> QuizEvents { get; set; }

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