using Microsoft.EntityFrameworkCore;
using TrainReservationSystem.Data;
using TrainReservationSystem.Models;

namespace TrainReservationSystem.Services;

public class SpecialRequestService
{
    private readonly ApplicationDbContext _context;


    public SpecialRequestService(ApplicationDbContext context)
    {
        _context = context;
    }



    public async Task<List<SpecialRequest>> GetAll()
    {
        return await _context.SpecialRequests
            .Include(r => r.Booking)
            .ToListAsync();
    }



    public async Task<SpecialRequest?> GetById(int id)
    {
        return await _context.SpecialRequests
            .Include(r => r.Booking)
            .FirstOrDefaultAsync(r => r.Id == id);
    }



    public async Task Add(SpecialRequest request)
    {
        _context.SpecialRequests.Add(request);

        await _context.SaveChangesAsync();
    }



    public async Task Update(SpecialRequest request)
    {
        var existing = await _context.SpecialRequests
            .FindAsync(request.Id);


        if (existing == null)
            return;


        existing.BookingId = request.BookingId;
        existing.RequestType = request.RequestType;
        existing.Description = request.Description;
        existing.RequestDate = request.RequestDate;
        existing.Status = request.Status;


        await _context.SaveChangesAsync();
    }



    public async Task Delete(int id)
    {
        var request = await _context.SpecialRequests
            .FindAsync(id);


        if (request != null)
        {
            _context.SpecialRequests.Remove(request);

            await _context.SaveChangesAsync();
        }
    }
}