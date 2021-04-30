using PatientEngTranscription.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain.Models.Problems
{
    public class ProblemDto
    {
        public bool IsProblem { get; set; }
        public string Name { get; set; }
        public DateTime RecordedDate { get; set; }
        public int category { get; set; }
    }
}
