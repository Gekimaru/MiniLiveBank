using MiniLiveBank.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MiniLiveBank.Core.Models;
using MiniLiveBank.Core.Models.Enums;
using MiniLiveBank.Core.Exceptions;

namespace MiniLiveBank.Core.Services;

public class SessionService 
{
    private readonly ISessionRepository _sessionRepository;
    public SessionService(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }
    public async Task<Session> CreateAsync(string customerName)
    {
        if(string.IsNullOrWhiteSpace(customerName))
        {
            throw new ArgumentException("Customer name cannot be null or empty.", nameof(customerName));
        }
        var session = new Session(customerName);
        await _sessionRepository.AddSessionAsync(session);
        await _sessionRepository.SaveChangesAsync();
        return session;
    }
    public async Task<List<Session>> GetQueueAsync()
    {
        return await _sessionRepository.GetWaitingListAsync();
    }
    public async Task<Session> AcceptAsync(int sessionId, int advisorId)
    {
        var session = await _sessionRepository.GetSessionByIdAsync(sessionId);
        if(session == null)
        {
            throw new SessionNotFoundException(sessionId);
        }
        if(session.Status != SessionStatus.Waiting)
        {
            throw new InvalidSessionStateException($"Session:{session.Id} is not in a waiting state, it's in {session.Status} state when trying to accept.");
        }
        session.Status = SessionStatus.Active;
        session.AdvisorId = advisorId;
        session.AcceptedAt = DateTime.UtcNow;
        await _sessionRepository.SaveChangesAsync();
        return session;
    }
    public async Task<Session> EndAsync(int sessionId)
    {
        var session = await _sessionRepository.GetSessionByIdAsync(sessionId);
        if (session == null)
        {
            throw new SessionNotFoundException(sessionId);
        }
        if (session.Status != SessionStatus.Active)
        {
            throw new InvalidSessionStateException($"Session: {session.Id} is not in an active state, it's in {session.Status} state when trying to end.");
        }
        session.Status = SessionStatus.Ended;
        session.EndedAt = DateTime.UtcNow;
        await _sessionRepository.SaveChangesAsync();
        return session;
    }
}
