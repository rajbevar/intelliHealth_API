using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain.Models
{
  public   class DrugSuggestionInputModel
    {
        public int? Age { get; set; }
        public int? Weight { get; set; }
        public bool Pain { get; set; }
        public bool Fever { get; set; }
        public bool Vomiting { get; set; }
        public bool Diarrhea { get; set; }
        public bool Headache { get; set; }
    }
}
