using TrainReservationSystem.Models;

namespace TrainReservationSystem.Services.Api;

public interface IBookingApiService
{
    Task<List<Booking>> GetAll();

    Task<Booking?> GetById(int id);

    Task Add(Booking booking);

    Task Update(Booking booking);

    Task Delete(int id);
}