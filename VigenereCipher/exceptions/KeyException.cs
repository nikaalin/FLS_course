using System;

namespace VigenereCipher
{
    public class KeyException : ApplicationException
    {
        public override string Message { get; }

        public KeyException(string message) : base()
        {
            Message = message;
        }
    }
}