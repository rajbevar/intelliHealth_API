using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
    public class Operation404NotFoundException : OperationException
    {
        public Operation404NotFoundException()
        {
        }

        public Operation404NotFoundException(string message) : base(message)
        {
        }

        public Operation404NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public Operation404NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override int StatusCode => 404;
    }
}
