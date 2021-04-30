using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
    public class Operation400BadRequestException : OperationException
    {
        public Operation400BadRequestException()
        {
        }

        public Operation400BadRequestException(string message) : base(message)
        {
        }

        public Operation400BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public Operation400BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override int StatusCode => 400;    
    }
}
