using QuizBar.Shared;

namespace QuizBar.Data.Entities;

public class QuizEvent : IEntityMetaData
{
    // Main
    public required string Id { get; set; }
    public required DateTime ScheduledOn { get; set; }

    // Foreign keys
    public required string QuizId { get; set; }

    // Navigation properties
    public virtual Quiz Quiz { get; set; }
    public virtual IEnumerable<TeamQuizEvent> TeamScores { get; set; }

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
