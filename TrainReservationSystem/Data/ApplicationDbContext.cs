using Microsoft.EntityFrameworkCore;
using TrainReservationSystem.Models;

namespace TrainReservationSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<Schedule> Schedules { get; set; }

    public DbSet<SpecialRequest> SpecialRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure decimal precision for ticket prices
        modelBuilder.Entity<Booking>()
            .Property(b => b.TicketPrice)
            .HasPrecision(10, 2);

        // Relationship: One Booking -> Many Special Requests
        modelBuilder.Entity<SpecialRequest>()
            .HasOne(s => s.Booking)
            .WithMany(b => b.SpecialRequests)
            .HasForeignKey(s => s.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        // Improve report query performance
        modelBuilder.Entity<Booking>()
            .HasIndex(b => b.TravelDate);
    }
}