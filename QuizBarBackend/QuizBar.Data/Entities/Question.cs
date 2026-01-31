namespace QuizBar.Data.Entities;

using QuizBar.Shared;

public class Question : IEntityMetaData
{
    // Main
    public string Id { get; set; }
    public QuestionType Type { get; set; }
    public string Title { get; set; }
	public string Resource { get; set; }
	public int Priority { get; set; }
    public double Score { get; set; }

    // Foreign keys
    public required string ChapterId { get; set; }

    // Navigation properties
    public virtual Chapter Chapter { get; set; }
    public virtual ICollection<Answer> Answers { get; set; }
    public virtual ICollection<TeamQuestionAnswer> TeamsAnswers { get; set; }

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