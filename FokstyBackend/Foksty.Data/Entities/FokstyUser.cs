using Microsoft.AspNetCore.Identity;

namespace Foksty.Data.Entities;

public class FokstyUser : IdentityUser<string>, IEntityMetaData
{
    public required string Status { get; set; }
    public string? Description { get; set; }

    public required DateTime CreatedOn { get; set; }
    public required string CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }

    public DateTime? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
}