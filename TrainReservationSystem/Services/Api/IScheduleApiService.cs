using TrainReservationSystem.Models;

namespace TrainReservationSystem.Services.Api;

public interface IScheduleApiService
{
    Task<List<Schedule>> GetAll();

    Task<Schedule?> GetById(int id);

    Task Add(Schedule schedule);

    Task Update(Schedule schedule);

    Task Delete(int id);
}