namespace BookingManagement.Api.DTOs;

public class BookingDto
{
    public int Id { get; init; }

    public string TrainName { get; init; } = string.Empty;

    public string FromStation { get; init; } = string.Empty;

    public string ToStation { get; init; } = string.Empty;

    public DateTime TravelDate { get; init; }

    public TimeSpan DepartureTime { get; init; }

    public string SeatNumber { get; init; } = string.Empty;

    public decimal TicketPrice { get; init; }

    public string Status { get; init; } = string.Empty;
}
