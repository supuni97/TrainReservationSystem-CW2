namespace ScheduleManagement.Api.DTOs;

public class ScheduleDto
{
    public int Id { get; init; }

    public int TotalSeats { get; init; }

    public string TrainName { get; init; } = string.Empty;

    public string FromStation { get; init; } = string.Empty;

    public string ToStation { get; init; } = string.Empty;

    public DateTime TravelDate { get; init; }

    public TimeSpan DepartureTime { get; init; }

    public TimeSpan ArrivalTime { get; init; }

    public bool IsActive { get; init; }
}