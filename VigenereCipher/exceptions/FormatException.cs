﻿using System;

namespace VigenereCipherAPI
{
    public class FormatException : ApplicationException
    {
        public override string Message { get; }

        public FormatException(string message) : base()
        {
            Message = message;
        }

    }
}