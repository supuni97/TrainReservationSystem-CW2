using TrainReservationSystem.Services;
using TrainReservationSystem.Models;


namespace TrainReservationSystem.Tests;


public class ReportServiceTests
{

    [Fact]
    public async Task WeeklyReport_Should_Return_Data()
    {

        var context =
            TestDbContextFactory.Create();


        var service =
            new ReportService(context);



        context.Bookings.Add(
            new Booking
            {
                TrainName = "Intercity",
                TravelDate = DateTime.Today,
                TicketPrice = 1000,
                Status = "Confirmed"
            });



        await context.SaveChangesAsync();




        var report =
            await service.GetWeeklyReportAsync(
                DateTime.Today);




        Assert.Equal(
            1,
            report.TotalBookings);



        Assert.Equal(
            1000,
            report.TotalRevenue);



        Assert.NotEmpty(
            report.Days);

    }

}