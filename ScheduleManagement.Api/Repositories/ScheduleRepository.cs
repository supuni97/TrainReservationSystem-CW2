using Microsoft.EntityFrameworkCore;
using ScheduleManagement.Api.Data;
using ScheduleManagement.Api.Interfaces;
using ScheduleManagement.Api.Models;

namespace ScheduleManagement.Api.Repositories;

public class ScheduleRepository(ScheduleDbContext context) : IScheduleRepository
{
    public async Task<IReadOnlyList<Schedule>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await context.Schedules
            .AsNoTracking()
            .OrderBy(schedule => schedule.TravelDate)
            .ThenBy(schedule => schedule.DepartureTime)
            .ToListAsync(cancellationToken);

    public Task<Schedule?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        context.Schedules.FirstOrDefaultAsync(schedule => schedule.Id == id, cancellationToken);

    public async Task AddAsync(Schedule schedule, CancellationToken cancellationToken = default)
    {
        await context.Schedules.AddAsync(schedule, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Schedule schedule, CancellationToken cancellationToken = default)
    {
        context.Schedules.Update(schedule);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Schedule schedule, CancellationToken cancellationToken = default)
    {
        context.Schedules.Remove(schedule);
        await context.SaveChangesAsync(cancellationToken);
    }
}