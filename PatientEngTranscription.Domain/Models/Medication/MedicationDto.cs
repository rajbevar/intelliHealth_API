using PatientEngTranscription.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain
{
    public class MedicationDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public String Frenquency { get; set; }
        public string Doage { get; set; }
        public string Strength { get; set; }

        public int Duration { get; set; }

        public int FrenquencyInDay { get; set; }

        public List<string> FrequencyTimings { get; set; }
        public DateTime? EffectiveDate { get; set; }

        public List<MedicationFollowupDto> MedicationFollowUps { get; set; }

    }
}
