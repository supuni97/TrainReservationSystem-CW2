using Microsoft.EntityFrameworkCore;
using TrainReservationSystem.Data;
using TrainReservationSystem.Services;
using TrainReservationSystem.Services.Api;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// MVC
builder.Services.AddControllersWithViews();

// Session
builder.Services.AddSession();

builder.Services.AddHttpClient<IScheduleApiService, ScheduleApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7296/");
});

// Application Services
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<ScheduleService>();
builder.Services.AddScoped<SpecialRequestService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<ChatbotService>();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var context =
        scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    DbInitializer.Seed(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();