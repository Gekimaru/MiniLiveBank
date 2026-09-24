using System.ComponentModel.DataAnnotations;

namespace MiniLiveBank.Api.Contracts;

public record AcceptSessionRequest
{
    [Required]
    public int AdvisorId { get; init; }
}
