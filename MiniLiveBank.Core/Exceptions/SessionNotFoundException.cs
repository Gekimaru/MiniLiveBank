using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLiveBank.Core.Exceptions;

public class SessionNotFoundException : Exception
{
    public SessionNotFoundException(int sessionId)
        : base($"Session with ID {sessionId} not found.")
    {
    }
}
