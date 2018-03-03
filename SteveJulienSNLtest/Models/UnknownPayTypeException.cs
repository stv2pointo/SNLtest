using System;
using System.Runtime.Serialization;

namespace SteveJulienSNLtest
{
    [Serializable]
    internal class UnknownPayTypeException : Exception
    {
        public UnknownPayTypeException()
        {
        }

        public UnknownPayTypeException(string message) : base(message)
        {
        }

        public UnknownPayTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownPayTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}