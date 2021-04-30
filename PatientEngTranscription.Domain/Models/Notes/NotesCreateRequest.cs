using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain
{
   public class NotesCreateRequest
    {
        public DateTime? CreateDateTime { get; set; }
        public Guid DoctorId { get; set; }
        public string Note { get; set; }
        public string ExternalId { get; set; }
    }
}
