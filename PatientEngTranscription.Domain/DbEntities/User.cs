using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain
{
    public class Users : Entity<Guid>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid RecogProfileId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsDeleted { get; set; }
    }
}
