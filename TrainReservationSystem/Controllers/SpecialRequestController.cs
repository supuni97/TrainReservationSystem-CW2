using Microsoft.AspNetCore.Mvc;
using TrainReservationSystem.Models;
using TrainReservationSystem.Services;

namespace TrainReservationSystem.Controllers;

public class SpecialRequestController : Controller
{
    private readonly SpecialRequestService _specialRequestService;
    private readonly BookingService _bookingService;


    public SpecialRequestController(
        SpecialRequestService specialRequestService,
        BookingService bookingService)
    {
        _specialRequestService = specialRequestService;
        _bookingService = bookingService;
    }



    public async Task<IActionResult> Index()
    {
        return View(await _specialRequestService.GetAll());
    }



    public async Task<IActionResult> Details(int id)
    {
        var request =
            await _specialRequestService.GetById(id);


        if (request == null)
            return NotFound();


        return View(request);
    }



    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadBookings();

        return View(new SpecialRequest());
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        SpecialRequest request)
    {
        await LoadBookings();

        await ValidateRequest(request);



        var requests =
            await _specialRequestService.GetAll();


        if (requests.Any(r =>
            r.BookingId == request.BookingId &&
            r.RequestType == request.RequestType))
        {
            ModelState.AddModelError(
                "",
                "This special request already exists for the selected booking."
            );
        }



        if (!ModelState.IsValid)
            return View(request);



        await _specialRequestService.Add(request);



        TempData["Success"] =
            "Special request created successfully.";


        return RedirectToAction(nameof(Index));
    }





    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var request =
            await _specialRequestService.GetById(id);


        if (request == null)
            return NotFound();



        await LoadBookings();


        return View(request);
    }





    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        SpecialRequest request)
    {
        await LoadBookings();

        await ValidateRequest(request);



        var requests =
            await _specialRequestService.GetAll();



        if (requests.Any(r =>
            r.Id != request.Id &&
            r.BookingId == request.BookingId &&
            r.RequestType == request.RequestType))
        {
            ModelState.AddModelError(
                "",
                "This special request already exists for the selected booking."
            );
        }




        if (!ModelState.IsValid)
            return View(request);




        var existing =
            await _specialRequestService.GetById(request.Id);



        if (existing == null)
            return NotFound();



        await _specialRequestService.Update(request);



        TempData["Success"] =
            "Special request updated successfully.";


        return RedirectToAction(nameof(Index));
    }





    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var request =
            await _specialRequestService.GetById(id);



        if (request == null)
            return NotFound();



        return View(request);
    }





    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var request =
            await _specialRequestService.GetById(id);



        if (request == null)
            return NotFound();



        await _specialRequestService.Delete(id);



        TempData["Success"] =
            "Special request deleted successfully.";


        return RedirectToAction(nameof(Index));
    }





    private async Task LoadBookings()
    {
        ViewBag.Bookings =
            await _bookingService.GetAll();
    }





    private async Task ValidateRequest(
        SpecialRequest request)
    {
        var bookings =
            await _bookingService.GetAll();



        if (!bookings.Any(b =>
            b.Id == request.BookingId))
        {
            ModelState.AddModelError(
                "BookingId",
                "Please select a valid booking."
            );
        }



        if (request.RequestDate.Date < DateTime.Today)
        {
            ModelState.AddModelError(
                "RequestDate",
                "Request date cannot be in the past."
            );
        }
    }
}