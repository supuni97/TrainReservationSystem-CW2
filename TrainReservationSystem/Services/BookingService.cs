using Microsoft.EntityFrameworkCore;
using TrainReservationSystem.Data;
using TrainReservationSystem.Models;

namespace TrainReservationSystem.Services;

public class BookingService
{
    private readonly ApplicationDbContext _context;


    public BookingService(ApplicationDbContext context)
    {
        _context = context;
    }



    public async Task<List<Booking>> GetAll()
    {
        return await _context.Bookings
            .Include(b => b.SpecialRequests)
            .ToListAsync();
    }




    public async Task<Booking?> GetById(int id)
    {
        return await _context.Bookings
            .Include(b => b.SpecialRequests)
            .FirstOrDefaultAsync(b => b.Id == id);
    }




    public async Task Add(Booking booking)
    {
        _context.Bookings.Add(booking);

        await _context.SaveChangesAsync();
    }





    public async Task Update(Booking booking)
    {
        var existing =
            await _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == booking.Id);


        if (existing == null)
            return;


        existing.TrainName = booking.TrainName;
        existing.FromStation = booking.FromStation;
        existing.ToStation = booking.ToStation;
        existing.TravelDate = booking.TravelDate;
        existing.DepartureTime = booking.DepartureTime;
        existing.SeatNumber = booking.SeatNumber;
        existing.TicketPrice = booking.TicketPrice;
        existing.Status = booking.Status;


        await _context.SaveChangesAsync();
    }





    public async Task Delete(int id)
    {
        var booking =
            await _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == id);


        if (booking == null)
            return;


        _context.Bookings.Remove(booking);


        await _context.SaveChangesAsync();
    }
}