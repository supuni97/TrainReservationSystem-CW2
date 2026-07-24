using TrainReservationSystem.Services;
using TrainReservationSystem.Models;

namespace TrainReservationSystem.Tests;

public class ChatbotServiceTests
{
    [Fact]
    public void Chatbot_Should_Return_Greeting()
    {
        var context =
            TestDbContextFactory.Create();


        var bookingService =
            new BookingService(context);


        var scheduleService =
            new ScheduleService(context);



        var chatbot =
            new ChatbotService(
                bookingService,
                scheduleService);



        scheduleService.Add(
            new Schedule
            {
                TrainName = "Intercity Express",
                TotalSeats = 100,
                IsActive = true
            });



        var result =
            chatbot.GetResponse("hello");



        Assert.Contains(
            "Hello",
            result);
    }
}