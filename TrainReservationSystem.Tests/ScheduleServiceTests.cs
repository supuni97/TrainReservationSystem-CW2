using TrainReservationSystem.Services;
using TrainReservationSystem.Models;

namespace TrainReservationSystem.Tests;

public class ScheduleServiceTests
{

    [Fact]
    public async Task Add_Schedule_Should_Save()
    {
        var context =
            TestDbContextFactory.Create();


        var service =
            new ScheduleService(context);



        var schedule = new Schedule
        {
            TrainName = "Ruhunu Kumari",
            FromStation = "Colombo",
            ToStation = "Matara",
            TotalSeats = 100,
            IsActive = true
        };


        await service.Add(schedule);



        var schedules = await service.GetAll();


        Assert.Single(schedules);
    }




    [Fact]
    public async Task Delete_Should_Remove_Schedule()
    {
        var context =
            TestDbContextFactory.Create();


        var service =
            new ScheduleService(context);



        var schedule = new Schedule
        {
            TrainName = "Test Train",
            FromStation = "Colombo",
            ToStation = "Kandy",
            TotalSeats = 50,
            IsActive = true
        };



        await service.Add(schedule);



        await service.Delete(schedule.Id);



        var schedules = await service.GetAll();


        Assert.Empty(schedules);

    }

}