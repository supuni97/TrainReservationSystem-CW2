using TrainReservationSystem.Services;
using TrainReservationSystem.Models;


namespace TrainReservationSystem.Tests;


public class SpecialRequestServiceTests
{

    [Fact]
    public async Task Add_Request_Should_Save()
    {
        var context =
            TestDbContextFactory.Create();


        // create required booking first
        var booking = new Booking
        {
            TrainName = "Intercity Express",
            FromStation = "Colombo",
            ToStation = "Kandy",
            TravelDate = DateTime.Today,
            TicketPrice = 1500,
            Status = "Confirmed"
        };


        context.Bookings.Add(booking);

        await context.SaveChangesAsync();



        var service =
            new SpecialRequestService(context);



        var request = new SpecialRequest
        {
            BookingId = booking.Id,
            RequestType = "Wheelchair",
            Status = "Pending",
            RequestDate = DateTime.Today
        };



        await service.Add(request);



        var result =
            await service.GetAll();



        Assert.Single(result);

    }





    [Fact]
    public async Task Delete_Request_Should_Remove()
    {
        var context =
            TestDbContextFactory.Create();


        var booking = new Booking
        {
            TrainName = "Test Train",
            TravelDate = DateTime.Today,
            TicketPrice = 1000
        };


        context.Bookings.Add(booking);

        await context.SaveChangesAsync();



        var service =
            new SpecialRequestService(context);



        var request = new SpecialRequest
        {
            BookingId = booking.Id,
            RequestType = "Food",
            Status = "Pending",
            RequestDate = DateTime.Today
        };


        await service.Add(request);



        await service.Delete(request.Id);



        var result =
            await service.GetAll();



        Assert.Empty(result);

    }

}