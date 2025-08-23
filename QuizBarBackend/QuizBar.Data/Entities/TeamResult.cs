using QuizBar.Shared;

namespace QuizBar.Data.Entities;

public class TeamResult : IEntityMetaData
{
    // Main
    public required string Id { get; set; }
    public required double Score { get; set; }

    // Foreign keys
    public required string TeamId { get; set; }
    public required string QuizEventId { get; set; }

    // Navigation properties
    public virtual Team Team { get; set; }
    public virtual QuizEvent QuizEvent { get; set; }

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