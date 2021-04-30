using PatientEngTranscription.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain.Models.Problems
{
    public class ProblemCreateRequest
    {

        public bool IsProblem { get; set; }
        public string Name { get; set; }
        public ProblemCategory category { get; set; }

    }
}
