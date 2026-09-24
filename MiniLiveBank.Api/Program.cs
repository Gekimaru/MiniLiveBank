using Microsoft.EntityFrameworkCore;
using MiniLiveBank.Api.Data;
using MiniLiveBank.Api.Endpoints;
using MiniLiveBank.Core.Exceptions;
using MiniLiveBank.Core.Interfaces;
using MiniLiveBank.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<ISessionRepository, EfSessionRepository>();
builder.Services.AddScoped<SessionService>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddValidation();
builder.Services.AddProblemDetails();


var app = builder.Build();

app.UseExceptionHandler(new ExceptionHandlerOptions {
    
       StatusCodeSelector = ex => ex switch
       {
           SessionNotFoundException => StatusCodes.Status404NotFound,
           InvalidSessionStateException => StatusCodes.Status409Conflict,
           ArgumentException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
       }
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapSessionEndpoints();

app.UseHttpsRedirection();





app.Run();

