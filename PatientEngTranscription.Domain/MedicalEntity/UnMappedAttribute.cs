﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain.MedicalEntity
{
  public   class UnMappedAttribute
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }

        public float  Score { get; set; }
        public int BeginOffset { get; set; }
        public int EndOffset { get; set; }
        public string Text { get; set; }
        public List<TraitEntity> Traits { get; set; }

    }
}
