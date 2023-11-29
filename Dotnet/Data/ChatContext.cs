using Microsoft.EntityFrameworkCore;
using Dotnet.Models;

namespace Dotnet.Data;

public class ChatContext : DbContext
{
    public ChatContext(DbContextOptions<ChatContext> options)
        : base(options)
    {
    }

    public DbSet<Chat> Chat { get; set; } = default!;
    public DbSet<Message> Message { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>().HasData(
            new Chat{Id = Guid.NewGuid(), Title = "New Chat"}
        );
        modelBuilder.Entity<Message>().HasData(
            new Message{Id = Guid.NewGuid(), ChatId = Guid.NewGuid(), Content = "Hello World!", Role = "user" },
            new Message{Id = Guid.NewGuid(), ChatId = Guid.NewGuid(), Content = "Hello Person!", Role = "assistant" },
            new Message{Id = Guid.NewGuid(), ChatId = Guid.NewGuid(), Content = "Goodbye!", Role = "user" }
        );
    }

}