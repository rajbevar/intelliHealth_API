using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain
{
    public class PatientDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string ExternalId { get; set; }
        public string Gender { get; set; }
        public int Weight { get; set; }

        public int Age { get
            {
                int age = 0;
                age = DateTime.Now.Year - DateOfBirth.Value.Year;
                if (DateTime.Now.DayOfYear < DateOfBirth.Value.DayOfYear)
                    age = age - 1;
                return age;
            }
        }
    }
}
