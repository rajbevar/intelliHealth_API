using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain.MedicalEntity
{
   public   class MedicationEntity
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public string Text { get; set; }
        public int BeginOffset { get; set; }
        public int EndOffset { get; set; }
        /// <summary>
        ///  The amount of medication ordered.
        /// </summary>
        public string Dosage { get; set; }
        /// <summary>
        /// The administration method of a medication.
        /// </summary>
        public string RouteOrMode { get; set; }
        /// <summary>
        /// How often to administer the medication.
        /// </summary>
        public string Frequency { get; set; }
        public string Duration { get; set; }
        public int DurationInNumber { get; set; }
        public int FrenquencyInDay { get; set; }


        /// <summary>
        /// The form of the medication.
        /// </summary>
        public string Form { get; set; }
        /// <summary>
        /// Primarily for medication infusions or IVs, the administration rate of the medication.
        /// </summary>
        public string Rate { get; set; }
        /// <summary>
        /// The medication strength.
        /// </summary>
        public string Strength { get; set; }

        public List<TraitEntity> Traits { get; set; }

        public string Type { get; set; }
        public string Category { get; set; }
    }

   
}
