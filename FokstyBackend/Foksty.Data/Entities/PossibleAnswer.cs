namespace Foksty.Data.Entities;

public class PossibleAnswer : IEntityMetaData
{
    // Main
    public string Id { get; set; }
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
    public int ShowOrder { get; set; }

    // Foreign keys
    public string QuestionId { get; set; }

    // Navigation properties
    public virtual Question Question { get; set; }

    // IEntityMetaData
    public required string Status { get; set; }
    public string? Description { get; set; }

    public DateTime CreatedOn { get; set; }
    public required string CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }

    public DateTime? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
}