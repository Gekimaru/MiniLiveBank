using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLiveBank.Core.Exceptions;

public class InvalidSessionStateException : Exception
{
    public InvalidSessionStateException(string message) : base(message)
    {

    }
}
