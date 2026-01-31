namespace QuizBar.Data.Entities;

using QuizBar.Shared;

public class Quiz : IEntityMetaData
{
    // Main
    public string Id { get; set; }
    public string Title { get; set; }
    public string Scope { get; set; }

    // Foreign keys
    public string SiteId { get; set; }

    // Navigation properties
    public virtual Site Site { get; set; }
    public virtual ICollection<Chapter> Chapters { get; set; }

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