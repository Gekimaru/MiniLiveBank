
namespace MiniLiveBank.Core.Models;

public class Advisor
{
    public Advisor()
    {

    }
    public Advisor(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
