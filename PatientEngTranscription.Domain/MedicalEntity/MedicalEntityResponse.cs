using PatientEngTranscription.Domain.MedicalEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain.Models
{
   public  class MedicalEntityResponse
    {

        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public string DetailedErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public List<MedicationEntity> Medications { get; set; }
        public List<AnatomyEntity> AnatomyList { get; set; }
        public List<UnMappedAttribute> MedicationsUnMapped { get; set; }


        public List<MedicalConditionEntity> MedicalConditions { get; set; }
        public List<UnMappedAttribute> MedicalConditionsUnmapped { get; set; }

        public List<MedicationEntity> SuggestedMedications { get; set; }



    }
}
