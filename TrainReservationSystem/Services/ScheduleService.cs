using Microsoft.EntityFrameworkCore;
using TrainReservationSystem.Data;
using TrainReservationSystem.Models;

namespace TrainReservationSystem.Services;

public class ScheduleService
{
    private readonly ApplicationDbContext _context;


    public ScheduleService(ApplicationDbContext context)
    {
        _context = context;
    }



    public async Task<List<Schedule>> GetAll()
    {
        return await _context.Schedules
            .ToListAsync();
    }




    public async Task<Schedule?> GetById(int id)
    {
        return await _context.Schedules
            .FirstOrDefaultAsync(s => s.Id == id);
    }




    public async Task Add(Schedule schedule)
    {
        _context.Schedules.Add(schedule);

        await _context.SaveChangesAsync();
    }




    public async Task Update(Schedule schedule)
    {
        var existing = await _context.Schedules
            .FindAsync(schedule.Id);


        if (existing == null)
            return;


        existing.TrainName = schedule.TrainName;
        existing.FromStation = schedule.FromStation;
        existing.ToStation = schedule.ToStation;
        existing.TravelDate = schedule.TravelDate;
        existing.DepartureTime = schedule.DepartureTime;
        existing.ArrivalTime = schedule.ArrivalTime;
        existing.TotalSeats = schedule.TotalSeats;
        existing.IsActive = schedule.IsActive;


        await _context.SaveChangesAsync();
    }





    public async Task Delete(int id)
    {
        var schedule = await _context.Schedules
            .FindAsync(id);


        if (schedule != null)
        {
            _context.Schedules.Remove(schedule);

            await _context.SaveChangesAsync();
        }
    }
}