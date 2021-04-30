using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain
{
    public class Medication : Entity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public String Frenquency { get; set; }
        public string Doage { get; set; }
        public string Strength { get; set; }
        public string ExternalId { get; set; }

        public int Duration { get; set; }

        public int FrenquencyInDay { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public virtual ICollection<MedicationFollowUp> MedicationFollowUps { get; set; }


    }
}
