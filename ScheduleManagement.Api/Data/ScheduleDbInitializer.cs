using Microsoft.EntityFrameworkCore;
using ScheduleManagement.Api.Models;

namespace ScheduleManagement.Api.Data;

public static class ScheduleDbInitializer
{
    public static async Task InitialiseAsync(ScheduleDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Schedules.AnyAsync())
        {
            return;
        }

        var travelDate = DateTime.Today.AddDays(1);

        context.Schedules.AddRange(
            new Schedule
            {
                TotalSeats = 100,
                TrainName = "Intercity Express",
                FromStation = "Colombo Fort",
                ToStation = "Kandy",
                TravelDate = travelDate,
                DepartureTime = new TimeSpan(8, 30, 0),
                ArrivalTime = new TimeSpan(11, 0, 0),
                IsActive = true
            },
            new Schedule
            {
                TotalSeats = 120,
                TrainName = "Udarata Menike",
                FromStation = "Colombo Fort",
                ToStation = "Badulla",
                TravelDate = travelDate.AddDays(1),
                DepartureTime = new TimeSpan(8, 45, 0),
                ArrivalTime = new TimeSpan(18, 15, 0),
                IsActive = true
            },
            new Schedule
            {
                TotalSeats = 150,
                TrainName = "Yal Devi",
                FromStation = "Colombo Fort",
                ToStation = "Jaffna",
                TravelDate = travelDate.AddDays(2),
                DepartureTime = new TimeSpan(6, 0, 0),
                ArrivalTime = new TimeSpan(13, 45, 0),
                IsActive = true
            });

        await context.SaveChangesAsync();
    }
}