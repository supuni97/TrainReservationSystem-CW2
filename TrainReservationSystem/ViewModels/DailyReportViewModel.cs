using TrainReservationSystem.Models;

namespace TrainReservationSystem.Models.ViewModels;

public class DailyReportViewModel
{
    public string DayName { get; set; } = "";

    public DateTime Date { get; set; }

    public List<Booking> Bookings { get; set; } = new();

    public List<SpecialRequest> SpecialRequests { get; set; } = new();
}