using MiniLiveBank.Core.Exceptions;
using MiniLiveBank.Core.Models.Enums;
using MiniLiveBank.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLiveBank.Tests;

public class SessionServiceTests : IDisposable
{
    private readonly SessionService sessionService;
    private readonly FakeSessionRepository sessionRepository;
    private const int NonExistingId = 999;
    private const string ExampleCustomerName = "John Doe";
    private const string ExampleAdvisorName = "Jane Smith";
    private const int FirstAdvisorId = 1;

    public SessionServiceTests()
    {
        sessionRepository = new FakeSessionRepository();
        sessionRepository.AddAdvisorAsync(new MiniLiveBank.Core.Models.Advisor() { Name = ExampleAdvisorName });
        sessionService = new SessionService(sessionRepository);
    }
    
    public void Dispose()
    {
        return;
    }
    [Fact]
    public async Task CreateAsync_ValidName_ReturnsWaitingSession()
    {
        

        var response = await sessionService.CreateAsync(ExampleCustomerName);

        Assert.Equal(SessionStatus.Waiting, response.Status);
        Assert.Equal(ExampleCustomerName, response.CustomerName);
    }

    [Fact]
    public async Task CreateAsync_EmptyName_ThrowsAsync()
    {

        await Assert.ThrowsAsync<ArgumentException>(() => sessionService.CreateAsync(""));
    }

    [Fact]
    public async Task AcceptAsync_WaitingSession_BecomesActive()
    {
        var session = await sessionService.CreateAsync(ExampleCustomerName);

        var changedSession = await sessionService.AcceptAsync(session.Id, FirstAdvisorId);
        
        Assert.Equal(SessionStatus.Active, changedSession.Status);
    }

    [Fact]
    public async Task AcceptAsync_NonExistingSession_Throws()
    {
        await Assert.ThrowsAsync<SessionNotFoundException>(() => sessionService.AcceptAsync(NonExistingId, FirstAdvisorId));
    }

    [Fact]
    public async Task AcceptAsync_NonExistingAdvisor_Throws()
    {
        var session = await sessionService.CreateAsync(ExampleCustomerName);
        await Assert.ThrowsAsync<ArgumentException>(() => sessionService.AcceptAsync(session.Id, NonExistingId));
    }

    [Fact]
    public async Task AcceptAsync_DoubleAccept_Throws()
    {
        var session = await sessionService.CreateAsync(ExampleCustomerName);
        var advisor = await sessionRepository.GetAdvisorByIdAsync(FirstAdvisorId);

        await sessionService.AcceptAsync(session.Id, FirstAdvisorId);
        await Assert.ThrowsAsync<InvalidSessionStateException>(() => sessionService.AcceptAsync(session.Id, FirstAdvisorId));
    }

    [Fact]
    public async Task GetQueueAsnyc_ThreeSessions_IsCorrectOrder()
    {
        await sessionService.CreateAsync("Customer 1");
        await Task.Delay(10);
        await sessionService.CreateAsync("Customer 2");
        await Task.Delay(10);
        await sessionService.CreateAsync("Customer 3");

        var queue = await sessionService.GetQueueAsync();

        Assert.Equal(3, queue.Count);
        Assert.Equal("Customer 1", queue[0].CustomerName);
        Assert.Equal("Customer 2", queue[1].CustomerName);
        Assert.Equal("Customer 3", queue[2].CustomerName);
    }


    [Fact]
    public async Task EndAsync_ActiveSession_BecomesEnded()
    {
        var session = await sessionService.CreateAsync(ExampleCustomerName);

        var acceptedSession = await sessionService.AcceptAsync(session.Id, FirstAdvisorId);
        var endedSession = await sessionService.EndAsync(acceptedSession.Id);

        Assert.Equal(SessionStatus.Ended, endedSession.Status);
    }

    [Fact]
    public async Task EndAsync_NonExistingSession_Throws()
    {
        await Assert.ThrowsAsync<SessionNotFoundException>(() => sessionService.EndAsync(NonExistingId));
    }

    [Fact]
    public async Task EndAsync_NonActiveSession_Throws()
    {
        var session = await sessionService.CreateAsync(ExampleCustomerName);

        await Assert.ThrowsAsync<InvalidSessionStateException>(() => sessionService.EndAsync(session.Id));
    }


}
