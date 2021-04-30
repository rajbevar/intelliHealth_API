using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain
{
   public  class MedicationFollowUp : Entity<Guid>
    {

    

        public DateTime TakenDate { get; set; }
        public string TakenTime { get; set; }

        public int Status { get; set; }

        public Guid MedicationId { get; set; }

        public Medication Medication { get; set; }

    }
}
