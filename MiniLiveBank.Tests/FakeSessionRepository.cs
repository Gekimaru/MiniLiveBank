using MiniLiveBank.Core.Interfaces;
using MiniLiveBank.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLiveBank.Tests;

public class FakeSessionRepository : ISessionRepository
{
    List<Session> _sessions = new List<Session>();
    List<Advisor> _advisors = new List<Advisor>();
    public Task AddSessionAsync(Session session)
    {
        session.Id = _sessions.Count > 0 ? _sessions.Max(s => s.Id) + 1 : 1;

        _sessions.Add(session);
        return Task.CompletedTask;
    }

    public Task AddAdvisorAsync(Advisor advisor)
    {
        advisor.Id = _advisors.Count > 0 ? _advisors.Max(a => a.Id) + 1 : 1;
        _advisors.Add(advisor);
        return Task.CompletedTask;
    }

    public Task<Advisor?> GetAdvisorByIdAsync(int advisorId)
    {
        
        Advisor? advisor = _advisors.FirstOrDefault(a => a.Id == advisorId);
        return Task.FromResult<Advisor?>(advisor);
    }

    public Task<Session?> GetSessionByIdAsync(int sessionId)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == sessionId);
        return Task.FromResult(session);
    }

    public Task<List<Session>> GetWaitingListAsync()
    {
        var waitingList = _sessions.Where(s => s.Status == Core.Models.Enums.SessionStatus.Waiting).OrderBy(s => s.CreatedAt).ToList();
        return Task.FromResult(waitingList);
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}
