using System.Collections.Generic;

namespace PatientEngTranscription.Domain.Models
{
    public class DrugSuggestionModel
    {
        public List<DrugTreatmentModel> Treatments { get; set; }
        public bool IsError {get;set;}
        public string ErrorMessage { get; set; }
    }

    public class DrugTreatmentModel
    {
        public string Treatment { get; set; }
        public List<MedicationSuggestionModel> Medications { get; set; }
    }

    public class MedicationSuggestionModel
    {
        public string Severity { get; set; }
        public string Ratings { get; set; }
        public string DrugName { get; set; }
        public List<string> Symptoms { get; set; }
        public List<Agecategory> AgeCategory { get; set; }
        public object AlternativeDrug { get; set; }
    }

    public class Agecategory
    {
        public string Group { get; set; }
        public string Dosage { get; set; }
        public string Frequencey { get; set; }
        public string Mode { get; set; }
    }


}
