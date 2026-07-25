using ScheduleManagement.Api.Models;

namespace ScheduleManagement.Api.Interfaces;

public interface IScheduleService
{
    Task<List<Schedule>> GetAllAsync(CancellationToken cancellationToken);
    Task<Schedule?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Schedule> CreateAsync(Schedule schedule, CancellationToken cancellationToken);
    Task<Schedule?> UpdateAsync(Schedule schedule, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}