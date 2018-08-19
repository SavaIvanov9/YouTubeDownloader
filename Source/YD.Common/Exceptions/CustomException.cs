using System;
using System.Runtime.Serialization;

namespace YD.Common.Exceptions
{
    public abstract class CustomException : Exception
    {
        protected CustomException(string message) : base(message)
        {
        }

        protected CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
