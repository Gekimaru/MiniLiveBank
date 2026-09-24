using System.ComponentModel.DataAnnotations;

namespace MiniLiveBank.Api.Contracts;

public record CreateSessionRequest
{
    [Required, MinLength(1)]
    public string CustomerName { get; init; } = string.Empty;
};
  
