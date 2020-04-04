using System;
using System.Runtime.Serialization;

namespace ServiciosEnvios
{
    [Serializable]
    internal class envioEntidadesValidationException : Exception
    {
        public envioEntidadesValidationException()
        {
        }

        public envioEntidadesValidationException(string message) : base(message)
        {
        }

        public envioEntidadesValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected envioEntidadesValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}