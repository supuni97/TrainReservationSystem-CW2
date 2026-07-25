using BookingManagement.Api.Data;
using BookingManagement.Api.Interfaces;
using BookingManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagement.Api.Repositories;

public class BookingRepository(BookingDbContext context) : IBookingRepository
{
    public async Task<IReadOnlyList<Booking>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await context.Bookings
            .AsNoTracking()
            .OrderBy(booking => booking.TravelDate)
            .ThenBy(booking => booking.DepartureTime)
            .ToListAsync(cancellationToken);

    public Task<Booking?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        context.Bookings.FirstOrDefaultAsync(booking => booking.Id == id, cancellationToken);

    public async Task AddAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        await context.Bookings.AddAsync(booking, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        context.Bookings.Update(booking);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        context.Bookings.Remove(booking);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> SeatIsBookedAsync(Booking booking, CancellationToken cancellationToken = default) =>
        context.Bookings.AnyAsync(existing =>
            existing.Id != booking.Id &&
            existing.TrainName == booking.TrainName &&
            existing.TravelDate.Date == booking.TravelDate.Date &&
            existing.DepartureTime == booking.DepartureTime &&
            existing.SeatNumber == booking.SeatNumber,
            cancellationToken);
}
