using PatientEngTranscription.Domain.MedicalEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain
{
    public class MedicationCreateRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public String Frenquency { get; set; }
        public string Doage { get; set; }
        public string Strength { get; set; }

        public int DurationInNumber { get; set; }
        public int FrenquencyInDay { get; set; }

    }

    public class MedCreateRequest
    {
        public string ExternalId { get; set; }
        public List<MedicationEntity> medicationEntity { get; set; }

        public MedCreateRequest()
        {
            medicationEntity = new List<MedicationEntity>();
        }
    }
}
