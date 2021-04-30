using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain
{
   [Flags]
     public enum PatientCreateStatus
    {

        
            Success = 1 << 1,
            AlreadyExist = 1 << 2,
            InternalServerError = 1 << 3
        
    }
}
