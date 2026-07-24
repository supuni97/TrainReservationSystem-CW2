using TrainReservationSystem.Services;
using TrainReservationSystem.Models;

namespace TrainReservationSystem.Tests;


public class BookingServiceTests
{

    [Fact]
    public async Task Add_Booking_Should_Save()
    {
        var context = TestDbContextFactory.Create();

        var service = new BookingService(context);


        var booking = new Booking
        {
            TrainName = "Intercity Express",
            FromStation = "Colombo",
            ToStation = "Kandy",
            TravelDate = DateTime.Today,
            TicketPrice = 1500,
            Status = "Confirmed"
        };


        await service.Add(booking);


        var result = await service.GetAll();


        Assert.Single(result);
        Assert.Equal(
            "Intercity Express",
            result[0].TrainName);
    }





    [Fact]
    public async Task GetById_Should_Return_Booking()
    {
        var context = TestDbContextFactory.Create();

        var service = new BookingService(context);


        var booking = new Booking
        {
            TrainName = "Ruhunu Kumari",
            TicketPrice = 1000
        };


        await service.Add(booking);



        var result =
            await service.GetById(booking.Id);



        Assert.NotNull(result);

        Assert.Equal(
            "Ruhunu Kumari",
            result!.TrainName);
    }




    [Fact]
    public async Task Delete_Should_Remove_Booking()
    {
        var context = TestDbContextFactory.Create();

        var service = new BookingService(context);


        var booking = new Booking
        {
            TrainName="Test Train"
        };


        await service.Add(booking);


        await service.Delete(booking.Id);



        var result =
            await service.GetAll();



        Assert.Empty(result);
    }

}