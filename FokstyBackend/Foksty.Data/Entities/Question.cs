namespace Foksty.Data.Entities;

public class Question : IEntityMetaData
{
    // Main
    public string Id { get; set; }
    public string Type { get; set; }
    public string Text { get; set; }
    public int ShowOrder { get; set; }

    // Foreign keys
    public string ChapterId { get; set; }

    // Navigation properties
    public virtual Chapter Chapter { get; set; }
    public virtual ICollection<PossibleAnswer> PossibleAnswers { get; set; }

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