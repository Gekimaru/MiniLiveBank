using MiniLiveBank.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLiveBank.Core.Interfaces;

public interface ISessionRepository
{
    Task AddSessionAsync(Session session);
    Task<List<Session>> GetWaitingListAsync();
    Task<Session?> GetSessionByIdAsync(int sessionId);
    Task<Advisor?> GetAdvisorByIdAsync(int advisorId);
    Task SaveChangesAsync();
}
