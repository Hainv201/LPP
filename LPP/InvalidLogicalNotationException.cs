using System;
using System.Runtime.Serialization;

namespace LPP
{
    [Serializable]
    internal class InvalidLogicalNotationException : Exception
    {
        public InvalidLogicalNotationException()
        {
        }

        public InvalidLogicalNotationException(string message) : base(message)
        {
        }

        public InvalidLogicalNotationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidLogicalNotationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}