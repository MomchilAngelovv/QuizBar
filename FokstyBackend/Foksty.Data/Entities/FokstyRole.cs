using Microsoft.AspNetCore.Identity;

namespace Foksty.Data.Entities
{
    public class FokstyRole : IdentityRole<string>, IEntityMetaData
    {
        public FokstyRole()
        {
            Id = Guid.CreateVersion7().ToString();
            CreatedOn = DateTime.UtcNow;
        }

        public required string Status { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
    }
}