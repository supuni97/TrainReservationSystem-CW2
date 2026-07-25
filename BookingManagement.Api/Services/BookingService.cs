using BookingManagement.Api.DTOs;
using BookingManagement.Api.Interfaces;
using BookingManagement.Api.Models;

namespace BookingManagement.Api.Services;

public class BookingService(IBookingRepository repository) : IBookingService
{
    public async Task<IReadOnlyList<BookingDto>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await repository.GetAllAsync(cancellationToken))
            .Select(ToDto)
            .ToList();

    public async Task<BookingDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var booking = await repository.GetByIdAsync(id, cancellationToken);
        return booking is null ? null : ToDto(booking);
    }

    public async Task<BookingDto> CreateAsync(CreateBookingRequest request, CancellationToken cancellationToken = default)
    {
        var booking = ToEntity(request);
        ValidateBooking(booking);

        if (await repository.SeatIsBookedAsync(booking, cancellationToken))
        {
            throw new InvalidOperationException("This seat has already been booked for the selected train and departure.");
        }

        await repository.AddAsync(booking, cancellationToken);
        return ToDto(booking);
    }

    public async Task<bool> UpdateAsync(int id, UpdateBookingRequest request, CancellationToken cancellationToken = default)
    {
        var booking = await repository.GetByIdAsync(id, cancellationToken);
        if (booking is null)
        {
            return false;
        }

        booking.TrainName = request.TrainName;
        booking.FromStation = request.FromStation;
        booking.ToStation = request.ToStation;
        booking.TravelDate = request.TravelDate;
        booking.DepartureTime = request.DepartureTime;
        booking.SeatNumber = request.SeatNumber;
        booking.TicketPrice = request.TicketPrice;
        booking.Status = request.Status;

        ValidateBooking(booking);

        if (await repository.SeatIsBookedAsync(booking, cancellationToken))
        {
            throw new InvalidOperationException("This seat has already been booked for the selected train and departure.");
        }

        await repository.UpdateAsync(booking, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var booking = await repository.GetByIdAsync(id, cancellationToken);
        if (booking is null)
        {
            return false;
        }

        await repository.DeleteAsync(booking, cancellationToken);
        return true;
    }

    private static Booking ToEntity(CreateBookingRequest request) => new()
    {
        TrainName = request.TrainName,
        FromStation = request.FromStation,
        ToStation = request.ToStation,
        TravelDate = request.TravelDate,
        DepartureTime = request.DepartureTime,
        SeatNumber = request.SeatNumber,
        TicketPrice = request.TicketPrice,
        Status = request.Status
    };

    private static BookingDto ToDto(Booking booking) => new()
    {
        Id = booking.Id,
        TrainName = booking.TrainName,
        FromStation = booking.FromStation,
        ToStation = booking.ToStation,
        TravelDate = booking.TravelDate,
        DepartureTime = booking.DepartureTime,
        SeatNumber = booking.SeatNumber,
        TicketPrice = booking.TicketPrice,
        Status = booking.Status
    };

    private static void ValidateBooking(Booking booking)
    {
        if (booking.FromStation == booking.ToStation)
        {
            throw new InvalidOperationException("Departure and destination stations cannot be the same.");
        }

        if (booking.TravelDate == default || booking.TravelDate.Date < DateTime.Today)
        {
            throw new InvalidOperationException("Travel date must be today or later.");
        }
    }
}
