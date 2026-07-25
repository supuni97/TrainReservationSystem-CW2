using BookingManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagement.Api.Data;

public static class BookingDbInitializer
{
    public static async Task InitialiseAsync(BookingDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Bookings.AnyAsync())
        {
            return;
        }

        var travelDate = DateTime.Today.AddDays(1);

        context.Bookings.AddRange(
            new Booking
            {
                TrainName = "Intercity Express",
                FromStation = "Colombo Fort",
                ToStation = "Kandy",
                TravelDate = travelDate,
                DepartureTime = new TimeSpan(8, 30, 0),
                SeatNumber = "A01",
                TicketPrice = 1500,
                Status = "Confirmed"
            },
            new Booking
            {
                TrainName = "Udarata Menike",
                FromStation = "Colombo Fort",
                ToStation = "Badulla",
                TravelDate = travelDate.AddDays(1),
                DepartureTime = new TimeSpan(8, 45, 0),
                SeatNumber = "A02",
                TicketPrice = 2200,
                Status = "Confirmed"
            },
            new Booking
            {
                TrainName = "Yal Devi",
                FromStation = "Colombo Fort",
                ToStation = "Jaffna",
                TravelDate = travelDate.AddDays(2),
                DepartureTime = new TimeSpan(6, 0, 0),
                SeatNumber = "A03",
                TicketPrice = 2000,
                Status = "Pending"
            });

        await context.SaveChangesAsync();
    }
}
