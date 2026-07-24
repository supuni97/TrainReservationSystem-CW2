using TrainReservationSystem.Models;

namespace TrainReservationSystem.Services;

public class ChatbotService
{
    private readonly BookingService _bookingService;
    private readonly ScheduleService _scheduleService;


    public ChatbotService(
        BookingService bookingService,
        ScheduleService scheduleService)
    {
        _bookingService = bookingService;
        _scheduleService = scheduleService;
    }



    public string GetResponse(string message)
    {
        
if (string.IsNullOrWhiteSpace(message))
        return "Please enter a question.";


    message = message
        .ToLower()
        .Trim();


var bookings = _bookingService
    .GetAll()
    .Result;
var schedules = _scheduleService
    .GetAll()
    .Result;


    if (!schedules.Any())
        return "No train schedules are currently available.";


        // Greeting

        if (ContainsAny(message,
            "hi",
            "hello",
            "hey",
            "good morning",
            "good evening"))
        {
            return
@"Hello! 👋

I am your AI Train Booking Assistant.

I can help you with:

🚆 Available trains
💺 Seat availability
📊 Demand prediction
💰 Ticket price trends
✅ Booking recommendations
🔥 Busiest train
🌱 Least busy train
📅 Today's schedules";
        }





        // Help

        if (ContainsAny(message, "help"))
        {
            return
@"You can ask:

• List trains
• Which train is busiest?
• Which train is least busy?
• Available seats on Intercity Express
• Demand for Yal Devi
• Ticket price trends
• Should I book a train?";
        }





        // Busiest train

        if (ContainsAny(message,
            "busiest",
            "most busy",
            "crowded",
            "popular"))
        {

            var result =
                GetTrainStatistics(
                    schedules,
                    bookings)
                .OrderByDescending(x => x.Bookings)
                .FirstOrDefault();



            if (result == null)
                return "No booking data available.";



            return
$@"🔥 Busiest Train

🚆 Train:
{result.Train}

👥 Bookings:
{result.Bookings}

📊 Demand:
{result.Demand}";
        }





        // Least busy train

        if (ContainsAny(message,
            "least busy",
            "least",
            "less busy",
            "empty",
            "lowest demand"))
        {

            var result =
                GetTrainStatistics(
                    schedules,
                    bookings)
                .OrderBy(x => x.Bookings)
                .FirstOrDefault();



            if(result == null)
                return "No booking data available.";



            return
$@"🌱 Least Busy Train

🚆 Train:
{result.Train}

👥 Bookings:
{result.Bookings}

📊 Demand:
{result.Demand}

This train may have better availability.";
        }





        // List trains

        if(ContainsAny(message,
            "list trains",
            "available trains",
            "show trains",
            "what trains",
            "trains"))
        {

            return
@"🚆 Available Trains

" +
string.Join("\n",
schedules
.Select(s => s.TrainName)
.Distinct()
.OrderBy(x => x));
        }





        // Today schedule

        if(ContainsAny(message,
            "today",
            "schedule"))
        {

            var today =
                schedules
                .Where(s =>
                    s.TravelDate.Date ==
                    DateTime.Today)
                .ToList();



            if(!today.Any())
                return "No trains available today.";



            return
"📅 Today's Schedule\n\n" +
string.Join("\n",
today.Select(s =>
$"🚆 {s.TrainName} - {s.DepartureTime}"));
        }





        // Specific train analysis

        foreach(var schedule in schedules)
        {

            if(!TrainMentioned(
                message,
                schedule.TrainName))
                continue;



            int confirmed =
                bookings.Count(b =>
                    b.TrainName.Equals(
                        schedule.TrainName,
                        StringComparison.OrdinalIgnoreCase)
                    &&
                    !b.Status.Equals(
                        "Cancelled",
                        StringComparison.OrdinalIgnoreCase));



            int available =
                Math.Max(
                    0,
                    schedule.TotalSeats - confirmed);



            double occupancy =
                schedule.TotalSeats == 0
                ?
                0
                :
                (double)confirmed /
                schedule.TotalSeats;



            string demand =
                occupancy >= 0.8
                ?
                "High"
                :
                occupancy >= 0.5
                ?
                "Medium"
                :
                "Low";





            if(ContainsAny(message,
                "seat",
                "availability",
                "available"))
            {
                return
$@"💺 Seat Availability

🚆 {schedule.TrainName}

Total Seats:
{schedule.TotalSeats}

Booked:
{confirmed}

Available:
{available}

Occupancy:
{occupancy * 100:0}%

Demand:
{demand}";
            }





            if(ContainsAny(message,"demand"))
            {
                return
$@"📊 Demand Prediction

Train:
{schedule.TrainName}

Demand:
{demand}

Occupancy:
{occupancy * 100:0}%";
            }





            if(ContainsAny(message,
                "price",
                "fare",
                "cost"))
            {
                return demand switch
                {
                    "High" =>
                    "💰 High demand detected. Prices may increase.",

                    "Medium" =>
                    "💰 Prices are expected to remain stable.",

                    _ =>
                    "💰 Low demand. Prices are unlikely to increase."
                };
            }





            if(ContainsAny(message,
                "book",
                "recommend",
                "should i",
                "choice"))
            {
                return
$@"✅ Booking Recommendation

🚆 {schedule.TrainName}

Available Seats:
{available}

Demand:
{demand}

Recommendation:
{(available <= 10
? "Book soon because seats are limited."
: "Good availability. You can book anytime.")}";
            }
        }



        return
@"Sorry, I could not understand.

Try:

• List trains
• Which train is busiest?
• Which train is least busy?
• Seat availability
• Demand prediction";
    }






    private bool ContainsAny(
        string message,
        params string[] keywords)
    {
        var words =
            message.Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries);



        foreach(var keyword in keywords)
        {
            if(keyword.Contains(" "))
            {
                if(message.Contains(keyword))
                    return true;
            }
            else
            {
                if(words.Contains(keyword))
                    return true;
            }
        }

        return false;
    }





    private bool TrainMentioned(
        string message,
        string trainName)
    {
        var parts =
            trainName
            .ToLower()
            .Split(' ',
            StringSplitOptions.RemoveEmptyEntries);



        return parts.Any(p =>
            message.Contains(p));
    }





    private List<TrainStats> GetTrainStatistics(
        List<Schedule> schedules,
        List<Booking> bookings)
    {

        return schedules
            .Select(s => new TrainStats
            {
                Train = s.TrainName,

                Bookings =
                    bookings.Count(b =>
                        b.TrainName.Equals(
                            s.TrainName,
                            StringComparison.OrdinalIgnoreCase)
                        &&
                        !b.Status.Equals(
                            "Cancelled",
                            StringComparison.OrdinalIgnoreCase))
            })

            .Select(x =>
            {
                x.Demand =
                    x.Bookings >= 20
                    ?
                    "High"
                    :
                    x.Bookings >= 10
                    ?
                    "Medium"
                    :
                    "Low";

                return x;

            })
            .ToList();
    }





    private class TrainStats
    {
        public string Train { get; set; } = "";

        public int Bookings { get; set; }

        public string Demand { get; set; } = "";
    }
}