using System;

namespace VigenereCipherAPI
{
    public class FileException: ApplicationException
    {
        public override string Message { get; }

        public FileException(string message) : base()
        {
            Message = message;
        }

    }
}