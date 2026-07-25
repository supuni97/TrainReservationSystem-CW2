using System.Net.Http.Json;
using TrainReservationSystem.Models;

namespace TrainReservationSystem.Services.Api;

public class ScheduleApiService(HttpClient httpClient) : IScheduleApiService
{
    public async Task<List<Schedule>> GetAll()
    {
        return await httpClient.GetFromJsonAsync<List<Schedule>>("api/schedules")
               ?? new List<Schedule>();
    }

    public async Task<Schedule?> GetById(int id)
    {
        return await httpClient.GetFromJsonAsync<Schedule>($"api/schedules/{id}");
    }

    public async Task Add(Schedule schedule)
    {
        var response = await httpClient.PostAsJsonAsync("api/schedules", schedule);
        response.EnsureSuccessStatusCode();
    }

    public async Task Update(Schedule schedule)
    {
        var response = await httpClient.PutAsJsonAsync($"api/schedules/{schedule.Id}", schedule);
        response.EnsureSuccessStatusCode();
    }

    public async Task Delete(int id)
    {
        var response = await httpClient.DeleteAsync($"api/schedules/{id}");
        response.EnsureSuccessStatusCode();
    }
}