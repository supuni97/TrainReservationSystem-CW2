using Microsoft.EntityFrameworkCore;
using ScheduleManagement.Api.Data;
using ScheduleManagement.Api.Interfaces;
using ScheduleManagement.Api.Repositories;
using ScheduleManagement.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScheduleDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ScheduleManagementConnection")));

builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();

    var context = scope.ServiceProvider.GetRequiredService<ScheduleDbContext>();

    await ScheduleDbInitializer.InitialiseAsync(context);

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();