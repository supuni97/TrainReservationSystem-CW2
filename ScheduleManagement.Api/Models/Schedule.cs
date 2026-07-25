using System.ComponentModel.DataAnnotations;

namespace ScheduleManagement.Api.Models;

public class Schedule
{
    public int Id { get; set; }

    [Range(1, 1000)]
    public int TotalSeats { get; set; } = 100;

    [Required]
    [StringLength(100)]
    public string TrainName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string FromStation { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string ToStation { get; set; } = string.Empty;

    [Required]
    public DateTime TravelDate { get; set; }

    [Required]
    public TimeSpan DepartureTime { get; set; }

    [Required]
    public TimeSpan ArrivalTime { get; set; }

    public bool IsActive { get; set; } = true;
}