namespace Foksty.Data.Entities;

public class TeamResultQuizResult : IEntityMetaData
{
    // Main
   
    // Foreign keys
    public string TeamResultId { get; set; }
    public string QuizResultId { get; set; }

    // Navigation properties
    public virtual TeamResult TeamResult { get; set; }
    public virtual QuizResult QuizResult { get; set; }

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