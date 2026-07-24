using System.ComponentModel.DataAnnotations;

namespace TrainReservationSystem.Models;

public class SpecialRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Booking ID is required.")]
    [Display(Name = "Booking ID")]
    public int BookingId { get; set; }

    [Required(ErrorMessage = "Please select a request type.")]
    [StringLength(100)]
    [Display(Name = "Request Type")]
    public string RequestType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter request details.")]
    [StringLength(300)]
    [Display(Name = "Request Details")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Requested Date")]
    [DataType(DataType.Date)]
    public DateTime RequestDate { get; set; } = DateTime.Today;

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Pending";

    public Booking? Booking { get; set; }
}