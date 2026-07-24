using TrainReservationSystem.Models;

namespace TrainReservationSystem.Models.ViewModels;

public class WeeklyReportViewModel
{
    public DateTime WeekStartDate { get; set; }

    public DateTime WeekEndDate { get; set; }

    public DateTime GeneratedOn { get; set; }


    // Summary
    public int TotalBookings { get; set; }

    public int TotalSpecialRequests { get; set; }

    public decimal TotalRevenue { get; set; }



    // Daily reports
    public List<DailyReportViewModel> Days { get; set; } = new();



    // Reporting Insights

    public int BusiestDayBookings { get; set; }

    public string BusiestDay { get; set; } = "";

    public decimal AverageTicketPrice { get; set; }

    public string MostUsedTrain { get; set; } = "";



    // Booking status analysis

    public Dictionary<string, int> BookingStatusSummary { get; set; } = new();

}