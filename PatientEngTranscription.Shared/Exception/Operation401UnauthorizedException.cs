using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
    public class Operation401UnauthorizedException : OperationException
    {
        public Operation401UnauthorizedException()
        {
        }

        public Operation401UnauthorizedException(string message) : base(message)
        {
        }

        public Operation401UnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public Operation401UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override int StatusCode => 401;
    }
}
