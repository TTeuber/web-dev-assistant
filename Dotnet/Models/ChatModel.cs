using System.ComponentModel.DataAnnotations;

namespace Dotnet.Models;

public class Chat
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; } = "New Chat";
    public IList<Message> Messages { get; set; }
}

public class Message
{
    [Key]
    public Guid Id { get; set; }

    public Guid ChatId { get; set; }
    public Chat Chat { get; set; }

    public string Content { get; set; }
    public string Role { get; set; }
}