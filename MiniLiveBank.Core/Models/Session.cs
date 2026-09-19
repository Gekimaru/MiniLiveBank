using MiniLiveBank.Core.Models.Enums;

namespace MiniLiveBank.Core.Models;

public class Session
{
    public Session(string customerName)
    {
        CustomerName = customerName;
        Status = SessionStatus.Waiting;
        CreatedAt = DateTime.UtcNow;
    }

    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int? AdvisorId { get; set; }
    public Advisor? Advisor { get; set; }
    public SessionStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? AcceptedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public List<Message> Messages { get; set; } = new();
}
