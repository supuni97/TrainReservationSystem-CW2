using Microsoft.AspNetCore.Mvc;
using TrainReservationSystem.Models;
using TrainReservationSystem.Services;

namespace TrainReservationSystem.Controllers;

public class BookingController : Controller
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }


    public async Task<IActionResult> Index()
    {
        return View(await _bookingService.GetAll());
    }



    public async Task<IActionResult> Details(int id)
    {
        var booking = await _bookingService.GetById(id);

        if (booking == null)
            return NotFound();

        return View(booking);
    }



    [HttpGet]
    public IActionResult Create()
    {
        return View(new Booking());
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Booking booking)
    {
        ValidateBooking(booking);


        var bookings = await _bookingService.GetAll();


        if (bookings.Any(b =>
            b.TrainName == booking.TrainName &&
            b.TravelDate.Date == booking.TravelDate.Date &&
            b.DepartureTime == booking.DepartureTime &&
            b.SeatNumber == booking.SeatNumber))
        {
            ModelState.AddModelError(
                "",
                "This seat has already been booked for the selected train and departure."
            );
        }


        if (!ModelState.IsValid)
            return View(booking);


        await _bookingService.Add(booking);


        TempData["Success"] =
            "Booking created successfully.";


        return RedirectToAction(nameof(Index));
    }




    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var booking = await _bookingService.GetById(id);

        if (booking == null)
            return NotFound();

        return View(booking);
    }





    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Booking booking)
    {
        ValidateBooking(booking);


        var bookings = await _bookingService.GetAll();


        if (bookings.Any(b =>
            b.Id != booking.Id &&
            b.TrainName == booking.TrainName &&
            b.TravelDate.Date == booking.TravelDate.Date &&
            b.DepartureTime == booking.DepartureTime &&
            b.SeatNumber == booking.SeatNumber))
        {
            ModelState.AddModelError(
                "",
                "This seat has already been booked for the selected train and departure."
            );
        }



        if (!ModelState.IsValid)
            return View(booking);



        var existing =
            await _bookingService.GetById(booking.Id);


        if (existing == null)
            return NotFound();



        await _bookingService.Update(booking);



        TempData["Success"] =
            "Booking updated successfully.";


        return RedirectToAction(nameof(Index));
    }




    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var booking =
            await _bookingService.GetById(id);


        if (booking == null)
            return NotFound();


        return View(booking);
    }




    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var booking =
            await _bookingService.GetById(id);


        if (booking == null)
            return NotFound();



        await _bookingService.Delete(id);



        TempData["Success"] =
            "Booking deleted successfully.";


        return RedirectToAction(nameof(Index));
    }




    private void ValidateBooking(Booking booking)
    {
        if (booking.FromStation == booking.ToStation)
        {
            ModelState.AddModelError(
                "ToStation",
                "Departure and destination stations cannot be the same."
            );
        }


        if (booking.TravelDate.Date < DateTime.Today)
        {
            ModelState.AddModelError(
                "TravelDate",
                "Travel date cannot be in the past."
            );
        }


        if (booking.TravelDate == default)
        {
            ModelState.AddModelError(
                "TravelDate",
                "Please select a valid travel date."
            );
        }
    }
}