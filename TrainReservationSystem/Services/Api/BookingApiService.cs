using System.Net.Http.Json;
using TrainReservationSystem.Models;

namespace TrainReservationSystem.Services.Api;

public class BookingApiService(HttpClient httpClient) : IBookingApiService
{
    public async Task<List<Booking>> GetAll()
    {
        return await httpClient.GetFromJsonAsync<List<Booking>>("api/bookings")
               ?? new List<Booking>();
    }

    public async Task<Booking?> GetById(int id)
    {
        return await httpClient.GetFromJsonAsync<Booking>($"api/bookings/{id}");
    }

    public async Task Add(Booking booking)
    {
        var response = await httpClient.PostAsJsonAsync("api/bookings", booking);
        response.EnsureSuccessStatusCode();
    }

    public async Task Update(Booking booking)
    {
        var response = await httpClient.PutAsJsonAsync($"api/bookings/{booking.Id}", booking);
        response.EnsureSuccessStatusCode();
    }

    public async Task Delete(int id)
    {
        var response = await httpClient.DeleteAsync($"api/bookings/{id}");
        response.EnsureSuccessStatusCode();
    }
}