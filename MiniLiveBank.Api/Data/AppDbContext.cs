using Microsoft.EntityFrameworkCore;
using MiniLiveBank.Core.Models;

namespace MiniLiveBank.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Session> Sessions { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Advisor> Advisors { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Session>().Property(e=>e.Status).HasConversion<string>();
        modelBuilder.Entity<Message>().Property(e=>e.Sender).HasConversion<string>();

        modelBuilder.Entity<Session>().HasData(
            new Session("John Doe") { Id = 1, Status = Core.Models.Enums.SessionStatus.Waiting, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Session("Jane Smith") { Id = 2, Status = Core.Models.Enums.SessionStatus.Waiting, CreatedAt = new DateTime(2026, 1, 10, 0, 0, 0, DateTimeKind.Utc) }
        );

        modelBuilder.Entity<Advisor>().HasData(
            new Advisor("Alice Johnson") { Id = 1 },
            new Advisor("Bob Brown") { Id = 2 }
        );

 


    }
}
