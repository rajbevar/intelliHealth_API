using PatientEngTranscription.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain.DbEntities
{
    public class Problems : Entity<Guid>
    {
        public bool IsProblem { get; set; }
        public string Name { get; set; }
        public DateTime RecordedDate { get; set; }
        public string ExternalId { get; set; }

        public ProblemCategory category { get; set; }
        

    }
}
