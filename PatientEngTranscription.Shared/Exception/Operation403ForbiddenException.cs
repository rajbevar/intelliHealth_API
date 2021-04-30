using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace System
{ 
    public class Operation403ForbiddenException : OperationException
    {
        public Operation403ForbiddenException()
        {
        }

        public Operation403ForbiddenException(string message) : base(message)
        {
        }

        public Operation403ForbiddenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public Operation403ForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override int StatusCode => throw new NotImplementedException();
    }
}
