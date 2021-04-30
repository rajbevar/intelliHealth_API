using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain
{
    [Flags]
    public enum NotesCreateStatus
    {
        Success = 1 << 1,
        AlreadyExist = 1 << 2,
        InternalServerError = 1 << 3
    }
}
