using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain.Models
{
   public  class MedicationFollowupDto
    {

        public DateTime TakenDate { get; set; }
        public string TakenTime { get; set; }

        public int Status { get; set; }

        public Guid MedicationId { get; set; }


    }
}
