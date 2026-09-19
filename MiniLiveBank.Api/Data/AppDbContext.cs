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
    }
}
