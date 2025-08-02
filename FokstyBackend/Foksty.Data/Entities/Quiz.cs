namespace Foksty.Data.Entities;

public class Quiz : IEntityMetaData
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Sector { get; set; }

    public virtual ICollection<Score> Scores { get; set; }
    public virtual ICollection<TeamQuiz> TeamsQuizzes { get; set; }

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