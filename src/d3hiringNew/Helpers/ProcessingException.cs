using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace d3hiringNew.Helpers
{
    [Serializable]
    internal class ProcessingException : Exception
    {
        public ProcessingException()
        {
        }

        public ProcessingException(string message) : base(message)
        {
        }

        public ProcessingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProcessingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
