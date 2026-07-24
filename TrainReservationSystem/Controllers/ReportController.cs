using Microsoft.AspNetCore.Mvc;
using System.Text;
using TrainReservationSystem.Services;

namespace TrainReservationSystem.Controllers;

public class ReportController : Controller
{
    private readonly ReportService _reportService;


    public ReportController(ReportService reportService)
    {
        _reportService = reportService;
    }



    // ==============================
    // Display Weekly Report
    // ==============================

    [HttpGet]
    public async Task<IActionResult> WeeklyBookings(DateTime? selectedDate)
    {
        DateTime date =
            selectedDate ?? DateTime.Today;


        var report =
            await _reportService
            .GetWeeklyReportAsync(date);


        return View(report);
    }





    // ==============================
    // Export Weekly Report CSV
    // ==============================

    [HttpGet]
    public async Task<IActionResult> ExportCsv(DateTime? selectedDate)
    {
        DateTime date =
            selectedDate ?? DateTime.Today;


        var report =
            await _reportService
            .GetWeeklyReportAsync(date);



        var csv = new StringBuilder();



        csv.AppendLine(
            "WEEKLY TRAIN RESERVATION REPORT");


        csv.AppendLine(
            $"Week,{report.WeekStartDate:dd MMM yyyy} - {report.WeekEndDate:dd MMM yyyy}");


       csv.AppendLine(
    $"Generated,{report.GeneratedOn:yyyy-MM-dd HH:mm:ss}");

        csv.AppendLine();



        csv.AppendLine(
            $"Total Bookings,{report.TotalBookings}");


        csv.AppendLine(
            $"Total Special Requests,{report.TotalSpecialRequests}");


        csv.AppendLine(
            $"Total Revenue,Rs {report.TotalRevenue:N2}");



        csv.AppendLine();




        foreach(var day in report.Days)
        {
            csv.AppendLine(
                $"{day.DayName} - {day.Date:dd MMM yyyy}");


            csv.AppendLine();



            // Bookings

            csv.AppendLine("BOOKINGS");

            csv.AppendLine(
                "Booking ID,Train,From,To,Travel Date,Departure,Seat,Price,Status");



            foreach(var booking in day.Bookings)
            {
                csv.AppendLine(
    $"{booking.Id}," +
    $"\"{booking.TrainName}\"," +
    $"\"{booking.FromStation}\"," +
    $"\"{booking.ToStation}\"," +
    $"{booking.TravelDate:dd/MM/yyyy}," +
    $"{booking.DepartureTime}," +
    $"{booking.SeatNumber}," +
    $"\"Rs {booking.TicketPrice:N2}\"," +
    $"{booking.Status}");
            }



            csv.AppendLine();



            // Special Requests

            csv.AppendLine("SPECIAL REQUESTS");

            csv.AppendLine(
                "Request ID,Booking ID,Request Type,Request Date,Status");



            foreach(var request in day.SpecialRequests)
            {
                csv.AppendLine(
                    $"{request.Id}," +
                    $"{request.BookingId}," +
                    $"{request.RequestType}," +
                    $"{request.RequestDate:dd/MM/yyyy}," +
                    $"{request.Status}");
            }



            csv.AppendLine();

            csv.AppendLine(
                "----------------------------------------");

            csv.AppendLine();
        }



        return File(
            Encoding.UTF8.GetBytes(csv.ToString()),
            "text/csv",
            $"WeeklyReport_{report.WeekStartDate:yyyyMMdd}.csv");
    }
}