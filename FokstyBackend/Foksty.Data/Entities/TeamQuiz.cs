using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foksty.Data.Entities
{
    public class TeamQuiz : IEntityMetaData
    {
        public string TeamId { get; set; }
        public string QuizId { get; set; }

        public virtual Team Team { get; set; }
        public virtual Quiz Quiz { get; set; }

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
}
