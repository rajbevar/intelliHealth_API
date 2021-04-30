﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain
{
    public class NotesDto
    {
        public Guid Id { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public Guid DoctorId { get; set; }
        public string Note { get; set; }

        public bool isParsed { get; set; } = false;

        public bool isIgnored { get; set; } = false;
    }
}
