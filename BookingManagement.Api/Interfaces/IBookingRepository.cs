using BookingManagement.Api.Models;

namespace BookingManagement.Api.Interfaces;

public interface IBookingRepository
{
    Task<IReadOnlyList<Booking>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Booking?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(Booking booking, CancellationToken cancellationToken = default);

    Task UpdateAsync(Booking booking, CancellationToken cancellationToken = default);

    Task DeleteAsync(Booking booking, CancellationToken cancellationToken = default);

    Task<bool> SeatIsBookedAsync(Booking booking, CancellationToken cancellationToken = default);
}
