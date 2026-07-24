using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TrainReservationSystem.Models.ViewModels;
using TrainReservationSystem.Services;

namespace TrainReservationSystem.Controllers;

public class ChatbotController : Controller
{
    private readonly ChatbotService _chatbotService;

    private const string SessionKey = "ChatHistory";

    private const int MaxMessages = 20;


    public ChatbotController(ChatbotService chatbotService)
    {
        _chatbotService = chatbotService;
    }



    [HttpGet]
    public IActionResult Index()
    {
        var model = new ChatbotViewModel
        {
            ChatHistory = GetHistory()
        };

        return View(model);
    }





    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(ChatbotViewModel model)
    {
        var history = GetHistory();


        if (!string.IsNullOrWhiteSpace(model.UserMessage))
        {
            string userMessage = model.UserMessage.Trim();


            string response =
                _chatbotService.GetResponse(userMessage);



            history.Add("You: " + userMessage);

            history.Add("Bot: " + response);



            SaveHistory(history);



            model.BotResponse = response;

            model.UserMessage = string.Empty;
        }



        model.ChatHistory = history;


        return View(model);
    }





    [HttpPost]
    public IActionResult Ask(
        [FromBody] ChatbotRequest request)
    {

        if(request == null ||
           string.IsNullOrWhiteSpace(request.Message))
        {
            return Json(new
            {
                success = false,
                response = "Please enter a question."
            });
        }



        string response =
            _chatbotService.GetResponse(request.Message);



        var history = GetHistory();


        history.Add("You: " + request.Message);

        history.Add("Bot: " + response);



        SaveHistory(history);



        return Json(new
        {
            success = true,
            response
        });
    }





    [HttpGet]
    public IActionResult Clear()
    {
        HttpContext.Session.Remove(SessionKey);

        return RedirectToAction(nameof(Index));
    }






    private List<string> GetHistory()
    {
        var history =
            HttpContext.Session.GetString(SessionKey);


        if(string.IsNullOrEmpty(history))
            return new List<string>();


        try
        {
            return JsonSerializer.Deserialize<List<string>>(history)
                   ?? new List<string>();
        }
        catch
        {
            return new List<string>();
        }
    }





    private void SaveHistory(List<string> history)
    {

        if(history.Count > MaxMessages)
        {
            history =
                history
                .TakeLast(MaxMessages)
                .ToList();
        }



        HttpContext.Session.SetString(
            SessionKey,
            JsonSerializer.Serialize(history));
    }
}




public class ChatbotRequest
{
    public string Message { get; set; }
        = string.Empty;
}