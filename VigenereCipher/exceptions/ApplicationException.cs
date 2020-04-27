using System;

namespace VigenereCipherAPI.exceptions
{
    public abstract class ApplicationException: Exception
    { 
        public string Message { get; set; }
    }
}