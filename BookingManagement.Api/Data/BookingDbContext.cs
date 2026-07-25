using BookingManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagement.Api.Data;

public class BookingDbContext(DbContextOptions<BookingDbContext> options)
    : DbContext(options)
{
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Booking>()
            .Property(booking => booking.TicketPrice)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Booking>()
            .HasIndex(booking => booking.TravelDate);
    }
}
