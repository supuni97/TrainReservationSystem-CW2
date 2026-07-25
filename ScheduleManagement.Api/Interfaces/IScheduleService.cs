using ScheduleManagement.Api.DTOs;

namespace ScheduleManagement.Api.Interfaces;

public interface IScheduleService
{
    Task<IReadOnlyList<ScheduleDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<ScheduleDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<ScheduleDto> CreateAsync(CreateScheduleRequest request, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(int id, UpdateScheduleRequest request, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}