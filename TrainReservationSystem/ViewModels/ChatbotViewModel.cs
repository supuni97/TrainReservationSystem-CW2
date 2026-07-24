using System.ComponentModel.DataAnnotations;

namespace TrainReservationSystem.Models.ViewModels;

public class ChatbotViewModel
{
    [Required(
        ErrorMessage = "Please enter a question.")]
    [Display(
        Name = "Ask a Question")]
    public string UserMessage { get; set; }
        = string.Empty;



    public string BotResponse { get; set; }
        = string.Empty;



    public List<string> ChatHistory { get; set; }
        = new();



    public string WelcomeMessage { get; set; }
        =
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