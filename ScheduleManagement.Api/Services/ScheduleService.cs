using ScheduleManagement.Api.DTOs;
using ScheduleManagement.Api.Interfaces;
using ScheduleManagement.Api.Models;

namespace ScheduleManagement.Api.Services;

public class ScheduleService(IScheduleRepository repository) : IScheduleService
{
    public async Task<IReadOnlyList<ScheduleDto>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await repository.GetAllAsync(cancellationToken))
            .Select(ToDto)
            .ToList();

    public async Task<ScheduleDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var schedule = await repository.GetByIdAsync(id, cancellationToken);
        return schedule is null ? null : ToDto(schedule);
    }

    public async Task<ScheduleDto> CreateAsync(CreateScheduleRequest request, CancellationToken cancellationToken = default)
    {
        var schedule = ToEntity(request);

        ValidateSchedule(schedule);

        await repository.AddAsync(schedule, cancellationToken);

        return ToDto(schedule);
    }

    public async Task<bool> UpdateAsync(int id, UpdateScheduleRequest request, CancellationToken cancellationToken = default)
    {
        var schedule = await repository.GetByIdAsync(id, cancellationToken);

        if (schedule is null)
        {
            return false;
        }

        schedule.TotalSeats = request.TotalSeats;
        schedule.TrainName = request.TrainName;
        schedule.FromStation = request.FromStation;
        schedule.ToStation = request.ToStation;
        schedule.TravelDate = request.TravelDate;
        schedule.DepartureTime = request.DepartureTime;
        schedule.ArrivalTime = request.ArrivalTime;
        schedule.IsActive = request.IsActive;

        ValidateSchedule(schedule);

        await repository.UpdateAsync(schedule, cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var schedule = await repository.GetByIdAsync(id, cancellationToken);

        if (schedule is null)
        {
            return false;
        }

        await repository.DeleteAsync(schedule, cancellationToken);

        return true;
    }

    private static Schedule ToEntity(CreateScheduleRequest request) => new()
    {
        TotalSeats = request.TotalSeats,
        TrainName = request.TrainName,
        FromStation = request.FromStation,
        ToStation = request.ToStation,
        TravelDate = request.TravelDate,
        DepartureTime = request.DepartureTime,
        ArrivalTime = request.ArrivalTime,
        IsActive = request.IsActive
    };

    private static ScheduleDto ToDto(Schedule schedule) => new()
    {
        Id = schedule.Id,
        TotalSeats = schedule.TotalSeats,
        TrainName = schedule.TrainName,
        FromStation = schedule.FromStation,
        ToStation = schedule.ToStation,
        TravelDate = schedule.TravelDate,
        DepartureTime = schedule.DepartureTime,
        ArrivalTime = schedule.ArrivalTime,
        IsActive = schedule.IsActive
    };

    private static void ValidateSchedule(Schedule schedule)
    {
        if (schedule.FromStation == schedule.ToStation)
        {
            throw new InvalidOperationException("Departure and destination stations cannot be the same.");
        }

        if (schedule.TravelDate == default || schedule.TravelDate.Date < DateTime.Today)
        {
            throw new InvalidOperationException("Travel date must be today or later.");
        }

        if (schedule.ArrivalTime <= schedule.DepartureTime)
        {
            throw new InvalidOperationException("Arrival time must be after departure time.");
        }
    }
}