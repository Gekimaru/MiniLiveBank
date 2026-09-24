using MiniLiveBank.Api.Contracts;
using MiniLiveBank.Core.Models;
using MiniLiveBank.Core.Services;

namespace MiniLiveBank.Api.Endpoints;

public static class SessionEndpoints
{

    public static void MapSessionEndpoints(this WebApplication app)
    {



        app.MapPost("/sessions", async (CreateSessionRequest request, SessionService sessionService) => {
            Session session;
            session = await sessionService.CreateAsync(request.CustomerName);
            return Results.Created($"/sessions/{session.Id}", session);
        });

        app.MapGet("/sessions/waiting", async (SessionService sessionService) =>
        {
            var sessions = await sessionService.GetQueueAsync();

            return Results.Ok(sessions);
        });

        app.MapPost("/sessions/{id}/accept", async (int id, AcceptSessionRequest request, SessionService sessionService) =>
        {
            Session session;

            session = await sessionService.AcceptAsync(id, request.AdvisorId);
            return Results.Ok(session);
        });

        app.MapPost("/sessions/{id}/end", async (int id, SessionService sessionService) =>
        {
            Session session;
            session = await sessionService.EndAsync(id);
            return Results.Ok(session);
        });

    }

}
