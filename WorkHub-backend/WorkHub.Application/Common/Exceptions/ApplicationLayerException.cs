using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Application
{
    public class ApplicationLayerException : Exception
    {
        public ApplicationLayerException(string message) : base(message) { }

        public ApplicationLayerException(string message, Exception innerException) : base(message, innerException) { }
    }
}
