using System.ComponentModel.DataAnnotations;

namespace ScheduleManagement.Api.DTOs;

public class CreateScheduleRequest
{
    [Range(1, 1000)]
    public int TotalSeats { get; init; } = 100;

    [Required]
    [StringLength(100)]
    public string TrainName { get; init; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string FromStation { get; init; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string ToStation { get; init; } = string.Empty;

    [Required]
    public DateTime TravelDate { get; init; }

    [Required]
    public TimeSpan DepartureTime { get; init; }

    [Required]
    public TimeSpan ArrivalTime { get; init; }

    public bool IsActive { get; init; } = true;
}