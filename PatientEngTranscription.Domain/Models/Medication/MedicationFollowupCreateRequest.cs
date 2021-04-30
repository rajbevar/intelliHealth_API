using System;
using System.ComponentModel.DataAnnotations;

namespace PatientEngTranscription.Domain.Models
{
    public class MedicationFollowupCreateRequest
    {
        [Required]
        public Guid MedicationId { get; set; }

        public string MedicationName { get; set; }

        public DateTime TakenDate { get; set; }
        public string TakenTime { get; set; }
        public MedicationFollowupStatus StatusType { get; set; }


    }


}
