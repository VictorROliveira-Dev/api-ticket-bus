using APIBusService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIBusService.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TicketCode).IsRequired();
            entity.HasOne(e => e.User).WithMany(u => u.Tickets).HasForeignKey(e => e.UserId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Age).IsRequired();
        });
    }
}
