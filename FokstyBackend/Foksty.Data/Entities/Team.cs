namespace Foksty.Data.Entities;

public class Team : IEntityMetaData
{
    // Main
    public string Id { get; set; }
    public string Title { get; set; }

    // Navigation properties
    public virtual ICollection<TeamResult> TeamResults { get; set; }

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
