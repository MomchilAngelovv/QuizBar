namespace QuizBar.Data.Entities;

using Microsoft.AspNetCore.Identity;
using QuizBar.Shared;

public class QuizBarUser : IdentityUser<string>, IEntityMetaData
{
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