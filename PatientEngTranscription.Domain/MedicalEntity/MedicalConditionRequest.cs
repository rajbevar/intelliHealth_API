using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain.MedicalEntity
{
   public  class MedicalConditionRequest
    {

        public List<MedicalConditionEntity> MedicalConditions { get; set; }

        public List<AnatomyEntity> Anatomies { get; set; }

    }
}
