using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain
{
    public class PatientCreateRequest
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string ExternalId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
