using Microsoft.AspNetCore.Mvc;
using TrainReservationSystem.Models;
using TrainReservationSystem.Services.Api;

namespace TrainReservationSystem.Controllers;

public class ScheduleController : Controller
{
    private readonly IScheduleApiService _scheduleApiService;

    public ScheduleController(IScheduleApiService scheduleApiService)
    {
        _scheduleApiService = scheduleApiService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _scheduleApiService.GetAll());
    }

    public async Task<IActionResult> Details(int id)
    {
        var schedule = await _scheduleApiService.GetById(id);

        if (schedule == null)
            return NotFound();

        return View(schedule);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Schedule());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Schedule schedule)
    {
        ValidateSchedule(schedule);

        var schedules = await _scheduleApiService.GetAll();

        if (schedules.Any(s =>
            s.TrainName == schedule.TrainName &&
            s.TravelDate.Date == schedule.TravelDate.Date &&
            s.DepartureTime == schedule.DepartureTime))
        {
            ModelState.AddModelError(
                "",
                "A schedule already exists for this train and departure time.");
        }

        if (!ModelState.IsValid)
            return View(schedule);

        await _scheduleApiService.Add(schedule);

        TempData["Success"] = "Schedule created successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var schedule = await _scheduleApiService.GetById(id);

        if (schedule == null)
            return NotFound();

        return View(schedule);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Schedule schedule)
    {
        ValidateSchedule(schedule);

        var schedules = await _scheduleApiService.GetAll();

        if (schedules.Any(s =>
            s.Id != schedule.Id &&
            s.TrainName == schedule.TrainName &&
            s.TravelDate.Date == schedule.TravelDate.Date &&
            s.DepartureTime == schedule.DepartureTime))
        {
            ModelState.AddModelError(
                "",
                "A schedule already exists for this train and departure time.");
        }

        if (!ModelState.IsValid)
            return View(schedule);

        var existing = await _scheduleApiService.GetById(schedule.Id);

        if (existing == null)
            return NotFound();

        await _scheduleApiService.Update(schedule);

        TempData["Success"] = "Schedule updated successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var schedule = await _scheduleApiService.GetById(id);

        if (schedule == null)
            return NotFound();

        return View(schedule);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var schedule = await _scheduleApiService.GetById(id);

        if (schedule == null)
            return NotFound();

        await _scheduleApiService.Delete(id);

        TempData["Success"] = "Schedule deleted successfully.";

        return RedirectToAction(nameof(Index));
    }

    private void ValidateSchedule(Schedule schedule)
    {
        if (schedule.FromStation == schedule.ToStation)
        {
            ModelState.AddModelError(
                "ToStation",
                "Departure and destination stations cannot be the same.");
        }

        if (schedule.ArrivalTime <= schedule.DepartureTime)
        {
            ModelState.AddModelError(
                "ArrivalTime",
                "Arrival time must be after departure time.");
        }

        if (schedule.TravelDate.Date < DateTime.Today)
        {
            ModelState.AddModelError(
                "TravelDate",
                "Travel date cannot be in the past.");
        }
    }
}