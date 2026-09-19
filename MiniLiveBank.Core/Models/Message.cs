using MiniLiveBank.Core.Models.Enums;

namespace MiniLiveBank.Core.Models;

public class Message
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public Session? Session { get; set; }
    public MessageSender Sender { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
