using System.ComponentModel.DataAnnotations;

namespace BookingManagement.Api.DTOs;

public class CreateBookingRequest
{
    [Required]
    [StringLength(100)]
    public string TrainName { get; init; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string FromStation { get; init; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string ToStation { get; init; } = string.Empty;

    public DateTime TravelDate { get; init; }

    public TimeSpan DepartureTime { get; init; }

    [Required]
    [RegularExpression(@"^[A-Z]\d{2}$")]
    public string SeatNumber { get; init; } = string.Empty;

    [Range(100, 10000)]
    public decimal TicketPrice { get; init; }

    [Required]
    [StringLength(20)]
    public string Status { get; init; } = "Confirmed";
}
