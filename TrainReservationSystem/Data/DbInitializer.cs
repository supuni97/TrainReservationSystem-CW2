using TrainReservationSystem.Models;

namespace TrainReservationSystem.Data;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Schedules.Any())
            return;


        DateTime today = DateTime.Today;

        DateTime weekStart =
            today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);



        // ==========================
        // TRAIN SCHEDULES
        // ==========================

        var schedules = new List<Schedule>
        {
            new()
            {
                TrainName="Intercity Express",
                FromStation="Colombo",
                ToStation="Kandy",
                DepartureTime=new TimeSpan(8,30,0),
                TravelDate=weekStart,
                TotalSeats=100,
                IsActive=true
            },

            new()
            {
                TrainName="Udarata Menike",
                FromStation="Colombo",
                ToStation="Badulla",
                DepartureTime=new TimeSpan(8,45,0),
                TravelDate=weekStart.AddDays(2),
                TotalSeats=120,
                IsActive=true
            },

            new()
            {
                TrainName="Yal Devi",
                FromStation="Colombo",
                ToStation="Jaffna",
                DepartureTime=new TimeSpan(6,0,0),
                TravelDate=weekStart.AddDays(4),
                TotalSeats=150,
                IsActive=true
            },

            new()
            {
                TrainName="Ruhunu Kumari",
                FromStation="Colombo",
                ToStation="Matara",
                DepartureTime=new TimeSpan(9,45,0),
                TravelDate=weekStart.AddDays(6),
                TotalSeats=90,
                IsActive=true
            },

            new()
            {
                TrainName="Sagarika",
                FromStation="Colombo",
                ToStation="Galle",
                DepartureTime=new TimeSpan(10,30,0),
                TravelDate=weekStart.AddDays(9),
                TotalSeats=110,
                IsActive=true
            },

            new()
            {
                TrainName="Podi Menike",
                FromStation="Colombo",
                ToStation="Nanu Oya",
                DepartureTime=new TimeSpan(5,30,0),
                TravelDate=weekStart.AddDays(15),
                TotalSeats=80,
                IsActive=true
            }
        };


        context.Schedules.AddRange(schedules);
        context.SaveChanges();




        // ==========================
        // BOOKINGS
        // ==========================

        var bookings = new List<Booking>();


        // Week 1

        AddBookings(
            bookings,
            "Intercity Express",
            "Colombo",
            "Kandy",
            8,
            1500,
            weekStart);


        AddBookings(
            bookings,
            "Yal Devi",
            "Colombo",
            "Jaffna",
            5,
            2000,
            weekStart.AddDays(1));


        AddBookings(
            bookings,
            "Udarata Menike",
            "Colombo",
            "Badulla",
            10,
            2200,
            weekStart.AddDays(2));


        AddBookings(
            bookings,
            "Sagarika",
            "Colombo",
            "Galle",
            6,
            1600,
            weekStart.AddDays(3));


        AddBookings(
            bookings,
            "Ruhunu Kumari",
            "Colombo",
            "Matara",
            12,
            1700,
            weekStart.AddDays(5));


        AddBookings(
            bookings,
            "Podi Menike",
            "Colombo",
            "Nanu Oya",
            4,
            2500,
            weekStart.AddDays(6));




        // Week 2


        AddBookings(
            bookings,
            "Intercity Express",
            "Colombo",
            "Kandy",
            15,
            1500,
            weekStart.AddDays(7));


        AddBookings(
            bookings,
            "Yal Devi",
            "Colombo",
            "Jaffna",
            7,
            2000,
            weekStart.AddDays(9));


        AddBookings(
            bookings,
            "Udarata Menike",
            "Colombo",
            "Badulla",
            13,
            2200,
            weekStart.AddDays(11));


        AddBookings(
            bookings,
            "Sagarika",
            "Colombo",
            "Galle",
            9,
            1600,
            weekStart.AddDays(13));




        // Week 3


        AddBookings(
            bookings,
            "Ruhunu Kumari",
            "Colombo",
            "Matara",
            18,
            1700,
            weekStart.AddDays(14));


        AddBookings(
            bookings,
            "Podi Menike",
            "Colombo",
            "Nanu Oya",
            10,
            2500,
            weekStart.AddDays(16));


        AddBookings(
            bookings,
            "Intercity Express",
            "Colombo",
            "Kandy",
            20,
            1500,
            weekStart.AddDays(18));


        AddBookings(
            bookings,
            "Yal Devi",
            "Colombo",
            "Jaffna",
            11,
            2000,
            weekStart.AddDays(20));



        context.Bookings.AddRange(bookings);
        context.SaveChanges();





        // ==========================
        // SPECIAL REQUESTS
        // ==========================

        context.SpecialRequests.AddRange(

            new SpecialRequest
            {
                BookingId=bookings[0].Id,
                RequestType="Wheelchair",
                Status="Pending",
                RequestDate=weekStart
            },


            new SpecialRequest
            {
                BookingId=bookings[15].Id,
                RequestType="Window Seat",
                Status="Approved",
                RequestDate=weekStart.AddDays(7)
            },


            new SpecialRequest
            {
                BookingId=bookings[35].Id,
                RequestType="Food",
                Status="Pending",
                RequestDate=weekStart.AddDays(18)
            }


        );


        context.SaveChanges();

    }





    private static void AddBookings(
        List<Booking> list,
        string train,
        string from,
        string to,
        int count,
        decimal price,
        DateTime travelDate)
    {

        for(int i=1;i<=count;i++)
        {
            list.Add(new Booking
            {
                TrainName=train,

                FromStation=from,

                ToStation=to,

                TravelDate=travelDate,

                DepartureTime =
                    new TimeSpan(
                        6 + (i % 6),
                        30,
                        0),

                SeatNumber=$"A{i}",

                TicketPrice=price,

                Status =
                    i % 6 == 0
                    ? "Pending"
                    : "Confirmed"
            });
        }
    }
}