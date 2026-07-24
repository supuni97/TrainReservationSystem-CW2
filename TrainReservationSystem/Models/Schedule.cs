using System.ComponentModel.DataAnnotations;

namespace TrainReservationSystem.Models;

public class Schedule
{
    public int Id { get; set; }

    [Range(1, 1000)]
    [Display(Name = "Total Seats")]
    public int TotalSeats { get; set; } = 100;

    [Required(ErrorMessage = "Please select a train.")]
    [StringLength(100)]
    [Display(Name = "Train")]
    public string TrainName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select the departure station.")]
    [StringLength(100)]
    [Display(Name = "Departure Station")]
    public string FromStation { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select the destination station.")]
    [StringLength(100)]
    [Display(Name = "Destination Station")]
    public string ToStation { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Travel Date")]
    public DateTime TravelDate { get; set; }

    [Required]
    [DataType(DataType.Time)]
    [Display(Name = "Departure Time")]
    public TimeSpan DepartureTime { get; set; }

    [Required]
    [DataType(DataType.Time)]
    [Display(Name = "Arrival Time")]
    public TimeSpan ArrivalTime { get; set; }

    [Display(Name = "Schedule Status")]
    public bool IsActive { get; set; } = true;
}