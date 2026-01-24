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
            Code = code;
            Message = message;
        }

        public static Error None => new Error(string.Empty, string.Empty);
        public static Error Create(string code, string message) => new (code, message);
    }
}
