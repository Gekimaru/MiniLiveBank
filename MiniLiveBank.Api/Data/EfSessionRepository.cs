using Microsoft.EntityFrameworkCore;
using MiniLiveBank.Core.Interfaces;
using MiniLiveBank.Core.Models;
using MiniLiveBank.Core.Models.Enums;

namespace MiniLiveBank.Api.Data;

public class EfSessionRepository : ISessionRepository
{
    private readonly AppDbContext _context;

    public EfSessionRepository(AppDbContext context)
    {
        _context = context;
    }
    public Task AddSessionAsync(Session session)
    {
        _context.Sessions.Add(session);
        return Task.CompletedTask;
    }

    public async Task<Advisor?> GetAdvisorByIdAsync(int advisorId)
    {
        return await _context.Advisors.FindAsync(advisorId);
    }

    public async Task<Session?> GetSessionByIdAsync(int sessionId)
    {
        return await _context.Sessions.FindAsync(sessionId);
    }

    public async Task<List<Session>> GetWaitingListAsync()
    {
        return await _context.Sessions.Where(s => s.Status == SessionStatus.Waiting).OrderBy(s => s.CreatedAt).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
