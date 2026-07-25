using BookingManagement.Api.DTOs;

namespace BookingManagement.Api.Interfaces;

public interface IBookingService
{
    Task<IReadOnlyList<BookingDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<BookingDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<BookingDto> CreateAsync(CreateBookingRequest request, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(int id, UpdateBookingRequest request, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
