using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
    [Serializable]
    public abstract class OperationException : Exception
    {
        public OperationException()
        {
        }

        public OperationException(string message) : base(message)
        {
        }

        public OperationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public abstract int StatusCode { get; }

    }
}
