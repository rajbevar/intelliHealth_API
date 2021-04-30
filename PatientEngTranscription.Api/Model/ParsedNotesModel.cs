using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientEngTranscription.Api.Model
{
    public class ParsedNotesModel
    {
        public bool isBot { get; set; }
        public string Name { get; set; }
        public string Doage { get; set; }
        public string Unit { get; set; }
        public string Duration { get; set; }
        public string DurationUnit { get; set; }
    }
}
