using System.ComponentModel.DataAnnotations;

namespace BookingManagement.Api.Models;

public class Booking
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please select a train.")]
    [StringLength(100)]
    [Display(Name = "Train")]
    public string TrainName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a departure station.")]
    [StringLength(100)]
    [Display(Name = "Departure Station")]
    public string FromStation { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a destination station.")]
    [StringLength(100)]
    [Display(Name = "Destination Station")]
    public string ToStation { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a travel date.")]
    [DataType(DataType.Date)]
    [Display(Name = "Travel Date")]
    public DateTime TravelDate { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Please select a departure time.")]
    [DataType(DataType.Time)]
    [Display(Name = "Departure Time")]
    public TimeSpan DepartureTime { get; set; }

    [Required(ErrorMessage = "Seat number is required.")]
    [RegularExpression(@"^[A-Z]\d{2}$", ErrorMessage = "Seat number must be in the format A01.")]
    [Display(Name = "Seat Number")]
    public string SeatNumber { get; set; } = string.Empty;

    [Required]
    [Range(100, 10000, ErrorMessage = "Ticket price must be between LKR 100 and LKR 10,000.")]
    [Display(Name = "Ticket Price (LKR)")]
    public decimal TicketPrice { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Confirmed";
}
