using Microsoft.EntityFrameworkCore;
using TrainReservationSystem.Data;
using TrainReservationSystem.Models.ViewModels;
using TrainReservationSystem.Services.Api;

namespace TrainReservationSystem.Services;

public class ReportService
{
    private readonly ApplicationDbContext _context;
    private readonly IBookingApiService _bookingApiService;

    public ReportService(
        ApplicationDbContext context,
        IBookingApiService bookingApiService)
    {
        _context = context;
        _bookingApiService = bookingApiService;
    }

    public async Task<WeeklyReportViewModel> GetWeeklyReportAsync(DateTime selectedDate)
    {
        int diff = selectedDate.DayOfWeek == DayOfWeek.Sunday
            ? 6
            : (int)selectedDate.DayOfWeek - 1;

        DateTime weekStart = selectedDate.Date.AddDays(-diff);
        DateTime weekEnd = weekStart.AddDays(6);

        var report = new WeeklyReportViewModel
        {
            WeekStartDate = weekStart,
            WeekEndDate = weekEnd,
            GeneratedOn = DateTime.Now
        };

        // ============================
        // Get Bookings from Booking API
        // ============================

        var allBookings = await _bookingApiService.GetAll();

        var weeklyBookings = allBookings
            .Where(b =>
                b.TravelDate.Date >= weekStart &&
                b.TravelDate.Date <= weekEnd)
            .OrderBy(b => b.TravelDate)
            .ToList();

        report.TotalBookings = weeklyBookings.Count;

        report.TotalRevenue = weeklyBookings.Sum(b => b.TicketPrice);

        // ============================
        // Get Special Requests locally
        // ============================

        var weeklyRequests = await _context.SpecialRequests
            .Where(r =>
                r.RequestDate.Date >= weekStart &&
                r.RequestDate.Date <= weekEnd)
            .OrderBy(r => r.RequestDate)
            .ToListAsync();

        report.TotalSpecialRequests = weeklyRequests.Count;

        // ============================
        // Daily Breakdown
        // ============================

        for (int i = 0; i < 7; i++)
        {
            DateTime currentDate = weekStart.AddDays(i);

            report.Days.Add(new DailyReportViewModel
            {
                DayName = currentDate.ToString("dddd"),

                Date = currentDate,

                Bookings = weeklyBookings
                    .Where(b =>
                        b.TravelDate.Date == currentDate.Date)
                    .OrderBy(b => b.DepartureTime)
                    .ToList(),

                SpecialRequests = weeklyRequests
                    .Where(r =>
                        r.RequestDate.Date == currentDate.Date)
                    .OrderBy(r => r.RequestType)
                    .ToList()
            });
        }

        // ============================
        // Insights
        // ============================

        var busiest = report.Days
            .OrderByDescending(d => d.Bookings.Count)
            .FirstOrDefault();

        if (busiest != null)
        {
            report.BusiestDay = busiest.DayName;
            report.BusiestDayBookings = busiest.Bookings.Count;
        }

        report.AverageTicketPrice = weeklyBookings.Any()
            ? weeklyBookings.Average(b => b.TicketPrice)
            : 0;

        report.MostUsedTrain = weeklyBookings.Any()
            ? weeklyBookings
                .GroupBy(b => b.TrainName)
                .OrderByDescending(g => g.Count())
                .First()
                .Key
            : "N/A";

        report.BookingStatusSummary = weeklyBookings
            .GroupBy(b => b.Status)
            .ToDictionary(
                g => g.Key,
                g => g.Count());

        return report;
    }
}