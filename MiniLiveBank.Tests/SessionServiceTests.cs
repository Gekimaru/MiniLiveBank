using MiniLiveBank.Core.Models.Enums;
using MiniLiveBank.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLiveBank.Tests;

public class SessionServiceTests
{
    [Fact]
    public async Task CreateAsync_ValidName_ReturnsWaitingSession()
    {
        var sessionRepository = new FakeSessionRepository();
        SessionService sessionService = new SessionService(sessionRepository);

        var response = await sessionService.CreateAsync("John Doe");

        Assert.Equal(SessionStatus.Waiting, response.Status);
        Assert.Equal("John Doe", response.CustomerName);
        return;
    }

    [Fact]
    public async Task CreateAsync_EmptyName_ThrowsAsync()
    {
        var sessionRepository = new FakeSessionRepository();
        SessionService sessionService = new SessionService(sessionRepository);
        await Assert.ThrowsAsync<ArgumentException>(() => sessionService.CreateAsync(""));
    }
    [Fact]
    public async Task AcceptAsync_WaitingSession_BecomesActive()
    {
        var sessionRepository = new FakeSessionRepository();
        SessionService sessionService = new SessionService(sessionRepository);

        var session = await sessionService.CreateAsync("John Doe");
        var changedSession = await sessionService.AcceptAsync(session.Id, 1);
        if(changedSession.Status != SessionStatus.Active)
        {
            throw new Exception("Session status should be Active after accepting.");
        }
        if(changedSession.AdvisorId != 1)
        {
            throw new Exception("AdvisorId should be set to 1 after accepting.");
        }
        if(changedSession.AcceptedAt == null)
        {
            throw new Exception("AcceptedAt should be set after accepting.");
        }
    }
    [Fact]
    public async Task AcceptAsync_NonExistingSession_Throws()
    {
        var sessionRepository = new FakeSessionRepository();
        SessionService sessionService = new SessionService(sessionRepository);
        await Assert.ThrowsAsync<ArgumentException>(() => sessionService.AcceptAsync(999, 1));
    }
    [Fact]
    public async Task AcceptAsync_NonExistingAdvisor_Throws()
    {
        var sessionRepository = new FakeSessionRepository();
        SessionService sessionService = new SessionService(sessionRepository);
        await Assert.ThrowsAsync<ArgumentException>(() => sessionService.AcceptAsync(1, 999));
    }
}
