using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientEngTranscription.Api.Model
{
    public class PatientDeleteModel
    {

        public List<string> externalIds { get; set; }

        public PatientDeleteModel()
        {
            externalIds = new List<string>();
        }
    }

}
