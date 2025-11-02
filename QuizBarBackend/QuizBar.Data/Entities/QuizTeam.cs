namespace QuizBar.Data.Entities;

using QuizBar.Shared;

public class QuizTeam : IEntityMetaData
{
    // Main
    public required string Id { get; set; }
    public required double Score { get; set; }

    // Foreign keys
    public required string QuizId { get; set; }
    public required string TeamId { get; set; }

    // Navigation properties
    public virtual Quiz Quiz { get; set; }
    public virtual Team Team { get; set; }

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