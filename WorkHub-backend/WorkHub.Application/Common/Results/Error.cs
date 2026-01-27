using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Application
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }

        private Error(string code, string message)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new InvalidOperationException("Error code cannot be null or empty.");

            Code = code;
            Message = message;
        }

        public static readonly Error None = new("None", string.Empty);
        public static Error Create(string code, string message) => new (code, message);
    }
}
