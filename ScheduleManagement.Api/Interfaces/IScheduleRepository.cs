using ScheduleManagement.Api.Models;

namespace ScheduleManagement.Api.Interfaces;

public interface IScheduleRepository
{
    Task<IReadOnlyList<Schedule>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Schedule?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(Schedule schedule, CancellationToken cancellationToken = default);

    Task UpdateAsync(Schedule schedule, CancellationToken cancellationToken = default);

    Task DeleteAsync(Schedule schedule, CancellationToken cancellationToken = default);
}