using Microsoft.EntityFrameworkCore;
using ScheduleManagement.Api.Models;

namespace ScheduleManagement.Api.Data;

public class ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
    : DbContext(options)
{
    public DbSet<Schedule> Schedules => Set<Schedule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Schedule>()
            .HasIndex(schedule => schedule.TravelDate);

        modelBuilder.Entity<Schedule>()
            .HasIndex(schedule => schedule.TrainName);
    }
}