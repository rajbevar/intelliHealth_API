using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
    public class Operation409ConflictException : OperationException
    {
        public Operation409ConflictException()
        {
        }

        public Operation409ConflictException(string message) : base(message)
        {
        }

        public Operation409ConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public Operation409ConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override int StatusCode => 409;
    }
}
